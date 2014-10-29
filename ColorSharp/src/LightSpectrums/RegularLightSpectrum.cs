/**
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

/**
* Contributors:
*  - Andrés Correa Casablanca <castarco@gmail.com , castarco@litipk.com>
*/


using System;


namespace Litipk.ColorSharp
{
	using ColorSpaces;

	namespace LightSpectrums
	{
		public class RegularLightSpectrum : ALightSpectrum
		{
			#region private properties

			// Needed values to interpret the data points
			readonly double nmPerStep;
			readonly double minWaveLength;
			readonly double maxWaveLength;

			// Data points
			readonly double[] amplitudes;
			#endregion


			#region constructors

			// Constructor
			public RegularLightSpectrum (double minWaveLength, double maxWaveLength, double[] amplitudes, AConvertibleColor dataSource=null) : base(dataSource)
			{
				this.minWaveLength = minWaveLength;
				this.maxWaveLength = maxWaveLength;
				nmPerStep = (maxWaveLength - minWaveLength) / amplitudes.Length;
				this.amplitudes = amplitudes;
			}

			// Constructor
			public RegularLightSpectrum (double minWaveLength, double[] amplitudes, double nmPerStep, AConvertibleColor dataSource=null) : base(dataSource)
			{
				this.nmPerStep = nmPerStep;
				this.minWaveLength = minWaveLength;
				maxWaveLength = minWaveLength + (nmPerStep) * (amplitudes.Length - 1);
				this.amplitudes = amplitudes;
			}

			#endregion


			#region inherited methods

			public override double EvaluateAt(double waveLength)
			{
				if (waveLength >= minWaveLength && waveLength <= maxWaveLength) {
					double dblIndex = (waveLength - minWaveLength) / nmPerStep;
					double floorIndex = Math.Floor (dblIndex);
					uint uIndex = (uint)floorIndex;

					if (dblIndex - floorIndex <= 2*double.Epsilon) {
						return amplitudes [uIndex];
					}

					double alpha = (dblIndex - floorIndex) / nmPerStep;

					return (1.0-alpha)*amplitudes[uIndex] + alpha*amplitudes[uIndex+1];
				}

				// TODO: add extrapolation
				throw new ArgumentOutOfRangeException ();
			}

			public override double GetSupportMinValue()
			{
				return minWaveLength;
			}

			public override double GetSupportMaxValue()
			{
				return maxWaveLength;
			}

			public override double GetMaxValueOnSupport ()
			{
				double max = 0;

				for (int i = 0; i < amplitudes.Length; i++) {
					if (amplitudes [i] > max) {
						max = amplitudes [i];
					}
				}

				return max;
			}

			public override int GetNumberOfDataPoints()
			{
				return amplitudes.Length;
			}

			public override double GetNextAmplitudeSample (double waveLength)
			{
				if (waveLength < minWaveLength && waveLength >= maxWaveLength) {
					throw new ArgumentOutOfRangeException ();
				}

				return minWaveLength + ((uint)Math.Floor ((waveLength - minWaveLength) / nmPerStep) + 1) * nmPerStep;
			}

			public override bool IsInsideColorSpace()
			{
				if (minWaveLength <= double.Epsilon)
					return false;

				foreach (double amplitude in amplitudes) {
					if (amplitude < 0.0)
						return false;
				}

				return true;
			}

			#endregion
		}
	}
}


