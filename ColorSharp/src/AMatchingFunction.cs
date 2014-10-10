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


namespace Litipk.ColorSharp
{
	public abstract class AMatchingFunction : IRealFunctionWithFiniteSupport
	{
		#region shared methods

		public double DoConvolution (ALightSpectrum lss)
		{
			double minWavelength = Math.Max (this.GetSupportMinValue (), lss.GetSupportMinValue ());
			double maxWavelength = Math.Min (this.GetSupportMaxValue (), lss.GetSupportMaxValue ());
			int numberOfPartitions = Math.Max (this.GetNumberOfDataPoints (), lss.GetNumberOfDataPoints ());

			if (numberOfPartitions == -1) {
				numberOfPartitions = ((int)Math.Round (maxWavelength-minWavelength)) + 2;
			}

			return MathNet.Numerics.Integration.NewtonCotesTrapeziumRule.IntegrateComposite (
				waveLength => this.EvaluateAt(waveLength)*lss.EvaluateAt(waveLength),
				minWavelength,
				maxWavelength,
				numberOfPartitions
			);
		}

		#endregion


		#region abstract methods to be implemented in subclasses

		/**
		 *
		 */
		public abstract double EvaluateAt (double waveLength);

		/**
		 * We need to know the matching function support in our algorithms.
		 * Analytic aproximations also have their confidence intervals, so
		 * there aren't exceptional cases here.
		 */
		public abstract double GetSupportMinValue ();
		public abstract double GetSupportMaxValue ();

		/**
		 * We need to know how many data points we have to make computations using all the information we have.
		 * If the concrete implementation is "analytical", then must return -1.
		 */
		public abstract int GetNumberOfDataPoints();

		#endregion
	}
}

