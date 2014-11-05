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


namespace Litipk.ColorSharp
{
	namespace ColorSpaces
	{
		/**
		 *
		 */
		public abstract class AConvertibleColor
		{
			#region properties

			/**
			 * <value>Original color sample</value>
			 */
			public readonly AConvertibleColor DataSource = null;

			#endregion


			#region constructors

			/**
			 * Boilerplate constructor
			 */
			protected AConvertibleColor(AConvertibleColor dataSource = null)
			{
				DataSource = dataSource;
			}

			#endregion


			#region conversion skeleton

			/**
			 * <see cref="ConvertTo"/>
			 */
			public T ConvertTo<T> (ConversionStrategy strategy=ConversionStrategy.Default) where T : AConvertibleColor
			{
				return (T)ConvertTo (typeof(T), strategy);
			}

			/**
			 * <summary>Method that allows conversions passing the type as a parameter.</summary>
			 * <remarks>DON'T USE it to implement conversion methods, use the non type-parametric variants.</remarks>
			 */
			public AConvertibleColor ConvertTo (Type t, ConversionStrategy strategy=ConversionStrategy.Default)
			{
				Type tt = GetType ();

				if (t == tt) {
					// Dumb conversion
					return this;
				}

				if (DataSource != null) {
					if (DataSource.GetType () == t) {
						return DataSource;
					}

					return DataSource.ConvertTo(t, strategy);
				}

				return InnerConvertTo(t, strategy);
			}

			/**
			 * Helper method used by ConvertTo.
			 */
			private AConvertibleColor InnerConvertTo (Type t, ConversionStrategy strategy = ConversionStrategy.Default)
			{
				if (t == typeof(CIEXYZ))
					return ToCIEXYZ (strategy);
				if (t == typeof(CIExyY))
					return ToCIExyY(strategy);
				if (t == typeof(SRGB))
					return ToSRGB (strategy);

				throw new NotImplementedException ("This conversion isn't implemented.");
			}

			#endregion


			#region abstract methods

			/**
			 * <summary>Tells us if the object represents a valid color sample in current color space.</summary>
			 */
			public abstract bool IsInsideColorSpace ();

			/**
			 * <summary>Converts the color sample to a CIE's 1931 XYZ color sample.</summary>
			 */
			public abstract CIEXYZ ToCIEXYZ(ConversionStrategy strategy = ConversionStrategy.Default);

			/**
			 * <summary>Converts the color sample to a CIE's 1931 xyY color sample.</summary>
			 */
			public abstract CIExyY ToCIExyY (ConversionStrategy strategy = ConversionStrategy.Default);

			/**
			 * <summary>Converts the color sample to an HP's and Microsoft's 1996 sRGB sample.</summary>
			 */
			public abstract SRGB ToSRGB(ConversionStrategy strategy = ConversionStrategy.Default);

			#endregion
		}
	}
}
