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
using Litipk.ColorSharp.InternalUtils;


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

			static xyYPoint[] Sharkfin;

			#endregion


			#region constructors

			static CIExyY()
			{
				var mfX = CIE1931XYZ5NmMatchingFunctionX.Instance.Amplitudes;
				var mfY = CIE1931XYZ5NmMatchingFunctionY.Instance.Amplitudes;
				var mfZ = CIE1931XYZ5NmMatchingFunctionZ.Instance.Amplitudes;

				var n = mfX.Length;

				Sharkfin = new xyYPoint[n];

				for (int i = 0; i < n; i++) {
					var XYZ = mfX [i] + mfY [i] + mfZ [i];
					Sharkfin [i] = new xyYPoint { x = mfX [i] / XYZ, y = mfY [i] / XYZ };
				}
			}

			public CIExyY() {
				Conversors.Add (typeof(CIEXYZ), ToXYZ);
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
				// Fast checks
				if (x >= 0.75 || y >= 0.85 || y > 1.0 - x || y < (x-0.25)*0.5) return false;

				List<xyYPoint> points = new List<xyYPoint> (Sharkfin);
				points.Add (new xyYPoint{x=x, y=y});

				xyYPoint[] convexHull = findConvexHull (points);

				for (int i = 0; i < convexHull.Length; i++) {
					if (convexHull [i].x == x && convexHull [i].y == y)
						return false;
				}

				return true;
			}

			/**
			 * Monotone Chain algorithm
			 */
			static xyYPoint[] findConvexHull(List<xyYPoint> points)
			{
				int n = points.Count, k = 0;
				xyYPoint[] tmpHull = new xyYPoint[2 * n];

				points.Sort(new xyYPointComparer());

				// Build lower hull
				for (int i = 0; i < n; i++) {
					while (k >= 2 && cross(tmpHull[k - 2], tmpHull[k - 1], points[i]) <= 0)
						k--;
					tmpHull[k++] = points[i];
				}

				// Build upper hull
				for (int i = n - 2, t = k + 1; i >= 0; i--) {
					while (k >= t && cross(tmpHull[k - 2], tmpHull[k - 1], points[i]) <= 0)
						k--;
					tmpHull[k++] = points[i];
				}

				tmpHull [k - 1].x = -1000.0; // remove repetition

				return tmpHull;
			}

			static double cross(xyYPoint O, xyYPoint A, xyYPoint B) {
				return (A.x - O.x) * (B.y - O.y) - (A.y - O.y) * (B.x - O.x);
			}

			public override bool Equals(Object obj)
			{
				CIExyY xyYObj = obj as CIExyY; 

				if (xyYObj == null || GetHashCode () != obj.GetHashCode ())
					return false;

				return (
					Math.Abs (x - xyYObj.x) <= double.Epsilon &&
					Math.Abs (y - xyYObj.y) <= double.Epsilon &&
					Math.Abs (Y - xyYObj.Y) <= double.Epsilon
				);
			}
			public override int GetHashCode ()
			{
				int hash = 19;

				hash = hash * 31 + x.GetHashCode ();
				hash = hash * 31 + y.GetHashCode ();
				hash = hash * 31 + Y.GetHashCode ();

				return hash;
			}

			#endregion
		}
	}
}
