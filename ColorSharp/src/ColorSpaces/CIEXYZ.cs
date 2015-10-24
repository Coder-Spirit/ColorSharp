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

using Litipk.ColorSharp.Strategies;


namespace Litipk.ColorSharp
{
	namespace ColorSpaces
	{
		/**
		 * <summary>CIE's 1931 (2º) XYZ Color Space.</summary>
		 */
		public sealed class CIEXYZ : AConvertibleColor
		{
			#region properties

			/**
			 * <value>X component of the CIE's 1931 XYZ color space.</value>
			 */
			public readonly double X;

			/**
			 * <value>Y component of the CIE's 1931 XYZ color space.</value>
			 */
			public readonly double Y;

			/**
			 * <value>Z component of the CIE's 1931 XYZ color space.</value>
			 */
			public readonly double Z;

			#endregion


			#region constructors

			/**
			 * <summary>Creates a new color sample in the CIE's 1931 xyY color space</summary>
			 * <param name="X">CIE's 1931 XYZ X coordinate</param>
			 * <param name="Y">CIE's 1931 XYZ Y coordinate</param>
			 * <param name="Z">CIE's 1931 XYZ Z coordinate</param>
			 * <param name="dataSource">If you aren't working with ColorSharp internals, don't use this parameter</param>
			 */
			public CIEXYZ (double X, double Y, double Z, AConvertibleColor dataSource=null) : base(dataSource)
			{
				this.X = X;
				this.Y = Y;
				this.Z = Z;
			}

			#endregion


			#region AConvertibleColor methods

			/**
			 * <inheritdoc />
			 */
			public override bool IsInsideColorSpace(bool highPrecision = false)
			{
				return ToCIExyY ().IsInsideColorSpace (highPrecision);
			}

			/**
			 * <inheritdoc />
			 */
			public override LMS ToLMS (LMSStrategy strategy=LMSStrategy.Bradford)
			{
				LMS lmsDS = DataSource as LMS;
				if (lmsDS != null) {
					// TODO : Check chromatic adaptation, depends on "connecting" the ASimpleColor class
					// TODO : Use the added historic data about strategy in LMS...
					return lmsDS;
				}

				if (strategy == LMSStrategy.Bradford) {
					return BradfordTransform ();
				}
				if (strategy == LMSStrategy.VonKries) {
					return VonKriesTransform ();
				}

				throw new NotImplementedException ();
			}

			/**
			 * <inheritdoc />
			 */
			public override CIEXYZ ToCIEXYZ ()
			{
				return this;
			}

			/**
			 * <inheritdoc />
			 */
			public override CIExyY ToCIExyY ()
			{
				CIExyY xyYDS = DataSource as CIExyY;
				if (xyYDS != null) {
					return xyYDS;
				}

				double XYZ = X + Y + Z;
				return new CIExyY (X / XYZ, Y / XYZ, Y, DataSource ?? this);
			}

			/**
			 * <inheritdoc />
			 */
			public override CIE1960 ToCIEUCS ()
			{
				return (DataSource as CIE1960) ?? new CIE1960 (
					2.0 * X / 3.0,                // U
					Y,                            // V
					4 * X / (X + 15 * Y + 3 * Z), // u
					6 * Y / (X + 15 * Y + 3 * Z), // v
					0.5 * (-X + 3 * Y + Z),       // W
					DataSource ?? this    
				);
			}

			/**
			* <inheritdoc />
			*/
			public override SRGB ToSRGB (ToSmallSpaceStrategy strategy=ToSmallSpaceStrategy.ForceLowTruncate|ToSmallSpaceStrategy.ForceHighStretch)
			{
				SRGB srgbDS = DataSource as SRGB;
				if (srgbDS != null) {
					return srgbDS;
				}

				// Linear transformation
				double r = X * 3.2406 + Y * -1.5372 + Z * -0.4986;
				double g = X * -0.9689 + Y * 1.8758 + Z * 0.0415;
				double b = X * 0.0557 + Y * -0.2040 + Z * 1.0570;

				// Gamma correction
				r = r > 0.0031308 ? 1.055 * Math.Pow(r, 1 / 2.4) - 0.055 : 12.92 * r;
				g = g > 0.0031308 ? 1.055 * Math.Pow(g, 1 / 2.4) - 0.055 : 12.92 * g;
				b = b > 0.0031308 ? 1.055 * Math.Pow(b, 1 / 2.4) - 0.055 : 12.92 * b;

				if ((strategy & ToSmallSpaceStrategy.ForceLowStretch) != 0) {
					double minChannelValue = Math.Min (r, Math.Min (g, b));

					if (minChannelValue < 0.0) {
						r -= minChannelValue;
						g -= minChannelValue;
						b -= minChannelValue;
					}
				} else if ((strategy & ToSmallSpaceStrategy.ForceLowTruncate) != 0) {
					r = Math.Max (0.0, r);
					g = Math.Max (0.0, g);
					b = Math.Max (0.0, b);
				}

				if ((strategy & ToSmallSpaceStrategy.ForceHighStretch) != 0) {
					double maxChannelValue = Math.Max (r, Math.Max (g, b));

					if (maxChannelValue > 1.0) {
						r = r / maxChannelValue;
						g = g / maxChannelValue;
						b = b / maxChannelValue;
					}
				} else if ((strategy & ToSmallSpaceStrategy.ForceHighTruncate) != 0) {
					r = Math.Min (1.0, r);
					g = Math.Min (1.0, g);
					b = Math.Min (1.0, b);
				}

				return new SRGB(r, g, b, DataSource ?? this);
			}

			#endregion


			#region Chromatic Adaptation

			public LMS BradfordTransform ()
			{
				return new LMS (
					+0.8951000 * X + 0.2664000 * Y - 0.1614000 * Z,
					-0.7502000 * X + 1.7135000 * Y + 0.0367000 * Z,
					+0.0389000 * X - 0.0685000 * Y + 1.0296000 * Z,
					DataSource ?? this
				);
			}

			public LMS VonKriesTransform ()
			{
				return new LMS (
					+0.4002400 * X + 0.7076000 * Y - 0.0808100 * Z,
					-0.2263000 * X + 1.1653200 * Y + 0.0457000 * Z,
					+0.0000000     + 0.0000000     + 0.9182200 * Z,
					DataSource ?? this
				);
			}

			// TODO : Add other transforms

			#endregion


			#region Object methods

			/**
			 * <inheritdoc />
			 */
			public override bool Equals(Object obj)
			{
				CIEXYZ xyzObj = obj as CIEXYZ; 

				if (xyzObj == this) {
					return true;
				}
				if (xyzObj == null || GetHashCode () != obj.GetHashCode ()) {
					return false;
				}

				return (X == xyzObj.X && Y == xyzObj.Y && Z == xyzObj.Z);
			}

			/**
			 * <inheritdoc />
			 */
			public override int GetHashCode ()
			{
				int hash = 30967 + X.GetHashCode ();  // 30967 == 173 * 179

				hash = hash * 179 + Y.GetHashCode ();

				return hash * 179 + Z.GetHashCode ();
			}

			#endregion
		}
	}
}

