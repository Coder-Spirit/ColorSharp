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


namespace Litipk.ColorSharp
{
	namespace MatchingFunctions
	{
		/**
		 * This class will be a singleton
		 */
		sealed public class CIE1931XYZ5NmMatchingFunctionZ : RegularMatchingFunction
		{
			public static readonly CIE1931XYZ5NmMatchingFunctionZ Instance = new CIE1931XYZ5NmMatchingFunctionZ (
				380.0, new [] {
					6.4500010e-03,
					1.0549990e-02,
					2.0050010e-02,
					3.6210000e-02,
					6.7850010e-02,
					1.1020000e-01,
					2.0740000e-01,
					3.7130000e-01,
					6.4560000e-01,
					1.0390501e+00,
					1.3856000e+00,
					1.6229600e+00,
					1.7470600e+00,
					1.7826000e+00,
					1.7721100e+00,
					1.7441000e+00,
					1.6692000e+00,
					1.5281000e+00,
					1.2876400e+00,
					1.0419000e+00,
					8.1295010e-01,
					6.1620000e-01,
					4.6518000e-01,
					3.5330000e-01,
					2.7200000e-01,
					2.1230000e-01,
					1.5820000e-01,
					1.1170000e-01,
					7.8249990e-02,
					5.7250010e-02,
					4.2160000e-02,
					2.9840000e-02,
					2.0300000e-02,
					1.3400000e-02,
					8.7499990e-03,
					5.7499990e-03,
					3.9000000e-03,
					2.7499990e-03,
					2.1000000e-03,
					1.8000000e-03,
					1.6500010e-03,
					1.4000000e-03,
					1.1000000e-03,
					1.0000000e-03,
					8.0000000e-04,
					6.0000000e-04,
					3.4000000e-04,
					2.4000000e-04,
					1.9000000e-04,
					1.0000000e-04,
					4.9999990e-05,
					3.0000000e-05,
					2.0000000e-05,
					1.0000000e-05,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
					0.0000000e+00,
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
			CIE1931XYZ5NmMatchingFunctionZ (double minWaveLength, double[] amplitudes, double nmPerStep) : base(
				minWaveLength, amplitudes, nmPerStep
			) {}
		}
	}
}



