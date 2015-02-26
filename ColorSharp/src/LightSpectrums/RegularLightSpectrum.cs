/*
 * The MIT License (MIT)
 * Copyright (c) 2014 Andrés Correa Casablanca
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

/*
 * Contributors:
 *  - Andrés Correa Casablanca <castarco@gmail.com , castarco@litipk.com>
 */


using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Litipk.ColorSharp.ColorSpaces;


namespace Litipk.ColorSharp
{
	namespace LightSpectrums
	{
		/**
		 * Class to handle light spectrum samples with equidistant data points
		 */
		public sealed class RegularLightSpectrum : AInterpolatedLightSpectrum
		{
			#region properties

			/**
			 * <value>Nanometers between each data point.</value>
			 */
			public readonly double NmPerStep;

			/**
			 * <value>Minimum wavelength for which we have information on this spectrum.</value>
			 */
			public readonly double MinWaveLength;

			/**
			 * <value>Maximum wavelength for which we have information on this spectrum.</value>
			 */
			public readonly double MaxWaveLength;

			/**
			 * <value>Equidistant data points (usually relative values)</value>
			 */
			public readonly ReadOnlyCollection<double> Amplitudes;
			#endregion


			#region constructors

			/**
			 * <param name="minWaveLength">Associated wavelength to the first value of 'amplitudes'.</param>
			 * <param name="maxWaveLength">Associated wavelength to the last value of 'amplitudes'.</param>
			 * <param name="amplitudes">Equidistant data points.</param>
			 * <param name="dataSource">
			 *     Reference to a AConvertibleColor instance from which this object has been generated.
			 * </param>
			 */
			public RegularLightSpectrum (double minWaveLength, double maxWaveLength, IList<double> amplitudes, AConvertibleColor dataSource=null) : base(dataSource)
			{
				MinWaveLength = minWaveLength;
				MaxWaveLength = maxWaveLength;
				NmPerStep = (maxWaveLength - minWaveLength) / (amplitudes.Count - 1);
				Amplitudes = new ReadOnlyCollection<double> (amplitudes);
			}

			/**
			 * <param name="minWaveLength">Associated wavelength to the first value of 'amplitudes'.</param>
			 * <param name="amplitudes">Equidistant data points.</param>
			 * <param name="nmPerStep">Nanometers between each data point in 'amplitudes'.</param>
			 * <param name="dataSource">
			 *     Reference to a AConvertibleColor instance from which this object has been generated.
			 * </param>
			 */
			public RegularLightSpectrum (double minWaveLength, IList<double> amplitudes, double nmPerStep, AConvertibleColor dataSource=null) : base(dataSource)
			{
				NmPerStep = nmPerStep;
				MinWaveLength = minWaveLength;
				MaxWaveLength = minWaveLength + nmPerStep * (amplitudes.Count - 1);
				Amplitudes = new ReadOnlyCollection<double> (amplitudes);
			}

			/**
			 * <param name="amplitudes">Equidistant data points.</param>
			 * <param name="nmPerStep">Nanometers between each data point in 'amplitudes'.</param>
			 * <param name="maxWaveLength">Associated wavelength to the last value of 'amplitudes'.</param>
			 * <param name="dataSource">
			 *     Reference to a AConvertibleColor instance from which this object has been generated.
			 * </param>
			 */
			public RegularLightSpectrum (IList<double> amplitudes, double nmPerStep, double maxWaveLength, AConvertibleColor dataSource=null) : base(dataSource)
			{
				NmPerStep = nmPerStep;
				MaxWaveLength = maxWaveLength;
				MinWaveLength = maxWaveLength - nmPerStep * (amplitudes.Count - 1);
				Amplitudes = new ReadOnlyCollection<double> (amplitudes);
			}

			#endregion


			#region ALightSpectrum methods

			/**
			 * <inheritdoc />
			 */
			public override double EvaluateAt (double waveLength)
			{
				if (waveLength >= MinWaveLength && waveLength <= MaxWaveLength) {
					double dblIndex = (waveLength - MinWaveLength) / NmPerStep;
					double floorIndex = Math.Floor (dblIndex);
					int uIndex = (int)floorIndex;

					if (dblIndex - floorIndex <= 2*double.Epsilon) {
						return Amplitudes [uIndex];
					}

					double alpha = (dblIndex - floorIndex);

					return Amplitudes [uIndex] + alpha * (Amplitudes [uIndex + 1] - Amplitudes [uIndex]);
				}

				// TODO: add extrapolation
				throw new ArgumentOutOfRangeException ();
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetSupportMinValue()
			{
				return MinWaveLength;
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetSupportMaxValue()
			{
				return MaxWaveLength;
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetMaxValueOnSupport ()
			{
				double max = 0;

				for (int i = 0; i < Amplitudes.Count; i++) {
					if (Amplitudes [i] > max) {
						max = Amplitudes [i];
					}
				}

				return max;
			}

			/**
			 * <inheritdoc />
			 */
			public override int GetNumberOfDataPoints()
			{
				return Amplitudes.Count;
			}

			#endregion


			#region AConvertibleColor methods

			/**
			 * <inheritdoc />
			 */
			public override bool IsInsideColorSpace(bool highPrecision = false)
			{
				if (MinWaveLength <= double.Epsilon)
					return false;

				foreach (double amplitude in Amplitudes) {
					if (amplitude < 0.0)
						return false;
				}

				return true;
			}

			#endregion


			#region Object methods

			/**
			 *  <inheritdoc />
			 */
			public override bool Equals(Object obj)
			{
				RegularLightSpectrum rls = obj as RegularLightSpectrum;

				if (rls == this) {
					return true;
				}
				if (rls == null) {
					return false;
				}

				return (
					NmPerStep     == rls.NmPerStep     &&
					MinWaveLength == rls.MinWaveLength &&
					MaxWaveLength == rls.MaxWaveLength &&
					Amplitudes.SequenceEqual(rls.Amplitudes)
				);
			}

			/**
			 *  <inheritdoc />
			 */
			public override int GetHashCode ()
			{
				int hash = 25591 + NmPerStep.GetHashCode ();  // 25591 == 157 * 163

				hash = hash * 163 + MinWaveLength.GetHashCode ();
				hash = hash * 163 + MaxWaveLength.GetHashCode ();

				return hash * 163 + Amplitudes.GetHashCode ();
			}

			#endregion
		}
	}
}


