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

using Litipk.ColorSharp.LightSpectrums;
using Litipk.ColorSharp.Strategies;


namespace Litipk.ColorSharp
{
	namespace ColorSpaces
	{
		/**
		 * <summary>CIE's 1960 UCS Color Space. Also named CIE's 1960 Yuv Color Space.</summary>
		 */
		public sealed class CIE1960 : AConvertibleColor
		{
			#region properties

			bool GetMinorCase;
			bool GetMajorCase;

			/**
			 * <value>U / (U + V + W)</value>
			 */
			public readonly double u;

			/**
			 * <value>V / (U + V + W)</value>
			 */
			public readonly double v;

			/**
			 * <value>U component of the CIE's 1960 UCS color space.</value>
			 */
			public readonly double U;

			/**
			 * <value>V component of the CIE's 1960 UCS color space (also Y in Yuv).</value>
			 */
			public readonly double V;

			/**
			 * <value>W component of the CIE's 1960 UCS color space.</value>
			 */
			public readonly double W;

			/**
			 * <value>Y component of the CIE's 1960 Yuv color space (also V in UCS).</value>
			 */
			public double Y { get { return V; } }

			static List<CIE1960> TemperatureChromaticities = null;

			double CCT = double.NaN;

			double Duv = double.NaN;

			#endregion


			#region constructors

			/**
			 * <summary>Creates a new color sample in the CIE's 1960 UCS color space</summary>
			 * <param name="U">CIE's 1960 UCS U coordinate</param>
			 * <param name="V">CIE's 1960 UCS V coordinate</param>
			 * <param name="W">CIE's 1960 UCS W coordinate</param>
			 * <param name="dataSource">If you aren't working with ColorSharp internals, don't use this parameter</param>
			 */
			public CIE1960 (double U, double V, double W, AConvertibleColor dataSource=null) : base(dataSource)
			{
				if (U < 0.0 || V < 0.0 || W < 0.0) {
					throw new ArgumentException ("Invalid color point");
				}

				GetMinorCase = false;
				GetMajorCase = true;

				this.U = U;
				this.V = V;
				this.W = W;

				u = U / (U + V + W);
				v = V / (U + V + W);
			}

			/**
			 * <summary>Creates a new color sample in the CIE's 1960 UCS color space</summary>
			 * <param name="u">CIE's 1960 UCS u coordinate</param>
			 * <param name="v">CIE's 1960 UCS v coordinate</param>
			 * <param name="W">CIE's 1960 UCS W coordinate</param>
			 * <param name="dataSource">If you aren't working with ColorSharp internals, don't use this parameter</param>
			 */
			public CIE1960 (AConvertibleColor dataSource, double u, double v, double W) : base(dataSource)
			{
				if (u < 0.0 || v < 0.0 || W < 0 || u + v > 1.0) {
					throw new ArgumentException ("Invalid color point");
				}

				GetMinorCase = true;
				GetMajorCase = false;

				this.u = u;
				this.v = v;
				this.W = W;

				U = W * u / (1.0 - u - v);
				V = W * v / (1.0 - u - v);
			}

			/**
			 * <summary>
			 *   Creates a new color sample in the CIE's 1960 UCS color space. This is a convenience constructor to
			 *   allow minimum information loss in color space conversions, but not recommeneded to construct colors
			 *   "by hand".
			 * </summary>
			 * <param name="U">CIE's 1960 UCS U coordinate</param>
			 * <param name="V">CIE's 1960 UCS V coordinate</param>
			 * <param name="u">CIE's 1960 UCS u coordinate</param>
			 * <param name="v">CIE's 1960 UCS v coordinate</param>
			 * <param name="W">CIE's 1960 UCS W coordinate</param>
			 * <param name="dataSource">If you aren't working with ColorSharp internals, don't use this parameter</param>
			 */
			public CIE1960 (double U, double V, double u, double v, double W, AConvertibleColor dataSource=null) : base(dataSource)
			{
				if (U < 0.0 || V < 0.0 || W < 0.0 || u < 0.0 || v < 0.0) {
					throw new ArgumentException ("Invalid color point");
				}

				if (Math.Abs (U / (U + V + W) - u) > 1e-15) {
					throw new ArgumentException ("Inconsistent data");
				}
				if (Math.Abs (V / (U + V + W) - v) > 1e-15) {
					throw new ArgumentException ("Inconsistent data");
				}

				this.U = U;
				this.V = V;
				this.u = u;
				this.v = v;
				this.W = W;

				GetMinorCase = true;
				GetMajorCase = true;
			}

			#endregion


			#region AConvertibleColor methods

			/**
			 * <inheritdoc />
			 */
			public override bool IsInsideColorSpace(bool highPrecision = false)
			{
				// TODO : Improve ?
				return ToCIExyY ().IsInsideColorSpace (highPrecision);
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetCCT ()
			{
				if (DataSource is BlackBodySpectrum) {
					return (DataSource as BlackBodySpectrum).CCT;
				}

				if (!double.IsNaN (CCT)) {
					return CCT;
				}

				// Precomputing interpolation tables...
				if (TemperatureChromaticities == null) {
					// Oversized to improve alignment (needs 302).
					TemperatureChromaticities = new List<CIE1960> (512);

					// From 1000º K to 20000º K
					for (double t = 1000.0; t < 20001.0; t *= 1.01) {
						TemperatureChromaticities.Add (
							new BlackBodySpectrum (t).ToCIEXYZ (Spd2ClrStrategy.Nm1Deg2).ToCIEUCS ()
						);
					}
				}

				int bestI = 0;
				Duv = double.PositiveInfinity;

				// First gross grained search
				// TODO: This is a naive search, must be improved!
				for (int i = 0; i < TemperatureChromaticities.Count; i++) {
					double tmpDuv = Math.Sqrt (
		                Math.Pow (u - TemperatureChromaticities [i].u, 2) +
		                Math.Pow (v - TemperatureChromaticities [i].v, 2)
	                );

					if (Duv > tmpDuv) {
						Duv = tmpDuv;
						bestI = i;
					}
				}
				CCT = TemperatureChromaticities [bestI].GetCCT ();

				// Preparing the following fine grained search
				double tMin = TemperatureChromaticities [
					(bestI > 0) ? bestI - 1 : bestI
				].GetCCT ();
				double tMax = TemperatureChromaticities [
					(bestI < TemperatureChromaticities.Count - 1) ? bestI + 1 : bestI
				].GetCCT ();
				double tDiff = (tMax - tMin) / 100.0;

				// Second fine grained search
				for (double t = tMin; t < tMax; t += tDiff) {
					var tmpUV = new BlackBodySpectrum (t).ToCIEXYZ (Spd2ClrStrategy.Nm1Deg2).ToCIEUCS ();

					double tmpDuv = Math.Sqrt (Math.Pow (u - tmpUV.u, 2) +	Math.Pow (v - tmpUV.v, 2));

					if (Duv > tmpDuv) {
						Duv = tmpDuv;
						CCT = t;
					}
				}

				return CCT;
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetDuv ()
			{
				if (DataSource is BlackBodySpectrum) {
					return 0;
				}

				if (double.IsNaN (Duv)) {
					GetCCT ();
				}

				return Duv;
			}

			/**
			 * <inheritdoc />
			 */
			public override CIEXYZ ToCIEXYZ ()
			{
				// TODO: Can be improved using extra info (minor case components, and boolean flags)
				// TODO: It's also possible to create a direct conversion to the xyY color space
				return (DataSource as CIEXYZ) ?? new CIEXYZ (
					1.5 * U, V, 1.5 * U - 3 * V + 2 * W, DataSource ?? this
				);
			}

			/**
			 * <inheritdoc />
			 */
			public override CIE1960 ToCIEUCS ()
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
				CIE1960 ucsObj = obj as CIE1960; 

				if (ucsObj == this) {
					return true;
				}
				if (ucsObj == null || GetHashCode () != obj.GetHashCode ()) {
					return false;
				}

				return (U == ucsObj.U && V == ucsObj.V && W == ucsObj.W);
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
