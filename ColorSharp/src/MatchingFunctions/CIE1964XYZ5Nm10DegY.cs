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
		sealed public class CIE1964XYZ5Nm10DegY : RegularMatchingFunction
		{
			/**
			 * <summary>Component Y of CIE's 1964 10º matching functions (5nm of precision)</summary>
			 */
			public static readonly CIE1964XYZ5Nm10DegY Instance = new CIE1964XYZ5Nm10DegY (
				360.0, new [] {
					0.000000013398,
					0.000000100650,
					0.000000651100,
					0.000003625000,
					0.000017364000,
					0.000071560000,
					0.000253400000,
					0.000768500000,
					0.002004400000,
					0.004509000000,
					0.008756000000,
					0.014456000000,
					0.021391000000,
					0.029497000000,
					0.038676000000,
					0.049602000000,
					0.062077000000,
					0.074704000000,
					0.089456000000,
					0.106256000000,
					0.128201000000,
					0.152761000000,
					0.185190000000,
					0.219940000000,
					0.253589000000,
					0.297665000000,
					0.339133000000,
					0.395379000000,
					0.460777000000,
					0.531360000000,
					0.606741000000,
					0.685660000000,
					0.761757000000,
					0.823330000000,
					0.875211000000,
					0.923810000000,
					0.961988000000,
					0.982200000000,
					0.991761000000,
					0.999110000000,
					0.997340000000,
					0.982380000000,
					0.955552000000,
					0.915175000000,
					0.868934000000,
					0.825623000000,
					0.777405000000,
					0.720353000000,
					0.658341000000,
					0.593878000000,
					0.527963000000,
					0.461834000000,
					0.398057000000,
					0.339554000000,
					0.283493000000,
					0.228254000000,
					0.179828000000,
					0.140211000000,
					0.107633000000,
					0.081187000000,
					0.060281000000,
					0.044096000000,
					0.031800400000,
					0.022601700000,
					0.015905100000,
					0.011130300000,
					0.007748800000,
					0.005375100000,
					0.003717740000,
					0.002564560000,
					0.001768470000,
					0.001222390000,
					0.000846190000,
					0.000586440000,
					0.000407410000,
					0.000284041000,
					0.000198730000,
					0.000139550000,
					0.000098428000,
					0.000069819000,
					0.000049737000,
					0.000035540500,
					0.000025486000,
					0.000018338400,
					0.000013249000,
					0.000009619600,
					0.000007012800,
					0.000005129800,
					0.000003764730,
					0.000002770810,
					0.000002046130,
					0.000001516770,
					0.000001128090,
					0.000000842160,
					0.000000629700
				}, 1
			);

			CIE1964XYZ5Nm10DegY (double minWaveLength, double[] amplitudes, double nmPerStep) : base(
				minWaveLength, amplitudes, nmPerStep
			) {}
		}
	}
}

