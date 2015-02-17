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
using Litipk.ColorSharp.LightSpectrums;


namespace Litipk.ColorSharpTests
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

		[Test]
		public void TestEquality()
		{
			var ls1 = new RegularLightSpectrum (100.0, 200.0, new [] { 20.0, 50.0, 20.0 });
			var ls2 = new RegularLightSpectrum (100.0, 200.0, new [] { 20.0, 50.0, 20.0 });

			Assert.AreEqual (ls1, ls2);
			Assert.AreEqual (ls2, ls1);
		}

		[Test]
		public void TestToCIEXYZ()
		{
			var ls = new RegularLightSpectrum (380.0, 780.0, new [] {
				2.2110000e-02,
				2.5760000e-02,
				3.0300000e-02,
				4.0200000e-02,
				5.0230000e-02,
				5.9630000e-02,
				6.8540000e-02,
				7.7220000e-02,
				8.4210000e-02,
				9.1320000e-02,
				9.9740000e-02,
				1.0850000e-01,
				1.1620000e-01,
				1.2530000e-01,
				1.3470000e-01,
				1.4500000e-01,
				1.5570000e-01,
				1.6660000e-01,
				1.7700000e-01,
				1.8900000e-01,
				1.9950000e-01,
				2.1080000e-01,
				2.2300000e-01,
				2.3490000e-01,
				2.4780000e-01,
				2.6110000e-01,
				2.7360000e-01,
				2.8730000e-01,
				3.0080000e-01,
				3.1480000e-01,
				3.2860000e-01,
				3.4290000e-01,
				3.5820000e-01,
				3.7330000e-01,
				3.8830000e-01,
				4.0400000e-01,
				4.2040000e-01,
				4.3650000e-01,
				4.5230000e-01,
				4.6830000e-01,
				4.8420000e-01,
				5.0040000e-01,
				5.1640000e-01,
				5.3250000e-01,
				5.4880000e-01,
				5.6470000e-01,
				5.8080000e-01,
				5.9670000e-01,
				6.1190000e-01,
				6.2760000e-01,
				6.4250000e-01,
				6.5780000e-01,
				6.7330000e-01,
				6.8830000e-01,
				7.0310000e-01,
				7.1820000e-01,
				7.3320000e-01,
				7.4780000e-01,
				7.6410000e-01,
				7.8110000e-01,
				7.9830000e-01,
				8.1280000e-01,
				8.2790000e-01,
				8.4070000e-01,
				8.5280000e-01,
				8.6520000e-01,
				8.8080000e-01,
				8.9220000e-01,
				9.0500000e-01,
				9.1950000e-01,
				9.3040000e-01,
				9.3820000e-01,
				9.5200000e-01,
				9.6700000e-01,
				9.7670000e-01,
				9.8540000e-01,
				1.0000000e+00,
				9.6700000e-01,
				9.7670000e-01,
				9.8540000e-01,
				1.0000000e+00
			});

			var XYZ = ls.ToCIEXYZ ();
			var xyz = XYZ.X + XYZ.Y + XYZ.Z;

			Assert.AreEqual (0.451, XYZ.X / xyz, 0.001);
			Assert.AreEqual (0.408, XYZ.Y / xyz, 0.001);
			Assert.AreEqual (0.141, XYZ.Z / xyz, 0.001);
		}
	}
}
