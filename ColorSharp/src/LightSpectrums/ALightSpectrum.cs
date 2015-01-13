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
using Litipk.ColorSharp.MatchingFunctions;
using Litipk.ColorSharp.InternalUtils;


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

			#region IRealFunctionWithFiniteSupport methods

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

			/**
			 * <inheritdoc />
			 */
			public abstract int GetNumberOfDataPoints();

			#endregion

			/**
			 * <summary>
			 * Supposing the light spectrum we have is a discrete sample, this gives us the next data point.
			 * If the method returns -1.0 , then we suppose we have an "analytic" spectrum, so we don't have samples.
			 * </summary>
			 */
			public abstract double GetNextAmplitudeSample (double waveLength);

			#endregion


			#region AConvertibleColor methods

			/**
			 * <inheritdoc />
			 */
			public override CIEXYZ ToCIEXYZ (ConversionStrategy strategy=ConversionStrategy.Default)
			{
				AMatchingFunction[] MFs;

				if ((strategy & ConversionStrategy.WaveLength1NmStep) != 0) {
					MFs = new AMatchingFunction[] {
						CIE1931XYZ1NmMatchingFunctionX.Instance,
						CIE1931XYZ1NmMatchingFunctionY.Instance,
						CIE1931XYZ1NmMatchingFunctionZ.Instance
					};
				} else { // By default we choose 5 Nm
					MFs = new AMatchingFunction[] {
						CIE1931XYZ5NmMatchingFunctionX.Instance,
						CIE1931XYZ5NmMatchingFunctionY.Instance,
						CIE1931XYZ5NmMatchingFunctionZ.Instance
					};
				}

				return new CIEXYZ (
					MFs [0].DoConvolution (this), MFs [1].DoConvolution (this), MFs [2].DoConvolution (this), DataSource ?? this
				);
			}

			#endregion
		}
	}
}
