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
		sealed public class CIE1964XYZ5Nm10DegX : RegularMatchingFunction
		{
			/**
			 * <summary>Component X of CIE's 1964 10º matching functions (5nm of precision)</summary>
			 */
			public static readonly CIE1964XYZ5Nm10DegX Instance = new CIE1964XYZ5Nm10DegX (
				360.0, new [] {
					0.000000122200,
					0.000000919270,
					0.000005958600,
					0.000033266000,
					0.000159952000,
					0.000662440000,
					0.002361600000,
					0.007242300000,
					0.019109700000,
					0.043400000000,
					0.084736000000,
					0.140638000000,
					0.204492000000,
					0.264737000000,
					0.314679000000,
					0.357719000000,
					0.383734000000,
					0.386726000000,
					0.370702000000,
					0.342957000000,
					0.302273000000,
					0.254085000000,
					0.195618000000,
					0.132349000000,
					0.080507000000,
					0.041072000000,
					0.016172000000,
					0.005132000000,
					0.003816000000,
					0.015444000000,
					0.037465000000,
					0.071358000000,
					0.117749000000,
					0.172953000000,
					0.236491000000,
					0.304213000000,
					0.376772000000,
					0.451584000000,
					0.529826000000,
					0.616053000000,
					0.705224000000,
					0.793832000000,
					0.878655000000,
					0.951162000000,
					1.014160000000,
					1.074300000000,
					1.118520000000,
					1.134300000000,
					1.123990000000,
					1.089100000000,
					1.030480000000,
					0.950740000000,
					0.856297000000,
					0.754930000000,
					0.647467000000,
					0.535110000000,
					0.431567000000,
					0.343690000000,
					0.268329000000,
					0.204300000000,
					0.152568000000,
					0.112210000000,
					0.081260600000,
					0.057930000000,
					0.040850800000,
					0.028623000000,
					0.019941300000,
					0.013842000000,
					0.009576880000,
					0.006605200000,
					0.004552630000,
					0.003144700000,
					0.002174960000,
					0.001505700000,
					0.001044760000,
					0.000727450000,
					0.000508258000,
					0.000356380000,
					0.000250969000,
					0.000177730000,
					0.000126390000,
					0.000090151000,
					0.000064525800,
					0.000046339000,
					0.000033411700,
					0.000024209000,
					0.000017611500,
					0.000012855000,
					0.000009413630,
					0.000006913000,
					0.000005093470,
					0.000003767100,
					0.000002795310,
					0.000002082000,
					0.000001553140
				}, 1
			);

			CIE1964XYZ5Nm10DegX (double minWaveLength, double[] amplitudes, double nmPerStep) : base(
				minWaveLength, amplitudes, nmPerStep
			) {}
		}
	}
}

