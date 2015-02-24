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
		 * The CIE illuminant C is a (deprecated) daylight simulator intended to represent
		 * average daylight with a correlated colour temperature of 6774 K.
		 * </summary>
		 */
		public static class CIE_C
		{
			/**
			 * <value>'C' expressed in its 'original form', a light spectrum.</value>
			 */
			public static readonly RegularLightSpectrum spectrum_Sample = new RegularLightSpectrum(
				380, 780, new [] {
					33.00,
					39.92,
					47.40,
					55.17,
					63.30,
					71.81,
					80.60,
					89.53,
					98.10,
					105.80,
					112.40,
					117.75,
					121.50,
					123.45,
					124.00,
					123.60,
					123.10,
					123.30,
					123.80,
					124.09,
					123.90,
					122.92,
					120.70,
					116.90,
					112.10,
					106.98,
					102.30,
					98.81,
					96.90,
					96.78,
					98.00,
					99.94,
					102.10,
					103.95,
					105.20,
					105.67,
					105.30,
					104.11,
					102.30,
					100.15,
					97.80,
					95.43,
					93.20,
					91.22,
					89.70,
					88.83,
					88.40,
					88.19,
					88.10,
					88.06,
					88.00,
					87.86,
					87.80,
					87.99,
					88.20,
					88.20,
					87.90,
					87.22,
					86.30,
					85.30,
					84.00,
					82.21,
					80.20,
					78.24,
					76.30,
					74.36,
					72.40,
					70.40,
					68.30,
					66.30,
					64.40,
					62.80,
					61.50,
					60.20,
					59.20,
					58.50,
					58.10,
					58.00,
					58.20,
					58.50,
					59.10
				}
			);
		}
	}
}



