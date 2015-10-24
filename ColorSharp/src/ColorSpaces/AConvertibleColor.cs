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

using Litipk.ColorSharp.LightSpectrums;
using Litipk.ColorSharp.Strategies;


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
			public T ConvertTo<T> () where T : AConvertibleColor
			{
				return (T)ConvertTo (typeof(T));
			}

			/**
			 * <summary>Method that allows conversions passing the type as a parameter.</summary>
			 * <remarks>DON'T USE it to implement conversion methods, use the non type-parametric variants.</remarks>
			 */
			public AConvertibleColor ConvertTo (Type t)
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

					return DataSource.ConvertTo(t);
				}

				return InnerConvertTo(t);
			}

			/**
			 * Helper method used by ConvertTo.
			 */
			AConvertibleColor InnerConvertTo (Type t)
			{
				if (t == typeof(CIEXYZ))
					return ToCIEXYZ ();
				if (t == typeof(CIExyY))
					return ToCIExyY();
				if (t == typeof(CIE1960))
					return ToCIEUCS ();
				if (t == typeof(SRGB))
					return ToSRGB ();
				if (t == typeof(LMS))
					return ToLMS ();

				throw new NotImplementedException ("This conversion isn't implemented.");
			}

			#endregion


			#region abstract methods

			/**
			 * <summary>Tells us if the object represents a valid color sample in current color space.</summary>
			 */
			public abstract bool IsInsideColorSpace (bool highPrecision = false);

			/**
			 * <summary>Gives us the Correlated Color Temperature associated to the spectrum or color.</summary>
			 */
			public virtual double GetCCT ()
			{
				if (DataSource is BlackBodySpectrum) {
					return DataSource.GetCCT ();
				}

				return ToCIEUCS ().GetCCT ();
			}

			/**
			 * <summary>
			 * Gives us the distance in the UCS color space between our color point and the nearest planckian locus.
			 * </summary>
			 */
			public virtual double GetDuv ()
			{
				if (DataSource is BlackBodySpectrum) {
					return 0;
				}

				return ToCIEUCS ().GetDuv ();
			}

			/**
			 * <summary>Converts the color sample to a CIE's 1931 XYZ color sample.</summary>
			 */
			public abstract CIEXYZ ToCIEXYZ();

			/**
			 * <summary>Converts the color point to an LMS cones stimulous value.</summary>
			 */
			public virtual LMS ToLMS (LMSStrategy strategy = LMSStrategy.Bradford)
			{
				if (DataSource is LMS) {
					// TODO: Check other LMS-related TODOs...
					return DataSource as LMS;
				}

				return ToCIEXYZ ().ToLMS (strategy);
			}

			/**
			 * <summary>Converts the color sample to a CIE's 1931 xyY color sample.</summary>
			 */
			public virtual CIExyY ToCIExyY ()
			{
				return (DataSource as CIExyY) ?? ToCIEXYZ ().ToCIExyY ();
			}

			/**
			 * <summary>Converts the color sample to a CIE's 1960 UCS color sample.</summary>
			 */
			public virtual CIE1960 ToCIEUCS ()
			{
				return (DataSource as CIE1960) ?? ToCIEXYZ ().ToCIEUCS ();
			}

			/**
			 * <summary>Converts the color sample to an HP's and Microsoft's 1996 sRGB sample.</summary>
			 */
			public virtual SRGB ToSRGB(ToSmallSpaceStrategy strategy = ToSmallSpaceStrategy.ForceLowTruncate|ToSmallSpaceStrategy.ForceHighStretch)
			{
				return (DataSource as SRGB) ?? ToCIEXYZ ().ToSRGB (strategy);
			}

			#endregion
		}
	}
}
