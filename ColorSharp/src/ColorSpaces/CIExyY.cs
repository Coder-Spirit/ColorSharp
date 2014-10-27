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

			static readonly xyYPoint nullP = new xyYPoint { x = -1.0, y = -1.0 };
			static readonly xyYPoint RsRGB = new xyYPoint { x = 0.640074499456775, y = 0.329970510631693 };
			static readonly xyYPoint GsRGB = new xyYPoint { x = 0.3, y = 0.6 };
			static readonly xyYPoint BsRGB = new xyYPoint { x = 0.150016622340426, y = 0.0600066489361702 };

			#endregion


			#region static properties

			static xyYPoint[] Sharkfin;
			static xyYPoint[] SortedSharkfin;

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

				// Used to speedup the convex hull algorithm
				SortedSharkfin = (xyYPoint[])Sharkfin.Clone();
				Array.Sort (SortedSharkfin, new xyYPointComparer());
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
				if (y > 1.0 - x || y < (x-0.25)*0.5 || y < 0.4 - x*4 || y >= 0.85) return false;

				xyYPoint[] points = new xyYPoint[SortedSharkfin.Length + 1];
				Array.Copy (SortedSharkfin, 0, points, 0, SortedSharkfin.Length);
				points[SortedSharkfin.Length] = new xyYPoint{x=x, y=y};

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
			static xyYPoint[] findConvexHull(xyYPoint[] points)
			{
				int n = points.Length, k = 0;
				xyYPoint[] tmpHull = new xyYPoint[2 * n];

				Array.Sort (points, new xyYPointComparer ());

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


			#region exclusive methods

			/**
			 * Preconditions:
			 *   - It's a valid CIExyY value.
			 *   - It's not in the SRGB space.
			 */
			public CIExyY GetClosestSRGBPoint()
			{
				xyYPoint closerPoint = RsRGB;
				double closerDistance = Math.Pow (x - RsRGB.x, 2.0) + Math.Pow (y - RsRGB.y, 2.0);

				var tmpDistance = Math.Pow (x - GsRGB.x, 2.0) + Math.Pow (y - GsRGB.y, 2.0);
				if (tmpDistance < closerDistance) {
					closerPoint = GsRGB;
					closerDistance = tmpDistance;
				}

				tmpDistance = Math.Pow (x - BsRGB.x, 2.0) + Math.Pow (y - BsRGB.y, 2.0);
				if (tmpDistance < closerDistance) {
					closerPoint = BsRGB;
					closerDistance = tmpDistance;
				}

				xyYPoint cPoint = new xyYPoint { x = x, y = y };

				xyYPoint tmpPoint = PointProjection (RsRGB, GsRGB, cPoint);
				if (!tmpPoint.Equals(nullP)) {
					tmpDistance = Math.Pow (x - tmpPoint.x, 2) + Math.Pow (y - tmpPoint.y, 2);
					if (tmpDistance < closerDistance) {
						closerPoint = tmpPoint;
						closerDistance = tmpDistance;
					}
				}

				tmpPoint = PointProjection (GsRGB, BsRGB, cPoint);
				if (!tmpPoint.Equals(nullP)) {
					tmpDistance = Math.Pow (x - tmpPoint.x, 2) + Math.Pow (y - tmpPoint.y, 2);
					if (tmpDistance < closerDistance) {
						closerPoint = tmpPoint;
						closerDistance = tmpDistance;
					}
				}

				tmpPoint = PointProjection (BsRGB, RsRGB, cPoint);
				if (!tmpPoint.Equals(nullP)) {
					tmpDistance = Math.Pow (x - tmpPoint.x, 2) + Math.Pow (y - tmpPoint.y, 2);
					if (tmpDistance < closerDistance) {
						closerPoint = tmpPoint;
					}
				}

				return new CIExyY (closerPoint.x, closerPoint.y, Y);
			}

			xyYPoint PointProjection(xyYPoint pt1, xyYPoint pt2, xyYPoint pt)
			{
				var U = (
					((pt.y - pt1.y) * (pt2.y - pt1.y)) + ((pt.x - pt1.x) * (pt2.x - pt1.x)) /
					(Math.Pow(pt2.y - pt1.y, 2) + Math.Pow(pt2.x - pt1.x, 2))
				);

				var r = new xyYPoint {
					x = pt1.x + (U * (pt2.x - pt1.x)),
					y = pt1.y + (U * (pt2.y - pt1.y))
				};

				var isValid = (
					r.y >= Math.Min(pt1.y, pt2.y) && r.y <= Math.Max(pt1.y, pt2.y) &&
					r.x >= Math.Min(pt1.x, pt2.x) && r.x <= Math.Max(pt1.x, pt2.x)
				);

				return isValid ? r : nullP;
			}

			#endregion
		}
	}
}
