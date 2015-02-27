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
		 * <summary></summary>
		 */
		public sealed class LMS : AConvertibleColor
		{
			#region properties

			/**
			 * <value>Large wavelength cones stimulous value.</value>
			 */
			public readonly double L;

			/**
			 * <value>Medium wavelength cones stimulous value.</value>
			 */
			public readonly double M;

			/**
			 * <value>Small wavelength cones stimulous value.</value>
			 */
			public readonly double S;

			#endregion


			public LMS (double L, double M, double S, AConvertibleColor dataSource=null) : base(dataSource)
			{
				if (double.IsNaN (L) || double.IsInfinity (L)) {
					throw new ArgumentException ("L must be a real number", "L");
				}
				if (double.IsNaN (M) || double.IsInfinity (M)) {
					throw new ArgumentException ("M must be a real number", "M");
				}
				if (double.IsNaN (S) || double.IsInfinity (S)) {
					throw new ArgumentException ("S must be a real number", "S");
				}

				this.L = L;
				this.M = M;
				this.S = S;
			}


			#region AConvertibleColor methods

			/**
			 * <inheritdoc />
			 */
			public override bool IsInsideColorSpace(bool highPrecision = false)
			{
				throw new NotImplementedException ();
			}

			/**
			 * <inheritdoc />
			 */
			public override CIEXYZ ToCIEXYZ ()
			{
				return ToCIEXYZ (LMSStrategy.Bradford);
			}

			/**
			 * <summary>Converter method that allows chosing the conversion strategy.</summary>
			 * <param name="strategy">Strategy to choose which conversion matrix we apply.</param>
			 */
			public CIEXYZ ToCIEXYZ (LMSStrategy strategy)
			{
				if (DataSource is CIEXYZ) {
					// TODO : Check chromatic adaptation, depends on "connecting" the ASimpleColor class
					// TODO : Add historic data about strategy because here we don't know the "applied" strategy
					return DataSource as CIEXYZ;
				}

				if (strategy == LMSStrategy.Bradford) {
					return BradfordTransform ();
				}
				if (strategy == LMSStrategy.VonKries) {
					return VonKriesTransform ();
				}

				throw new NotImplementedException ();
			}

			#endregion


			#region Chromatic Adaptation

			public CIEXYZ BradfordTransform ()
			{
				return new CIEXYZ (
					+0.9869929 * L - 0.1470543 * M + 0.1599627 * S,
					+0.4323053 * L + 0.5183603 * M + 0.0492912 * S,
					-0.0085287 * L + 0.0400428 * M + 0.9684867 * S, 
					DataSource ?? this
				);
			}

			public CIEXYZ VonKriesTransform ()
			{
				return new CIEXYZ (
					1.8599364 * L - 1.1293816 * M + 0.2198974 * S,
					0.3611914 * L + 0.6388125 * M - 0.0000064 * S,
					0.0000000     + 0.0000000     + 1.0890636 * S,
					DataSource ?? this
				);
			}

			// TODO : Add other transforms

			#endregion


			#region Object methods

			/**
			 *  <inheritdoc />
			 */
			public override bool Equals(Object obj)
			{
				LMS lmsObj = obj as LMS; 

				if (lmsObj == this) {
					return true;
				}
				if (lmsObj == null) {
					return false;
				}

				return (L == lmsObj.L && M == lmsObj.M && S == lmsObj.S);
			}

			/**
			 *  <inheritdoc />
			 */
			public override int GetHashCode ()
			{
				// TODO : Change prime numbers?
				int hash = 28891 + L.GetHashCode ();  // 28891 == 167 * 173

				hash = hash * 173 + M.GetHashCode ();

				return hash * 173 + S.GetHashCode ();
			}

			#endregion
		}
	}
}

