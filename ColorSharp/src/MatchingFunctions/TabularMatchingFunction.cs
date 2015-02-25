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

using Litipk.ColorSharp.InternalUtils;


namespace Litipk.ColorSharp
{
	namespace MatchingFunctions
	{
		/**
		 * <summary>Useful to define matching functions with irregular spaced data points.</summary>
		 */
		public class TabularMatchingFunction : AMatchingFunction
		{
			#region private properties

			readonly List<KeyValuePair<double, double>> TabularData;

			#endregion

			#region constructors

			/**
			 * <summary>Useful to define matching functions with irregular spaced data points.</summary>
			 * <param name="tabularData">irregular spaced data points</param>
			 */
			public TabularMatchingFunction (List<KeyValuePair<double, double>> tabularData)
			{
				TabularData = tabularData;
			}

			#endregion

			#region AMatchingFunction implementation

			/**
			 * <inheritdoc />
			 */
			public override double EvaluateAt(double waveLength)
			{
				if (waveLength < TabularData[0].Key || waveLength > TabularData[TabularData.Count-1].Key) {
					// TODO: Add extrapolation?
					throw new ArgumentOutOfRangeException();
				}

				int index = TabularData.BinarySearch(
					new KeyValuePair<double, double>(waveLength, 0), new KeyValuePairComparer()
				);

				if (index > 0)
					return TabularData [index].Value;

				index = ~index;

				double alpha = (
				   waveLength - TabularData [index].Key
				) / (
				   TabularData [index + 1].Key - TabularData [index].Key
				);

				return TabularData [index].Value + alpha * (
				    TabularData [index + 1].Value - TabularData [index].Value
				);
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetSupportMinValue ()
			{
				return TabularData [0].Key;
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetSupportMaxValue ()
			{
				return TabularData [TabularData.Count - 1].Key;
			}

			/**
			 * <inheritdoc />
			 */
			public override int GetNumberOfDataPoints()
			{
				return TabularData.Count;
			}

			#endregion
		}
	}
}
