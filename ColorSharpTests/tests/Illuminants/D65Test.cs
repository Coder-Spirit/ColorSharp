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
using Litipk.ColorSharp.Illuminants;


namespace Litipk.ColorSharpTests
{
	[TestFixture]
	public class D65Test
	{
		[Test]
		public void TestRGBValue()
		{
			var tmp = CIE_D65.spectrum_Sample.ToCIEXYZ ();
			//System.Console.WriteLine ("(X="+tmp.X+", "+"Y="+tmp.Y+", Z="+tmp.Z+")");

			var xyzWhitePoint = CIE_D65.XYZ_Sample.ToSRGB ();

			// Because there are a lot of transformations, we can't ensure a very little delta
			Assert.AreEqual (1.0, xyzWhitePoint.R, 0.0001);
			Assert.AreEqual (1.0, xyzWhitePoint.G, 0.0001);
			Assert.AreEqual (1.0, xyzWhitePoint.B, 0.0010);


			var specWhitePoint = CIE_D65.spectrum_Sample.ToSRGB ();

			// Because there are a lot of transformations, we can't ensure a very little delta
			Assert.AreEqual (1.0, specWhitePoint.R, 0.0002);
			Assert.AreEqual (1.0, specWhitePoint.G, 0.0001);
			Assert.AreEqual (1.0, specWhitePoint.B, 0.0010);
		}
	}
}
