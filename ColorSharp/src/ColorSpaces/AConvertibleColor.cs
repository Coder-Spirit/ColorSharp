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
		public abstract class AConvertibleColor
		{
			#region properties

			// If this "color" comes from another data source, then we keep the original data.
			protected AConvertibleColor DataSource = null;

			#endregion


			#region constructors

			// TODO: Check if this should be public in order to work as expected with reflection.
			protected AConvertibleColor(AConvertibleColor dataSource = null)
			{
				DataSource = dataSource;
			}

			#endregion


			#region conversion skeleton

			public T ConvertTo<T> (ConversionStrategy strategy=ConversionStrategy.Default) where T : AConvertibleColor
			{
				Type t = typeof(T);
				Type tt = GetType ();

				if (t == tt) {
					// Dumb conversion
					return (T)(AConvertibleColor)this;
				}

				if (DataSource != null) {
					if (DataSource.GetType () == t) {
						return (T)DataSource;
					}

					return DataSource.ConvertTo<T> (strategy);
				}

				return InnerConvertTo<T> (strategy);
			}

			protected T InnerConvertTo<T> (ConversionStrategy strategy = ConversionStrategy.Default) where T : AConvertibleColor
			{
				Type t = typeof(T);

				if (t == typeof(CIEXYZ))
					return (T)(AConvertibleColor)ToCIEXYZ (strategy);
				if (t == typeof(CIExyY))
					return (T)(AConvertibleColor)ToCIExyY(strategy);
				if (t == typeof(SRGB))
					return (T)(AConvertibleColor)ToSRGB (strategy);

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
			 * <summary>Converts the color sample to an HP's & Microsoft's 1996 sRGB sample.</summary>
			 */
			public abstract SRGB ToSRGB(ConversionStrategy strategy = ConversionStrategy.Default);

			#endregion
		}
	}
}
