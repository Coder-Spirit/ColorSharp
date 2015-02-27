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
		 * <summary>HP's and Microsoft's 1996 sRGB Color Space.</summary>
		 */
		public sealed class SRGB : AConvertibleColor
		{
			#region private properties

			/**
			 * <value>Red component</value>
			 */
			public readonly double R;

			/**
			 * <value>Green component</value>
			 */
			public readonly double G;

			/**
			 * <value>Blue component</value>
			 */
			public readonly double B;

			#endregion


			#region constructors

			/**
			 * <summary>Creates a new color sample in the sRGB color space</summary>
			 * <param name="R">Red component (between 0 and 1)</param>
			 * <param name="G">Green component (between 0 and 1)</param>
			 * <param name="B">Blue component (between 0 and 1)</param>
			 * <param name="dataSource">If you aren't working with ColorSharp internals, don't use this parameter</param>
			 */
			public SRGB (double R, double G, double B, AConvertibleColor dataSource=null) : base(dataSource)
			{
				this.R = R;
				this.G = G;
				this.B = B;
			}

			#endregion


			#region AConvertibleColor methods

			/**
			 * <inheritdoc />
			 */
			public override bool IsInsideColorSpace(bool highPrecision = false)
			{
				return (
					0.0 <= R && R <= 1.0 &&
					0.0 <= G && B <= 1.0 &&
					0.0 <= B && B <= 1.0
				);
			}

			/**
			 * <inheritdoc />
			 */
			public override CIEXYZ ToCIEXYZ ()
			{
				CIEXYZ xyzDS = DataSource as CIEXYZ;
				if (xyzDS != null) {
					return xyzDS;
				}

				// Gamma correction
				double r = R > 0.04045 ? Math.Pow((R+0.055)/1.055, 2.4) : R/12.92 ;
				double g = G > 0.04045 ? Math.Pow((G+0.055)/1.055, 2.4) : G/12.92 ;
				double b = B > 0.04045 ? Math.Pow((B+0.055)/1.055, 2.4) : B/12.92 ;

				return new CIEXYZ(
					// Linear transformation
					r * 0.412424 + g * 0.357579 + b * 0.180464,
					r * 0.212656 + g * 0.715158 + b * 0.072186,
					r * 0.019332 + g * 0.119193 + b * 0.950444,
					DataSource ?? this
				);
			}

			/**
			 * <inheritdoc />
			 */
			public override SRGB ToSRGB (ToSmallSpaceStrategy strategy = ToSmallSpaceStrategy.Default)
			{
				return this;
			}

			#endregion


			#region Object methods

			/**
			 * <inheritdoc />
			 */
			public override bool Equals(Object obj)
			{
				SRGB srgbObj = obj as SRGB; 

				if (srgbObj == this) {
					return true;
				}
				if (srgbObj == null || GetHashCode () != obj.GetHashCode ()) {
					return false;
				}

				return (R == srgbObj.R && G == srgbObj.G && B == srgbObj.B);
			}

			/**
			 * <inheritdoc />
			 */
			public override int GetHashCode ()
			{
				int hash = 32399 + R.GetHashCode (); // 32399 == 179 * 181

				hash = hash * 181 + G.GetHashCode ();

				return hash * 181 + B.GetHashCode ();
			}

			#endregion
		}
	}
}


