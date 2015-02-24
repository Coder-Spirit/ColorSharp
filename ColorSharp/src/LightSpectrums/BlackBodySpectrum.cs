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


namespace Litipk.ColorSharp
{
	namespace LightSpectrums
	{
		public sealed class BlackBodySpectrum : ALightSpectrum
		{
			#region properties

			public readonly double CCT;

			public readonly double MinWaveLength;
			public readonly double MaxWaveLength;

			#endregion

			#region constructors

			public BlackBodySpectrum (double cct) : this (cct, 380.0, 780.0)
			{
				// Nothing to do here
			}

			public BlackBodySpectrum (double cct, double minWaveLength, double maxWaveLength)
			{
				if (minWaveLength <= 0.0) {
					throw new ArgumentOutOfRangeException ("minWaveLength", "minWaveLength must be greater than 0");
				}

				if (maxWaveLength <= minWaveLength) {
					throw new ArgumentException("maxWaveLength must be greater than minWaveLength", "maxWaveLength");
				}

				if (cct <= 1) {
					throw new ArgumentOutOfRangeException ("cct", "cct must be greater than 1");
				}

				CCT = cct;
				MinWaveLength = minWaveLength;
				MaxWaveLength = maxWaveLength;
			}

			#endregion

			#region inherited methods

			public override double EvaluateAt (double waveLength)
			{
				// 2*h*c² = 2 * 299792458 * 1.98644568e-25 = 1.1910428661813628e-16
				return (1.1910428661813628e19 /*1.1910428661813628e-16*/ / (
					Math.Pow(waveLength/* * 1e-7*/, 5.0) * (Math.Exp(0.014387769576158687 / (waveLength * CCT)) - 1.0)
				));
			}

			public override double GetMaxValueOnSupport ()
			{
				double result = Double.NegativeInfinity;

				for (int i = 0; i + MinWaveLength < MaxWaveLength; i++) {
					result = Math.Max (EvaluateAt (MinWaveLength + i), result);
				}

				return result;
			}

			public override double GetSupportMinValue ()
			{
				return MinWaveLength;
			}

			public override double GetSupportMaxValue ()
			{
				return MaxWaveLength;
			}

			public override bool IsInsideColorSpace (bool highPrecision = false)
			{
				return true;
			}

			#endregion
		}
	}
}
