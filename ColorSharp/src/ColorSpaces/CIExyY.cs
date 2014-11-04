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
			static xyYPoint[] Sharkfin;

			// Sharkfin transformation with performance purposes.
			static xyYPoint[] SortedSharkfin;
			#endregion

			#region readonly properties
			/**
			 * <value>x component of the CIE's 1931 color space : X/(X+Y+Z) .</value>
			 */
			public readonly double x;

			/**
			 * <value>y component of the CIE's 1931 color space : Y/(X+Y+Z) .</value>
			 */
			public readonly double y;

			/**
			 * <value>Y component of the CIE's 1931 color space. The same Y from CIE's 1931 XYZ color space.</value>
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

			/**
			 * <summary>HACK: DONT' USE this constructor.</summary>
			 */
			public CIExyY() {
				Conversors.Add (typeof(CIEXYZ), ToXYZ);
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

				Conversors.Add (typeof(CIEXYZ), ToXYZ);
			}

			#endregion


			#region conversors

			public CIEXYZ ToXYZ (ConversionStrategy strategy=ConversionStrategy.Default)
			{
				return new CIEXYZ (x*Y/y, Y, Y*(1.0 - x - y)/y, DataSource ?? this);
			}

			#endregion


			#region inherited methods

			/**
			 * <inheritdoc />
			 */
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
			 *  <inheritdoc />
			 */
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

			/**
			 *  <inheritdoc />
			 */
			public override int GetHashCode ()
			{
				int hash = 19;

				hash = hash * 31 + x.GetHashCode ();
				hash = hash * 31 + y.GetHashCode ();
				hash = hash * 31 + Y.GetHashCode ();

				return hash;
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
