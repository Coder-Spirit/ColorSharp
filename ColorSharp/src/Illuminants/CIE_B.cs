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
		 * The CIE Illuminant B is a (deprecated) daylight simulator intended to represent direct
		 * noon sunlight with a correlated colour temperature of 4874 K.
		 * </summary>
		 */
		public static class CIE_B
		{
			/**
			 * <value>'B' expressed in its 'original form', a light spectrum.</value>
			 */
			public static readonly RegularLightSpectrum spectrum_Sample = new RegularLightSpectrum(
				340, 770, new [] {
					2.40,
					4.00,
					5.60,
					7.60,
					9.60,
					12.40,
					15.20,
					18.80,
					22.40,
					26.85,
					31.30,
					36.18,
					41.30,
					46.62,
					52.10,
					57.70,
					63.20,
					68.37,
					73.10,
					77.31,
					80.80,
					83.44,
					85.40,
					86.88,
					88.30,
					90.08,
					92.00,
					93.75,
					95.20,
					96.23,
					96.50,
					95.71,
					94.20,
					92.37,
					90.70,
					89.65,
					89.50,
					90.43,
					92.20,
					94.46,
					96.90,
					99.16,
					101.00,
					102.20,
					102.80,
					102.92,
					102.60,
					101.90,
					101.00,
					100.07,
					99.20,
					98.44,
					98.00,
					98.08,
					98.50,
					99.06,
					99.70,
					100.36,
					101.00,
					101.56,
					102.20,
					103.05,
					103.90,
					104.59,
					105.00,
					105.08,
					104.90,
					104.55,
					103.90,
					102.84,
					101.60,
					100.38,
					99.10,
					97.70,
					96.20,
					94.60,
					92.90,
					91.10,
					89.40,
					88.00,
					86.90,
					85.90,
					85.20,
					84.80,
					84.70,
					84.90,
					85.40
				}
			);
		}
	}
}


