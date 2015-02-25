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

using Litipk.ColorSharp.ColorSpaces;
using Litipk.ColorSharp.InternalUtils;


namespace Litipk.ColorSharp
{
	namespace LightSpectrums
	{
		/**
		 * Class to handle light spectrum samples with not equidistant data points
		 */
		public sealed class TabularLightSpectrum : AInterpolatedLightSpectrum
		{
			#region private properties

			readonly List<KeyValuePair<double, double>> RawAmplitudes;

			#endregion


			#region constructors

			/**
			 * Preconditions : amplitudes MUST be sorted (crescent order)
			 */
			public TabularLightSpectrum (List<KeyValuePair<double, double>> amplitudes, AConvertibleColor dataSource=null) : base(dataSource)
			{
				RawAmplitudes = amplitudes;
			}

			#endregion


			#region ALightSpectrum methods

			/**
			 * <inheritdoc />
			 */
			public override double GetSupportMinValue ()
			{
				return RawAmplitudes[0].Key;
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetSupportMaxValue ()
			{
				return RawAmplitudes[RawAmplitudes.Count-1].Key;
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetMaxValueOnSupport ()
			{
				double max = 0;

				foreach (KeyValuePair<double, double> element in RawAmplitudes) {
					if (element.Value > max) {
						max = element.Value;
					}
				}

				return max;
			}

			/**
			 * <inheritdoc />
			 */
			public override int GetNumberOfDataPoints ()
			{
				return RawAmplitudes.Count;
			}

			/**
			 * <inheritdoc />
			 */
			public override double EvaluateAt(double waveLength)
			{
				if (waveLength < RawAmplitudes[0].Key || waveLength > RawAmplitudes[RawAmplitudes.Count-1].Key) {
					// TODO: Add extrapolation?
					throw new ArgumentOutOfRangeException();
				}

				int index = RawAmplitudes.BinarySearch(
					new KeyValuePair<double, double>(waveLength, 0), new KeyValuePairComparer()
				);

				if (index > 0)
					return RawAmplitudes [index].Value;

				index = ~index;

				double alpha = (waveLength-RawAmplitudes [index].Key)/(RawAmplitudes [index+1].Key-RawAmplitudes [index].Key);
				return (1.0-alpha)*RawAmplitudes [index].Value + alpha*RawAmplitudes [index+1].Value;
			}

			#endregion


			#region AConvertibleColor methods

			/**
			 * <inheritdoc />
			 */
			public override bool IsInsideColorSpace(bool highPrecision = false)
			{
				if (RawAmplitudes[0].Key <= double.Epsilon)
					return false;

				foreach (KeyValuePair<double, double> entry in RawAmplitudes) {
					if (entry.Value < 0.0)
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
				TabularLightSpectrum tls = obj as TabularLightSpectrum;

				if (tls == this) {
					return true;
				}
				if (tls == null || GetHashCode () != tls.GetHashCode ()) {
					return false;
				} 

				return RawAmplitudes.Equals(tls.RawAmplitudes);
			}

			/**
			 *  <inheritdoc />
			 */
			public override int GetHashCode ()
			{
				return 27221 + RawAmplitudes.GetHashCode ();  // 27221 = 163 * 167
			}

			#endregion
		}
	}
}

