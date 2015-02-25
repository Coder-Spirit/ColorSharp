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
		sealed public class CIE1931XYZ5Nm2DegX : RegularMatchingFunction
		{
			/**
			 * <summary>Component X of CIE's 1931 2º matching functions (5nm of precision)</summary>
			 */
			public static readonly CIE1931XYZ5Nm2DegX Instance = new CIE1931XYZ5Nm2DegX (
				360.0, new [] {
					0.000129900000,
					0.000232100000,
					0.000414900000,
					0.000741600000,
					0.001368000000,
					0.002236000000,
					0.004243000000,
					0.007650000000,
					0.014310000000,
					0.023190000000,
					0.043510000000,
					0.077630000000,
					0.134380000000,
					0.214770000000,
					0.283900000000,
					0.328500000000,
					0.348280000000,
					0.348060000000,
					0.336200000000,
					0.318700000000,
					0.290800000000,
					0.251100000000,
					0.195360000000,
					0.142100000000,
					0.095640000000,
					0.057950010000,
					0.032010000000,
					0.014700000000,
					0.004900000000,
					0.002400000000,
					0.009300000000,
					0.029100000000,
					0.063270000000,
					0.109600000000,
					0.165500000000,
					0.225749900000,
					0.290400000000,
					0.359700000000,
					0.433449900000,
					0.512050100000,
					0.594500000000,
					0.678400000000,
					0.762100000000,
					0.842500000000,
					0.916300000000,
					0.978600000000,
					1.026300000000,
					1.056700000000,
					1.062200000000,
					1.045600000000,
					1.002600000000,
					0.938400000000,
					0.854449900000,
					0.751400000000,
					0.642400000000,
					0.541900000000,
					0.447900000000,
					0.360800000000,
					0.283500000000,
					0.218700000000,
					0.164900000000,
					0.121200000000,
					0.087400000000,
					0.063600000000,
					0.046770000000,
					0.032900000000,
					0.022700000000,
					0.015840000000,
					0.011359160000,
					0.008110916000,
					0.005790346000,
					0.004109457000,
					0.002899327000,
					0.002049190000,
					0.001439971000,
					0.000999949300,
					0.000690078600,
					0.000476021300,
					0.000332301100,
					0.000234826100,
					0.000166150500,
					0.000117413000,
					0.000083075270,
					0.000058706520,
					0.000041509940,
					0.000029353260,
					0.000020673830,
					0.000014559770,
					0.000010253980,
					0.000007221456,
					0.000005085868,
					0.000003581652,
					0.000002522525,
					0.000001776509,
					0.000001251141
				}, 5
			);

			/**
			 * We must seal this 
			 */
			CIE1931XYZ5Nm2DegX (double minWaveLength, double[] amplitudes, double nmPerStep) : base(
				minWaveLength, amplitudes, nmPerStep
			) {}
		}
	}
}

