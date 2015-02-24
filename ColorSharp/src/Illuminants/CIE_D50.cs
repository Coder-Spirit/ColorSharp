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
		 * CIE Standard Illuminant D50. Constructed to represent natural daylight. CCT of 5003 K.
		 * </summary>
		 */
		public static class CIE_D50
		{
			/**
			 * <value>D50 expressed in its 'original form', a light spectrum.</value>
			 */
			public static readonly RegularLightSpectrum spectrum_Sample = new RegularLightSpectrum(
				380, 780, new [] {
					24.50,
					27.20,
					29.80,
					39.60,
					49.30,
					52.90,
					56.50,
					58.30,
					60.00,
					58.90,
					57.80,
					66.30,
					74.80,
					81.00,
					87.20,
					88.90,
					90.60,
					91.00,
					91.40,
					93.30,
					95.20,
					93.60,
					92.00,
					93.90,
					95.70,
					96.20,
					96.60,
					96.90,
					97.10,
					99.60,
					102.10,
					101.50,
					100.80,
					101.60,
					102.30,
					101.20,
					100.00,
					98.90,
					97.70,
					98.30,
					98.90,
					96.20,
					93.50,
					95.60,
					97.70,
					98.50,
					99.30,
					99.20,
					99.00,
					97.40,
					95.70,
					97.30,
					98.80,
					97.30,
					95.70,
					97.00,
					98.20,
					100.60,
					103.00,
					101.10,
					99.10,
					93.30,
					87.40,
					89.50,
					91.60,
					92.30,
					92.90,
					84.90,
					76.80,
					81.70,
					86.60,
					89.60,
					92.60,
					85.40,
					78.20,
					68.00,
					57.70,
					70.30,
					82.90,
					80.60,
					78.30,
				}
			);
		}
	}
}
