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
		/**
		 * <summary>Class to represent black body spectrums at a given temperature.</summary>
		 */
		public sealed class BlackBodySpectrum : ALightSpectrum
		{
			#region properties

			/**
			 *
			 */
			public readonly double CCT;

			#endregion

			#region constructors

			/**
			 * <param name="cct">Black body's temperature (in Kelvin degrees).</param>
			 */
			public BlackBodySpectrum (double cct)
			{
				if (cct <= 1) {
					throw new ArgumentOutOfRangeException ("cct", "cct must be greater than 1");
				}

				CCT = cct;
			}

			#endregion

			#region inherited methods

			/**
			 * <inheritdoc />
			 */
			public override double EvaluateAt (double waveLength)
			{
				// Conversion from nanometers to meters
				waveLength *= 1e-9;

				// Applying Plank's Law : https://en.wikipedia.org/wiki/Planck's_law
				return (1.1910428661813628e-16 * Math.Pow(waveLength, -5.0) / (
					Math.Exp(0.014387769576158687 / (waveLength * CCT)) - 1.0
				));
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetMaxValueOnSupport ()
			{
				// https://en.wikipedia.org/wiki/Wien's_displacement_law
				return EvaluateAt (2.8977721e6/CCT);
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetSupportMinValue ()
			{
				return 1e-6;
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetSupportMaxValue ()
			{
				return double.PositiveInfinity;
			}

			/**
			 * <inheritdoc />
			 */
			public override bool IsInsideColorSpace (bool highPrecision = false)
			{
				return true;
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetCCT ()
			{
				return CCT;
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetDuv ()
			{
				return 0;
			}

			#endregion
		}
	}
}
