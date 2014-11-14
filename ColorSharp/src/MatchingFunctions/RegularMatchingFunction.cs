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
using System.Collections.ObjectModel;


namespace Litipk.ColorSharp
{
	namespace MatchingFunctions
	{
		/**
		 * <summary>
		 * Implementation of AMatching function based on equally spaced data points.
		 * </summary>
		 */
		public class RegularMatchingFunction : AMatchingFunction
		{
			#region readonly properties

			/**
			 * <value>Number of nanometers between each data point.</value>
			 */
			public readonly double NmPerStep;

			/**
			 * <value>Minimum wave length with an associated data point.</value>
			 */
			public readonly double MinWaveLength;

			/**
			 * <value>Maximum wave length with an associated data point.</value>
			 */
			public readonly double MaxWaveLength;

			/**
			 * <value>Data points</value>
			 */
			public readonly ReadOnlyCollection<double> Amplitudes;
			#endregion


			#region constructors

			/**
			 * <summary>Creates a new regular matching function.</summary>
			 * <param name="minWaveLength">Lower boundary of the matching function's support.</param>
			 * <param name="maxWaveLength">Upper boundary of the matching function's support.</param>
			 * <param name="amplitudes">Data points.</param>
			 */
			public RegularMatchingFunction (double minWaveLength, double maxWaveLength, IList<double> amplitudes)
			{
				MinWaveLength = minWaveLength;
				MaxWaveLength = maxWaveLength;
				NmPerStep     = (maxWaveLength - minWaveLength) / (amplitudes.Count - 1);
				Amplitudes    =  new ReadOnlyCollection<double> (amplitudes);
			}

			/**
			 * <summary>Creates a new regular matching function.</summary>
			 * <param name="minWaveLength">Lower boundary of the matching function's support.</param>
			 * <param name="amplitudes">Data points.</param>
			 * <param name="nmPerStep">Number of nanometers between data points.</param>
			 */
			public RegularMatchingFunction (double minWaveLength, IList<double> amplitudes, double nmPerStep)
			{
				NmPerStep     = nmPerStep;
				MinWaveLength = minWaveLength;
				MaxWaveLength = minWaveLength + nmPerStep * (amplitudes.Count - 1);
				Amplitudes    =  new ReadOnlyCollection<double> (amplitudes);
			}

			#endregion


			#region AMatchingFunction implementation

			/**
			 * <inheritdoc />
			 */
			public override double EvaluateAt (double waveLength)
			{
				if (waveLength >= MinWaveLength && waveLength <= MaxWaveLength) {
					double dblIndex = (waveLength - MinWaveLength) / NmPerStep;
					double floorIndex = Math.Floor (dblIndex);
					int uIndex = (int)floorIndex;

					if (dblIndex - floorIndex <= 2*double.Epsilon) {
						return Amplitudes [uIndex];
					}

					double alpha = (dblIndex - floorIndex);

					return Amplitudes [uIndex] + alpha * (Amplitudes [uIndex + 1] - Amplitudes [uIndex]);
				}

				// TODO: add extrapolation
				throw new ArgumentOutOfRangeException ();
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetSupportMinValue ()
			{
				return MinWaveLength;
			}

			/**
			 * <inheritdoc />
			 */
			public override double GetSupportMaxValue ()
			{
				return MaxWaveLength;
			}

			/**
			 * <inheritdoc />
			 */
			public override int GetNumberOfDataPoints()
			{
				return Amplitudes.Count;
			}

			#endregion
		}
	}
}
