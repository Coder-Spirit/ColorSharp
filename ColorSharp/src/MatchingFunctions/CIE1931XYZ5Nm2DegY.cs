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
		sealed public class CIE1931XYZ5Nm2DegY : RegularMatchingFunction
		{
			/**
			 * <summary>Component Y of CIE's 1931 2º matching functions (5nm of precision)</summary>
			 */
			public static readonly CIE1931XYZ5Nm2DegY Instance = new CIE1931XYZ5Nm2DegY (
				360.0, new [] {
					0.000003917000,
					0.000006965000,
					0.000012390000,
					0.000022020000,
					0.000039000000,
					0.000064000000,
					0.000120000000,
					0.000217000000,
					0.000396000000,
					0.000640000000,
					0.001210000000,
					0.002180000000,
					0.004000000000,
					0.007300000000,
					0.011600000000,
					0.016840000000,
					0.023000000000,
					0.029800000000,
					0.038000000000,
					0.048000000000,
					0.060000000000,
					0.073900000000,
					0.090980000000,
					0.112600000000,
					0.139020000000,
					0.169300000000,
					0.208020000000,
					0.258600000000,
					0.323000000000,
					0.407300000000,
					0.503000000000,
					0.608200000000,
					0.710000000000,
					0.793200000000,
					0.862000000000,
					0.914850100000,
					0.954000000000,
					0.980300000000,
					0.994950100000,
					1.000000000000,
					0.995000000000,
					0.978600000000,
					0.952000000000,
					0.915400000000,
					0.870000000000,
					0.816300000000,
					0.757000000000,
					0.694900000000,
					0.631000000000,
					0.566800000000,
					0.503000000000,
					0.441200000000,
					0.381000000000,
					0.321000000000,
					0.265000000000,
					0.217000000000,
					0.175000000000,
					0.138200000000,
					0.107000000000,
					0.081600000000,
					0.061000000000,
					0.044580000000,
					0.032000000000,
					0.023200000000,
					0.017000000000,
					0.011920000000,
					0.008210000000,
					0.005723000000,
					0.004102000000,
					0.002929000000,
					0.002091000000,
					0.001484000000,
					0.001047000000,
					0.000740000000,
					0.000520000000,
					0.000361100000,
					0.000249200000,
					0.000171900000,
					0.000120000000,
					0.000084800000,
					0.000060000000,
					0.000042400000,
					0.000030000000,
					0.000021200000,
					0.000014990000,
					0.000010600000,
					0.000007465700,
					0.000005257800,
					0.000003702900,
					0.000002607800,
					0.000001836600,
					0.000001293400,
					0.000000910930,
					0.000000641530,
					0.000000451810
				}, 5
			);

			/**
			 * We must seal this 
			 */
			CIE1931XYZ5Nm2DegY (double minWaveLength, double[] amplitudes, double nmPerStep) : base(
				minWaveLength, amplitudes, nmPerStep
			) {}
		}
	}
}


