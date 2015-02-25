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
		sealed public class CIE1964XYZ5Nm10DegZ : RegularMatchingFunction
		{
			/**
			 * <summary>Component Z of CIE's 1964 10º matching functions (5nm of precision)</summary>
			 */
			public static readonly CIE1964XYZ5Nm10DegZ Instance = new CIE1964XYZ5Nm10DegZ (
				360.0, new [] {
					0.000000535027,
					0.000004028300,
					0.000026143700,
					0.000146220000,
					0.000704776000,
					0.002927800000,
					0.010482200000,
					0.032344000000,
					0.086010900000,
					0.197120000000,
					0.389366000000,
					0.656760000000,
					0.972542000000,
					1.282500000000,
					1.553480000000,
					1.798500000000,
					1.967280000000,
					2.027300000000,
					1.994800000000,
					1.900700000000,
					1.745370000000,
					1.554900000000,
					1.317560000000,
					1.030200000000,
					0.772125000000,
					0.570060000000,
					0.415254000000,
					0.302356000000,
					0.218502000000,
					0.159249000000,
					0.112044000000,
					0.082248000000,
					0.060709000000,
					0.043050000000,
					0.030451000000,
					0.020584000000,
					0.013676000000,
					0.007918000000,
					0.003988000000,
					0.001091000000,
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
				}, 1
			);

			CIE1964XYZ5Nm10DegZ (double minWaveLength, double[] amplitudes, double nmPerStep) : base(
				minWaveLength, amplitudes, nmPerStep
			) {}
		}
	}
}

