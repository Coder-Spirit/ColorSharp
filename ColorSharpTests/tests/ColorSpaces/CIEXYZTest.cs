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
using Litipk.ColorSharp.ColorSpaces;


namespace Litipk.ColorSharpTests
{
	[TestFixture]
	public class CIEXYZTest
	{
		[Test]
		public void TestConversions()
		{
			Assert.AreEqual (
				new CIEXYZ (50.0, 60.0, 30.0).ConvertTo<CIExyY> (),
				new CIEXYZ (50.0, 60.0, 30.0).ConvertTo<SRGB> ().ConvertTo<CIExyY> ()
			);

			Assert.AreEqual (
				new CIEXYZ (50.0, 60.0, 30.0).ConvertTo<SRGB> (),
				new CIEXYZ (50.0, 60.0, 30.0).ConvertTo<CIExyY> ().ConvertTo<SRGB> ()
			);
		}
	}
}
