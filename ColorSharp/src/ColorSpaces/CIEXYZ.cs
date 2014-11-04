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


namespace Litipk.ColorSharp
{
	namespace ColorSpaces
	{
		/**
		 * CIE 1931 (2º) XYZ Color Space.
		 */
		public sealed class CIEXYZ : AConvertibleColor
		{
			#region readonly properties

			public readonly double X, Y, Z;

			#endregion


			#region constructors

			public CIEXYZ() {
				Conversors.Add (typeof(SRGB), ToSRGB);
				Conversors.Add (typeof(CIExyY), ToxyY);
			}

			/**
			 * This constructor "installs" the conversor methods into the instance
			 */
			private CIEXYZ(AConvertibleColor dataSource=null) : base(dataSource) {
				Conversors.Add (typeof(SRGB), ToSRGB);
				Conversors.Add (typeof(CIExyY), ToxyY);
			}

			// Constructor
			public CIEXYZ (double X, double Y, double Z, AConvertibleColor dataSource=null) : this(dataSource)
			{
				this.X = X;
				this.Y = Y;
				this.Z = Z;
			}

			#endregion


			#region conversors

			public CIExyY ToxyY (ConversionStrategy strategy=ConversionStrategy.Default)
			{
				double XYZ = X + Y + Z;
				return new CIExyY (X / XYZ, Y / XYZ, Y, DataSource ?? this);
			}

			/**
			 * Converts the CIE 1931 XYZ sample to a HP's & Microsoft's 1996 sRGB sample
			 */
			public SRGB ToSRGB (ConversionStrategy strategy=ConversionStrategy.Default)
			{
				double tx = X / 100.0;
				double ty = Y / 100.0;
				double tz = Z / 100.0;

				// Linear transformation
				double r = tx * 3.2406 + ty * -1.5372 + tz * -0.4986;
				double g = tx * -0.9689 + ty * 1.8758 + tz * 0.0415;
				double b = tx * 0.0557 + ty * -0.2040 + tz * 1.0570;

				// Gamma correction
				r = r > 0.0031308 ? 1.055 * Math.Pow(r, 1 / 2.4) - 0.055 : 12.92 * r;
				g = g > 0.0031308 ? 1.055 * Math.Pow(g, 1 / 2.4) - 0.055 : 12.92 * g;
				b = b > 0.0031308 ? 1.055 * Math.Pow(b, 1 / 2.4) - 0.055 : 12.92 * b;

				if ((strategy & ConversionStrategy.ForceStretching) != 0) {
					double minChannelValue = Math.Min (r, Math.Min (g, b));

					if (minChannelValue < 0.0) {
						r -= minChannelValue;
						g -= minChannelValue;
						b -= minChannelValue;
					}
				} else if ((strategy & ConversionStrategy.ForceTruncating) != 0) {
					r = Math.Max (0.0, r);
					g = Math.Max (0.0, g);
					b = Math.Max (0.0, b);
				}

				double maxChannelValue = Math.Max (r, Math.Max (g, b));

				if (maxChannelValue > 1.0) {
					r = r / maxChannelValue;
					g = g / maxChannelValue;
					b = b / maxChannelValue;
				}

				return new SRGB(r, g, b, DataSource ?? this);
			}

			#endregion


			#region inherited methods

			public override bool IsInsideColorSpace()
			{
				return ConvertTo<CIExyY> ().IsInsideColorSpace ();
			}

			public override bool Equals(Object obj)
			{
				CIEXYZ xyzObj = obj as CIEXYZ; 

				if (xyzObj == null || GetHashCode () != obj.GetHashCode ())
					return false;

				return (
					Math.Abs (X - xyzObj.X) <= double.Epsilon &&
					Math.Abs (Y - xyzObj.Y) <= double.Epsilon &&
					Math.Abs (Z - xyzObj.Z) <= double.Epsilon
				);
			}
			public override int GetHashCode ()
			{
				int hash = 23;

				hash = hash * 57 + X.GetHashCode ();
				hash = hash * 57 + Y.GetHashCode ();
				hash = hash * 57 + Z.GetHashCode ();

				return hash;
			}


			#endregion
		}
	}
}

