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


using NUnit.Framework;


namespace Litipk.ColorSharp
{
	namespace ColorSpaces
	{
		[TestFixture]
		public class CIExyYTest
		{
			[Test]
			public void Conversions()
			{
				Assert.AreEqual (
					new CIExyY (0.5, 0.5, 100.0).ConvertTo<SRGB>(),
					new CIExyY (0.5, 0.5, 100.0).ConvertTo<CIEXYZ>().ConvertTo<SRGB>()
				);
			}

			[Test]
			public void CheckIsInsideColorSpace()
			{
				Assert.IsFalse (new CIExyY (0.05, 0.25, 100.0).IsInsideColorSpace ());
				Assert.IsFalse (new CIExyY (0.25, 0.75, 100.0).IsInsideColorSpace ());
				Assert.IsFalse (new CIExyY (0.30, 0.05, 100.0).IsInsideColorSpace ());
				Assert.IsFalse (new CIExyY (0.40, 0.10, 100.0).IsInsideColorSpace ());
				Assert.IsFalse (new CIExyY (0.50, 0.15, 100.0).IsInsideColorSpace ());
				Assert.IsFalse (new CIExyY (0.65, 0.20, 100.0).IsInsideColorSpace ());
				Assert.IsFalse (new CIExyY (0.80, 0.80, 100.0).IsInsideColorSpace ());

				Assert.IsTrue (new CIExyY (0.05, 0.30, 100.0).IsInsideColorSpace ());
				Assert.IsTrue (new CIExyY (0.10, 0.65, 100.0).IsInsideColorSpace ());
				Assert.IsTrue (new CIExyY (0.20, 0.10, 100.0).IsInsideColorSpace ());
				Assert.IsTrue (new CIExyY (0.65, 0.25, 100.0).IsInsideColorSpace ());
				Assert.IsTrue (new CIExyY (0.70, 0.25, 100.0).IsInsideColorSpace ());
			}

			[Test]
			public void CheckGetClosestSRGBPoint()
			{
				CIExyY red = new CIExyY (0.640074499456775, 0.329970510631693, 100.0);
				CIExyY green = new CIExyY (0.3, 0.6, 100.0);
				CIExyY blue = new CIExyY (0.150016622340426, 0.0600066489361702, 100.0);

				CIExyY closestR = new CIExyY (0.7, 0.3, 100.0).GetClosestSRGBPoint ();
				CIExyY closestG = new CIExyY (0.2, 0.7, 100.0).GetClosestSRGBPoint ();
				CIExyY closestB = new CIExyY (0.1, 0.05, 100.0).GetClosestSRGBPoint ();

				Assert.AreEqual (red.x, closestR.x, 0.001);
				Assert.AreEqual (red.y, closestR.y, 0.001);

				Assert.AreEqual (green.x, closestG.x, 0.001);
				Assert.AreEqual (green.y, closestG.y, 0.001);

				Assert.AreEqual (blue.x, closestB.x, 0.001);
				Assert.AreEqual (blue.y, closestB.y, 0.001);
			}
		}
	}
}
