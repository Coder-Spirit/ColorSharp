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


using Litipk.ColorSharp.ColorSpaces;
using Litipk.ColorSharp.InternalUtils;
using Litipk.ColorSharp.MatchingFunctions;
using Litipk.ColorSharp.Strategies;


namespace Litipk.ColorSharp
{
	namespace LightSpectrums
	{
		/**
		 * <summary>Abstract class that provides basic methods to manipulate light spectrums.</summary>
		 */
		public abstract class ALightSpectrum : AConvertibleColor, IRealFunctionWithFiniteSupport
		{
			/**
			 * Boilerplate constructor
			 */
			protected ALightSpectrum(AConvertibleColor dataSource=null) : base(dataSource) { }

			#region abstract methods

			/**
			 * <inheritdoc />
			 */
			public abstract double EvaluateAt(double waveLength);

			/**
			 * <inheritdoc />
			 */
			public abstract double GetSupportMinValue ();

			/**
			 * <inheritdoc />
			 */
			public abstract double GetSupportMaxValue ();

			/**
			 * <inheritdoc />
			 */
			public abstract double GetMaxValueOnSupport ();

			#endregion


			#region AConvertibleColor methods

			/**
			 * <inheritdoc />
			 */
			public override CIEXYZ ToCIEXYZ ()
			{
				// We don't need the Color Conversion strategy here, but the Spectrum Conversion strategy
				return ToCIEXYZ (Spd2ClrStrategy.Default);
			}

			/**
			 * <see cref="ToCIEXYZ(ColorStrategy)"/>
			 * <param name="strategy">Strategy used to obtain the tristimulous values</param>
			 */
			public CIEXYZ ToCIEXYZ (Spd2ClrStrategy strategy=Spd2ClrStrategy.Default)
			{
				AMatchingFunction[] MFs;

				int step;
				if (strategy == Spd2ClrStrategy.Nm1Deg2) {
					step = 1;
					MFs = new AMatchingFunction[] {
						CIE1931XYZ1Nm2DegX.Instance, CIE1931XYZ1Nm2DegY.Instance, CIE1931XYZ1Nm2DegZ.Instance
					};
				} else if (strategy == Spd2ClrStrategy.Nm1Deg10) {
					step = 1;
					MFs = new AMatchingFunction[] {
						CIE1964XYZ1Nm10DegX.Instance, CIE1964XYZ1Nm10DegY.Instance, CIE1964XYZ1Nm10DegZ.Instance
					};
				} else if (strategy == Spd2ClrStrategy.Nm5Deg10) {
					step = 5;
					MFs = new AMatchingFunction[] {
						CIE1964XYZ5Nm10DegX.Instance, CIE1964XYZ5Nm10DegY.Instance, CIE1964XYZ5Nm10DegZ.Instance
					};
				} else { // if (strategy == SpectrumStrategy.Nm5Deg2) {
					step = 5;
					MFs = new AMatchingFunction[] {
						CIE1931XYZ5Nm2DegX.Instance, CIE1931XYZ5Nm2DegY.Instance, CIE1931XYZ5Nm2DegZ.Instance
					};
				}

				double X = 0.0, Y = 0.0, Z = 0.0;
				int n = MFs [0].GetNumberOfDataPoints ();
				double minV = MFs [0].GetSupportMinValue ();

				for (int i = 0; i < n; i++) {
					if (i * step + minV < GetSupportMinValue ()) {
						continue;
					}
					if (i * step + minV > GetSupportMaxValue ()) {
						break;
					}

					double v = EvaluateAt (i * step + minV);

					X += MFs [0].EvaluateAt (i * step + minV) * v * step;
					Y += MFs [1].EvaluateAt (i * step + minV) * v * step;
					Z += MFs [2].EvaluateAt (i * step + minV) * v * step;
				}

				return new CIEXYZ (X, Y, Z, DataSource ?? this);
			}

			#endregion
		}
	}
}
