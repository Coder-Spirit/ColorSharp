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
		sealed public class CIE1931XYZ5NmMatchingFunctionY : RegularMatchingFunction
		{
			public static readonly CIE1931XYZ5NmMatchingFunctionY Instance = new CIE1931XYZ5NmMatchingFunctionY (
				380.0, new [] {
					3.9000000e-05,
					6.4000000e-05,
					1.2000000e-04,
					2.1700000e-04,
					3.9600000e-04,
					6.4000000e-04,
					1.2100000e-03,
					2.1800000e-03,
					4.0000000e-03,
					7.3000000e-03,
					1.1600000e-02,
					1.6840000e-02,
					2.3000000e-02,
					2.9800000e-02,
					3.8000000e-02,
					4.8000000e-02,
					6.0000000e-02,
					7.3900000e-02,
					9.0980000e-02,
					1.1260000e-01,
					1.3902000e-01,
					1.6930000e-01,
					2.0802000e-01,
					2.5860000e-01,
					3.2300000e-01,
					4.0730000e-01,
					5.0300000e-01,
					6.0820000e-01,
					7.1000000e-01,
					7.9320000e-01,
					8.6200000e-01,
					9.1485010e-01,
					9.5400000e-01,
					9.8030000e-01,
					9.9495010e-01,
					1.0000000e+00,
					9.9500000e-01,
					9.7860000e-01,
					9.5200000e-01,
					9.1540000e-01,
					8.7000000e-01,
					8.1630000e-01,
					7.5700000e-01,
					6.9490000e-01,
					6.3100000e-01,
					5.6680000e-01,
					5.0300000e-01,
					4.4120000e-01,
					3.8100000e-01,
					3.2100000e-01,
					2.6500000e-01,
					2.1700000e-01,
					1.7500000e-01,
					1.3820000e-01,
					1.0700000e-01,
					8.1600000e-02,
					6.1000000e-02,
					4.4580000e-02,
					3.2000000e-02,
					2.3200000e-02,
					1.7000000e-02,
					1.1920000e-02,
					8.2100000e-03,
					5.7230000e-03,
					4.1020000e-03,
					2.9290000e-03,
					2.0910000e-03,
					1.4840000e-03,
					1.0470000e-03,
					7.4000000e-04,
					5.2000000e-04,
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
			CIE1931XYZ5NmMatchingFunctionY (double minWaveLength, double[] amplitudes, double nmPerStep) : base(
				minWaveLength, amplitudes, nmPerStep
			) {}
		}
	}
}


