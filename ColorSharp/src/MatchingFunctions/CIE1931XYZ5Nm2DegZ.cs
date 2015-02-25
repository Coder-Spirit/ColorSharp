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
		sealed public class CIE1931XYZ5Nm2DegZ : RegularMatchingFunction
		{
			/**
			 * <summary>Component Z of CIE's 1931 2º matching functions (5nm of precision)</summary>
			 */
			public static readonly CIE1931XYZ5Nm2DegZ Instance = new CIE1931XYZ5Nm2DegZ (
				360.0, new [] {
					0.000606100000,
					0.001086000000,
					0.001946000000,
					0.003486000000,
					0.006450001000,
					0.010549990000,
					0.020050010000,
					0.036210000000,
					0.067850010000,
					0.110200000000,
					0.207400000000,
					0.371300000000,
					0.645600000000,
					1.039050100000,
					1.385600000000,
					1.622960000000,
					1.747060000000,
					1.782600000000,
					1.772110000000,
					1.744100000000,
					1.669200000000,
					1.528100000000,
					1.287640000000,
					1.041900000000,
					0.812950100000,
					0.616200000000,
					0.465180000000,
					0.353300000000,
					0.272000000000,
					0.212300000000,
					0.158200000000,
					0.111700000000,
					0.078249990000,
					0.057250010000,
					0.042160000000,
					0.029840000000,
					0.020300000000,
					0.013400000000,
					0.008749999000,
					0.005749999000,
					0.003900000000,
					0.002749999000,
					0.002100000000,
					0.001800000000,
					0.001650001000,
					0.001400000000,
					0.001100000000,
					0.001000000000,
					0.000800000000,
					0.000600000000,
					0.000340000000,
					0.000240000000,
					0.000190000000,
					0.000100000000,
					0.000049999990,
					0.000030000000,
					0.000020000000,
					0.000010000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000,
					0.000000000000
				}, 5
			);

			/**
			 * We must seal this 
			 */
			CIE1931XYZ5Nm2DegZ (double minWaveLength, double[] amplitudes, double nmPerStep) : base(
				minWaveLength, amplitudes, nmPerStep
			) {}
		}
	}
}



