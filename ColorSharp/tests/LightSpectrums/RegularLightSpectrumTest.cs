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
	namespace LightSpectrums
	{
		[TestFixture]
		public class RegularLightSpectrumTest
		{
			[Test]
			public void TestEvaluateAt()
			{
				var ls = new RegularLightSpectrum (100.0, 200.0, new [] { 20.0, 50.0, 20.0 });

				Assert.AreEqual (20.0, ls.EvaluateAt (100.0), 0.00001);
				Assert.AreEqual (35.0, ls.EvaluateAt (125.0), 0.00001);
				Assert.AreEqual (50.0, ls.EvaluateAt (150.0), 0.00001);
				Assert.AreEqual (35.0, ls.EvaluateAt (175.0), 0.00001);
				Assert.AreEqual (20.0, ls.EvaluateAt (200.0), 0.00001);
			}
		}
	}
}
