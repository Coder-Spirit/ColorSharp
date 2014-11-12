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
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Litipk.ColorSharp
{
	using ColorSpaces;

	namespace LightSpectrums
	{
		/**
		 * Class to handle light spectrum samples with equidistant data points
		 */
		public sealed class RegularLightSpectrum : ALightSpectrum
		{
			#region properties

			// Needed values to interpret the data points
			public readonly double NmPerStep;
			public readonly double MinWaveLength;
			public readonly double MaxWaveLength;

			// Data points
			public readonly ReadOnlyCollection<double> Amplitudes;
			#endregion


			#region constructors

			// Constructor
			public RegularLightSpectrum (double minWaveLength, double maxWaveLength, IList<double> amplitudes, AConvertibleColor dataSource=null) : base(dataSource)
			{
				MinWaveLength = minWaveLength;
				MaxWaveLength = maxWaveLength;
				NmPerStep = (maxWaveLength - minWaveLength) / (amplitudes.Count - 1);
				Amplitudes = new ReadOnlyCollection<double> (amplitudes);
			}

			// Constructor
			public RegularLightSpectrum (double minWaveLength, IList<double> amplitudes, double nmPerStep, AConvertibleColor dataSource=null) : base(dataSource)
			{
				NmPerStep = nmPerStep;
				MinWaveLength = minWaveLength;
				MaxWaveLength = minWaveLength + nmPerStep * (amplitudes.Count - 1);
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

			/**
			 * <inheritdoc />
			 */
			public override double GetNextAmplitudeSample (double waveLength)
			{
				if (waveLength < MinWaveLength && waveLength >= MaxWaveLength) {
					throw new ArgumentOutOfRangeException ();
				}

				return MinWaveLength + ((uint)Math.Floor ((waveLength - MinWaveLength) / NmPerStep) + 1) * NmPerStep;
			}

			#endregion


			#region AConvertibleColor methods

			/**
			 * <inheritdoc />
			 */
			public override bool IsInsideColorSpace()
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
		}
	}
}


