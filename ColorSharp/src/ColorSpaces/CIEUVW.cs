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
		 * <summary>CIE's 1960 UVW Color Space.</summary>
		 */
		public sealed class CIEUVW : AConvertibleColor
		{
			#region properties

			/**
			 * <value>U component of the CIE's 1960 UVW color space.</value>
			 */
			public readonly double U;

			/**
			 * <value>V component of the CIE's 1960 UVW color space.</value>
			 */
			public readonly double V;

			/**
			 * <value>W component of the CIE's 1960 UVW color space.</value>
			 */
			public readonly double W;

			#endregion


			#region constructors

			public CIEUVW (double U, double V, double W, AConvertibleColor dataSource=null) : base(dataSource)
			{
				this.U = U;
				this.V = V;
				this.W = W;
			}

			#endregion


			#region AConvertibleColor methods

			/**
			 * <inheritdoc />
			 */
			public override bool IsInsideColorSpace()
			{
				// TODO : Improve ?
				return ToCIExyY ().IsInsideColorSpace ();
			}

			/**
			 * <inheritdoc />
			 */
			public override CIEXYZ ToCIEXYZ (ConversionStrategy strategy=ConversionStrategy.Default)
			{
				return (DataSource as CIEXYZ) ?? new CIEXYZ (
					1.5 * U, V, 1.5 * U - 3 * V + 2 * W, DataSource ?? this
				);
			}

			/**
			 * <inheritdoc />
			 */
			public override CIEUVW ToCIEUVW (ConversionStrategy strategy = ConversionStrategy.Default)
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
				CIEUVW uvwObj = obj as CIEUVW; 

				if (uvwObj == this) {
					return true;
				}
				if (uvwObj == null || GetHashCode () != obj.GetHashCode ()) {
					return false;
				}

				return (U == uvwObj.U && V == uvwObj.V && W == uvwObj.W);
			}

			/**
			 * <inheritdoc />
			 */
			public override int GetHashCode ()
			{
				int hash = 77837 + U.GetHashCode ();  // 77837 == 277 * 281

				hash = hash * 281 + V.GetHashCode ();

				return hash * 281 + W.GetHashCode ();
			}

			#endregion
		}
	}
}
