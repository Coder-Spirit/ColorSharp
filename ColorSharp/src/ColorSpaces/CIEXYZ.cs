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
			public override CIEXYZ ToCIEXYZ (ColorStrategy strategy=ColorStrategy.Default)
			{
				return this;
			}

			/**
			 * <inheritdoc />
			 */
			public override CIExyY ToCIExyY (ColorStrategy strategy=ColorStrategy.Default)
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
			public override CIEUCS ToCIEUCS (ColorStrategy strategy = ColorStrategy.Default)
			{
				return (DataSource as CIEUCS) ?? new CIEUCS (
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
			public override SRGB ToSRGB (ColorStrategy strategy=ColorStrategy.ForceLowTruncate|ColorStrategy.ForceHighStretch)
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

				if ((strategy & ColorStrategy.ForceLowStretch) != 0) {
					double minChannelValue = Math.Min (r, Math.Min (g, b));

					if (minChannelValue < 0.0) {
						r -= minChannelValue;
						g -= minChannelValue;
						b -= minChannelValue;
					}
				} else if ((strategy & ColorStrategy.ForceLowTruncate) != 0) {
					r = Math.Max (0.0, r);
					g = Math.Max (0.0, g);
					b = Math.Max (0.0, b);
				}

				if ((strategy & ColorStrategy.ForceHighStretch) != 0) {
					double maxChannelValue = Math.Max (r, Math.Max (g, b));

					if (maxChannelValue > 1.0) {
						r = r / maxChannelValue;
						g = g / maxChannelValue;
						b = b / maxChannelValue;
					}
				} else if ((strategy & ColorStrategy.ForceHighTruncate) != 0) {
					r = Math.Min (1.0, r);
					g = Math.Min (1.0, g);
					b = Math.Min (1.0, b);
				}

				return new SRGB(r, g, b, DataSource ?? this);
			}

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

