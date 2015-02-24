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


using Litipk.ColorSharp.ColorSpaces;
using Litipk.ColorSharp.LightSpectrums;


namespace Litipk.ColorSharp
{
	namespace Illuminants
	{
		/**
		 * <summary>
		 * CIE Standard Illuminant D55. Constructed to represent mid-morning/mid-afternoon daylight. CCT of 5503 K.
		 * </summary>
		 */
		public static class CIE_D55
		{
			/**
			 * <value>D50 expressed in its 'original form', a light spectrum.</value>
			 */
			public static readonly RegularLightSpectrum spectrum_Sample = new RegularLightSpectrum(
				380, 780, new [] {
					32.58,
					35.34,
					38.09,
					49.52,
					60.95,
					64.75,
					68.55,
					70.07,
					71.58,
					69.75,
					67.91,
					76.76,
					85.61,
					91.80,
					97.99,
					99.23,
					100.46,
					100.19,
					99.91,
					101.33,
					102.74,
					100.41,
					98.08,
					99.38,
					100.68,
					100.69,
					100.70,
					100.34,
					99.99,
					102.10,
					104.21,
					103.16,
					102.10,
					102.53,
					102.97,
					101.48,
					100.00,
					98.61,
					97.22,
					97.48,
					97.75,
					94.59,
					91.43,
					92.93,
					94.42,
					94.78,
					95.14,
					94.68,
					94.22,
					92.33,
					90.45,
					91.39,
					92.33,
					90.59,
					88.85,
					89.59,
					90.32,
					92.13,
					93.95,
					91.95,
					89.96,
					84.82,
					79.68,
					81.26,
					82.84,
					83.84,
					84.84,
					77.54,
					70.24,
					74.77,
					79.30,
					82.15,
					84.99,
					78.44,
					71.88,
					62.34,
					52.79,
					64.36,
					75.93,
					73.87,
					71.82
				}
			);
		}
	}
}

