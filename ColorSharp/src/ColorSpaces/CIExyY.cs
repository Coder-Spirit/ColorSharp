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


using System;
using System.Collections.Generic;
using Litipk.ColorSharp.MatchingFunctions;


namespace Litipk.ColorSharp
{
	namespace ColorSpaces
	{
		/**
		 * CIE 1931 (2º) xyY Color Space.
		 */
		public class CIExyY : AConvertibleColor
		{
			#region readonly properties

			public readonly double x, y, Y;

			#endregion


			#region static properties

			static double[] SharkfinX;
			static double[] SharkfinY;

			#endregion


			#region constructors

			static CIExyY()
			{
				var mfX = CIE1931XYZ5NmMatchingFunctionX.Instance.Amplitudes;
				var mfY = CIE1931XYZ5NmMatchingFunctionY.Instance.Amplitudes;
				var mfZ = CIE1931XYZ5NmMatchingFunctionZ.Instance.Amplitudes;

				var n = mfX.Length;

				SharkfinX = new double[n];
				SharkfinY = new double[n];

				for (int i = 0; i < n; i++) {
					var XYZ = mfX [i] + mfY [i] + mfZ [i];

					SharkfinX [i] = mfX [i] / XYZ;
					SharkfinY [i] = mfY [i] / XYZ;
				}
			}

			protected CIExyY(AConvertibleColor dataSource=null) : base(dataSource) {
				Conversors.Add (typeof(CIEXYZ), ToXYZ);
			}

			public CIExyY (double x, double y, double Y, AConvertibleColor dataSource=null) : this(dataSource)
			{
				this.x = x;
				this.y = y;
				this.Y = Y;
			}

			#endregion


			#region conversors

			public CIEXYZ ToXYZ (Dictionary<KeyValuePair<Type, Type>, object> strategies=null)
			{
				return new CIEXYZ (x*Y/y, Y, Y*(1.0 - x - y)/y, DataSource ?? this);
			}

			#endregion


			#region inherited methods

			public override bool IsInsideColorSpace()
			{
				var n = SharkfinX.Length;
				var ai = (SharkfinX [1] - x) * (SharkfinY [0] - y) - (SharkfinX [0] - x) * (SharkfinY [1] - y);

				if (Math.Abs (ai) <= double.Epsilon) {
					return true;
				}

				int aiSign = ai > 0 ? 1 : -1;

				for (int i = 1; i < n; i++) {
					ai = (SharkfinX [(i+1)%n] - x) * (SharkfinY [i] - y) - (SharkfinX [i] - x) * (SharkfinY [(i+1)%n] - y);

					if (Math.Abs (ai) <= double.Epsilon) {
						return true;
					}

					if (ai > 0 && aiSign == -1 || ai < 0 && aiSign == 1) {
						return false;
					}
				}

				return true;
			}

			#endregion
		}
	}
}
