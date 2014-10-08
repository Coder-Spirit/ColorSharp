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
	public abstract class ALightSpectrum : AConvertibleColor
	{
		#region constructors

		protected ALightSpectrum(AConvertibleColor dataSource=null) : base(dataSource) {
			// TODO: Add conversors
		}

		#endregion


		#region inheritable methods

		/**
		 * This gives us the wave amplitude at a given wave length, if it's necessary the method will do interpolation.
		 */
		public abstract double GetAmplitudeAt(double waveLength);

		/**
		 * Supposing the light spectrum we have is a discrete sample, this gives us the next data point.
		 * If the method returns -1.0 , then we suppose we have an "analytic" spectrum, so we don't have samples.
		 */
		public abstract double GetNextAmplitudeSample (double waveLength);

		#endregion


		#region conversors

		public CIEXYZ ToCIEXYZ (Dictionary<KeyValuePair<Type, Type>, object> strategies=null)
		{
			var strategyKey = new KeyValuePair<Type, Type> (
				typeof(ALightSpectrum), typeof(CIEXYZ)
			);

			if (strategies == null || !strategies.ContainsKey(strategyKey)) {
				throw new ArgumentException (
					"Unable top find the proper strategy"
				);
			}

			var MFs = (IMatchingFunction[])strategies [strategyKey];

			if (MFs == null || MFs.Length != 3) {
				throw new ArgumentException (
					"Unable top find the matching functions"
				);
			}

			return new CIEXYZ (
				MFs [0].DoConvolution (this), MFs [1].DoConvolution (this), MFs [2].DoConvolution (this), DataSource ?? this
			);
		}

		#endregion
	}
}
