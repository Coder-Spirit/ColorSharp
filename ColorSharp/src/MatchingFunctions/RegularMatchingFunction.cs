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
	namespace MatchingFunctions
	{
		public class RegularMatchingFunction : AMatchingFunction
		{
			#region readonly properties

			// Needed values to interpret the data points
			public readonly double NmPerStep;
			public readonly double MinWaveLength;
			public readonly double MaxWaveLength;

			// Data points
			public readonly double[] Amplitudes;
			#endregion


			#region constructors

			// Constructor
			public RegularMatchingFunction (double minWaveLength, double maxWaveLength, double[] amplitudes)
			{
				MinWaveLength = minWaveLength;
				MaxWaveLength = maxWaveLength;
				NmPerStep     = (maxWaveLength - minWaveLength) / (amplitudes.Length - 1);
				Amplitudes    = amplitudes;
			}

			// Constructor
			public RegularMatchingFunction (double minWaveLength, double[] amplitudes, double nmPerStep)
			{
				NmPerStep     = nmPerStep;
				MinWaveLength = minWaveLength;
				MaxWaveLength = minWaveLength + nmPerStep * (amplitudes.Length - 1);
				Amplitudes    = amplitudes;
			}

			#endregion


			#region AMatchingFunction implementation

			public override double EvaluateAt (double waveLength)
			{
				if (waveLength >= MinWaveLength && waveLength <= MaxWaveLength) {
					double dblIndex = (waveLength - MinWaveLength) / NmPerStep;
					double floorIndex = Math.Floor (dblIndex);
					uint uIndex = (uint)floorIndex;

					if (dblIndex - floorIndex <= 2*double.Epsilon) {
						return Amplitudes [uIndex];
					}

					double alpha = (dblIndex - floorIndex);

					return Amplitudes [uIndex] + alpha * (Amplitudes [uIndex + 1] - Amplitudes [uIndex]);
				}

				// TODO: add extrapolation
				throw new ArgumentOutOfRangeException ();
			}

			public override double GetSupportMinValue ()
			{
				return MinWaveLength;
			}

			public override double GetSupportMaxValue ()
			{
				return MaxWaveLength;
			}

			public override int GetNumberOfDataPoints()
			{
				return Amplitudes.Length;
			}

			#endregion
		}
	}
}
