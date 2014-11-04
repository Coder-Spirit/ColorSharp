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


namespace Litipk.ColorSharp
{
	namespace MatchingFunctions
	{
		/**
		 * This class will be a singleton
		 */
		sealed public class CIE1931XYZ5NmMatchingFunctionX : RegularMatchingFunction
		{
			public static readonly CIE1931XYZ5NmMatchingFunctionX Instance = new CIE1931XYZ5NmMatchingFunctionX (
				380.0, new [] {
					1.3680000e-03,
					2.2360000e-03,
					4.2430000e-03,
					7.6500000e-03,
					1.4310000e-02,
					2.3190000e-02,
					4.3510000e-02,
					7.7630000e-02,
					1.3438000e-01,
					2.1477000e-01,
					2.8390000e-01,
					3.2850000e-01,
					3.4828000e-01,
					3.4806000e-01,
					3.3620000e-01,
					3.1870000e-01,
					2.9080000e-01,
					2.5110000e-01,
					1.9536000e-01,
					1.4210000e-01,
					9.5640000e-02,
					5.7950010e-02,
					3.2010000e-02,
					1.4700000e-02,
					4.9000000e-03,
					2.4000000e-03,
					9.3000000e-03,
					2.9100000e-02,
					6.3270000e-02,
					1.0960000e-01,
					1.6550000e-01,
					2.2574990e-01,
					2.9040000e-01,
					3.5970000e-01,
					4.3344990e-01,
					5.1205010e-01,
					5.9450000e-01,
					6.7840000e-01,
					7.6210000e-01,
					8.4250000e-01,
					9.1630000e-01,
					9.7860000e-01,
					1.0263000e+00,
					1.0567000e+00,
					1.0622000e+00,
					1.0456000e+00,
					1.0026000e+00,
					9.3840000e-01,
					8.5444990e-01,
					7.5140000e-01,
					6.4240000e-01,
					5.4190000e-01,
					4.4790000e-01,
					3.6080000e-01,
					2.8350000e-01,
					2.1870000e-01,
					1.6490000e-01,
					1.2120000e-01,
					8.7400000e-02,
					6.3600000e-02,
					4.6770000e-02,
					3.2900000e-02,
					2.2700000e-02,
					1.5840000e-02,
					1.1359160e-02,
					8.1109160e-03,
					5.7903460e-03,
					4.1094570e-03,
					2.8993270e-03,
					2.0491900e-03,
					1.4399710e-03,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00
				}, 5
			);

			/**
			 * We must seal this 
			 */
			CIE1931XYZ5NmMatchingFunctionX (double minWaveLength, double[] amplitudes, double nmPerStep) : base(
				minWaveLength, amplitudes, nmPerStep
			) {}
		}
	}
}

