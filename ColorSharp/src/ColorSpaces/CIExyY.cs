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


using System;
using System.Collections.Generic;
using Litipk.ColorSharp.MatchingFunctions;
using Litipk.ColorSharp.InternalUtils;


namespace Litipk.ColorSharp
{
	namespace ColorSpaces
	{
		/**
		 * <summary>CIE's 1931 (2º) xyY Color Space.</summary>
		 */
		public sealed class CIExyY : AConvertibleColor
		{

			#region properties

			#region static properties
			// Light wavelengths coordinates in the CIE's 1931 xyY color sapce.
			static xyYPoint[] Sharkfin1Nm;
			static xyYPoint[] Sharkfin5Nm;

			// Sharkfin transformation with performance purposes.
			static xyYPoint[] SortedSharkfin1Nm;
			static xyYPoint[] SortedSharkfin5Nm;
			#endregion

			#region readonly properties
			/**
			 * <value>x component of the CIE's 1931 xyY color space : X/(X+Y+Z) .</value>
			 */
			public readonly double x;

			/**
			 * <value>y component of the CIE's 1931 xyY color space : Y/(X+Y+Z) .</value>
			 */
			public readonly double y;

			/**
			 * <value>Y component of the CIE's 1931 xyY color space. The same Y from CIE's 1931 XYZ color space.</value>
			 */
			public readonly double Y;
			#endregion

			#endregion


			#region constructors

			/**
			 * Static members initialization
			 */
			static CIExyY()
			{
				computeSharkfin (
					CIE1931XYZ5Nm2DegX.Instance.Amplitudes,
					CIE1931XYZ5Nm2DegY.Instance.Amplitudes,
					CIE1931XYZ5Nm2DegZ.Instance.Amplitudes,
					ref Sharkfin5Nm,
					ref SortedSharkfin5Nm
				);

				computeSharkfin (
					CIE1931XYZ1Nm2DegX.Instance.Amplitudes,
					CIE1931XYZ1Nm2DegY.Instance.Amplitudes,
					CIE1931XYZ1Nm2DegZ.Instance.Amplitudes,
					ref Sharkfin1Nm,
					ref SortedSharkfin1Nm
				);
			}

			/**
			 * Computes the 'sharkin' points. 
			 */
			static void computeSharkfin(IList<double> mfX, IList<double> mfY, IList<double> mfZ, ref xyYPoint[] Sharkfin, ref xyYPoint[] SortedSharkfin)
			{
				var n = mfX.Count;

				Sharkfin = new xyYPoint[n];

				for (int i = 0; i < n; i++) {
					var XYZ = mfX [i] + mfY [i] + mfZ [i];

					if (Math.Abs (XYZ) < double.Epsilon) {
						Sharkfin [i] = new xyYPoint { x = 0.0, y = 0.0 };
					} else {
						Sharkfin [i] = new xyYPoint { x = mfX [i] / XYZ, y = mfY [i] / XYZ };
					}
				}

				// Used to speedup the convex hull algorithm
				SortedSharkfin = (xyYPoint[])Sharkfin.Clone();

				Array.Sort (SortedSharkfin, new xyYPointComparer());
			}

			/**
			 * <summary>Creates a new color sample in the CIE's 1931 xyY color space</summary>
			 * <param name="x">CIE's 1931 xyY x coordinate</param>
			 * <param name="y">CIE's 1931 xyY y coordinate</param>
			 * <param name="Y">Lightness parameter</param>
			 * <param name="dataSource">If you aren't working with ColorSharp internals, don't use this parameter</param>
			 */
			public CIExyY (double x, double y, double Y, AConvertibleColor dataSource=null) : base(dataSource)
			{
				this.x = x;
				this.y = y;
				this.Y = Y;
			}

			#endregion


			#region AConvertibleColor methods

			/**
			 * <inheritdoc />
			 */
			public override bool IsInsideColorSpace(bool highPrecision = false)
			{
				// Fast checks
				if (y > 1.0 - x || y < (x - 0.25) * 0.5 || y < 0.4 - x * 4 || y >= 0.85)
					return false;

				xyYPoint[] points;

				if (highPrecision) {
					points = new xyYPoint[SortedSharkfin1Nm.Length + 1];
					Array.Copy (SortedSharkfin1Nm, 0, points, 0, SortedSharkfin1Nm.Length);
					points [SortedSharkfin1Nm.Length] = new xyYPoint{ x = x, y = y };
				} else {
					points = new xyYPoint[SortedSharkfin5Nm.Length + 1];
					Array.Copy (SortedSharkfin5Nm, 0, points, 0, SortedSharkfin5Nm.Length);
					points [SortedSharkfin5Nm.Length] = new xyYPoint{ x = x, y = y };
				}

				xyYPoint[] convexHull = findConvexHull (points);

				for (int i = 0; i < convexHull.Length; i++) {
					if (convexHull [i].x == x && convexHull [i].y == y)
						return false;
				}

				return true;
			}

			/**
			 * <inheritdoc />
			 */
			public override CIEXYZ ToCIEXYZ ()
			{
				return (DataSource as CIEXYZ) ?? new CIEXYZ (x*Y/y, Y, Y*(1.0 - x - y)/y, DataSource ?? this);
			}

			/**
			 * <inheritdoc />
			 */
			public override CIExyY ToCIExyY ()
			{
				return this;
			}

			/**
			 * <inheritdoc />
			 */
			public override CIE1960 ToCIEUCS ()
			{
				if (DataSource is CIE1960) {
					return DataSource as CIE1960;
				}

				// Warning, it's not a good idea to do it with other types, we do with CIEXYZ bacause
				// we have a more precise conversion and because a call-loop it's not possible.
				if (DataSource is CIEXYZ) {
					return (DataSource as CIEXYZ).ToCIEUCS ();
				}

				double div = (12 * y - 2 * x + 3);
				double yy = Y / y;

				return new CIE1960 (
					DataSource ?? this,
					4 * x / div, 6 * y / div, 0.5 * (-x * yy + 3 * Y + yy * (1.0 - x - y))
				);
			}

			#endregion


			#region Object methods

			/**
			 *  <inheritdoc />
			 */
			public override bool Equals(Object obj)
			{
				CIExyY xyYObj = obj as CIExyY; 

				if (xyYObj == this) {
					return true;
				}
				if (xyYObj == null) {
					return false;
				}

				return (x == xyYObj.x && y == xyYObj.y && Y == xyYObj.Y);
			}

			/**
			 *  <inheritdoc />
			 */
			public override int GetHashCode ()
			{
				int hash = 28891 + x.GetHashCode ();  // 28891 == 167 * 173

				hash = hash * 173 + y.GetHashCode ();

				return hash * 173 + Y.GetHashCode ();
			}

			#endregion


			#region internal utilities

			/**
			 * Monotone Chain algorithm. See
			 * http://en.wikibooks.org/wiki/Algorithm_Implementation/Geometry/Convex_hull/Monotone_chain
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

			/**
			 * Used inside findConvexHull method.
			 */
			static double cross(xyYPoint O, xyYPoint A, xyYPoint B) {
				return (A.x - O.x) * (B.y - O.y) - (A.y - O.y) * (B.x - O.x);
			}

			#endregion
		}
	}
}
