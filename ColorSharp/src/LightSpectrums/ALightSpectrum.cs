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
	using ColorSpaces;
	using MatchingFunctions;
	using InternalUtils;

	namespace LightSpectrums
	{
		public abstract class ALightSpectrum : AConvertibleColor, IRealFunctionWithFiniteSupport
		{
			#region constructors

			protected ALightSpectrum(AConvertibleColor dataSource=null) : base(dataSource) {
				Conversors.Add (typeof(CIEXYZ), ToCIEXYZ);
			}

			#endregion


			#region inheritable methods

			#region IRealFunctionWithFiniteSupport methods

			/**
			 * This gives us the wave amplitude at a given wave length, if it's necessary the method will do interpolation.
			 */
			public abstract double EvaluateAt(double waveLength);

			// Analytic aproximations also have their confidence intervals, so
			// there aren't exceptional cases here.
			public abstract double GetSupportMinValue ();
			public abstract double GetSupportMaxValue ();

			// 
			public abstract double GetMaxValueOnSupport ();

			// We need to know how many data points we have to make computations using all the information we have.
			// If the concrete implementation is "analytical", then must return -1.
			public abstract int GetNumberOfDataPoints();

			#endregion

			/**
			 * Supposing the light spectrum we have is a discrete sample, this gives us the next data point.
			 * If the method returns -1.0 , then we suppose we have an "analytic" spectrum, so we don't have samples.
			 */
			public abstract double GetNextAmplitudeSample (double waveLength);

			#endregion


			#region conversors

			public CIEXYZ ToCIEXYZ (ConversionStrategy strategy=ConversionStrategy.Default)
			{
				// TODO : Check ConversionStrategy

				AMatchingFunction[] MFs = {
					CIE1931XYZ5NmMatchingFunctionX.Instance,
					CIE1931XYZ5NmMatchingFunctionY.Instance,
					CIE1931XYZ5NmMatchingFunctionZ.Instance
				};

				return new CIEXYZ (
					MFs [0].DoConvolution (this), MFs [1].DoConvolution (this), MFs [2].DoConvolution (this), DataSource ?? this
				);
			}

			#endregion
		}
	}
}
