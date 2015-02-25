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


using Litipk.ColorSharp.InternalUtils;


namespace Litipk.ColorSharp
{
	namespace MatchingFunctions
	{
		/**
		 * <summary>
		 * Abstract class to handle 'matching functions'. The matching functions are used to transform spectrums into
		 * simpler parametric spaces (with information loss), like CIE's 1931 XYZ color space.
		 * </summary>
		 */
		public abstract class AMatchingFunction : IRealFunctionWithFiniteSupport
		{
			#region abstract methods to be implemented in subclasses

			/**
			 * <summary>Returns the value of the matching function for a given wave length.</summary>
			 */
			public abstract double EvaluateAt (double waveLength);

			/**
			 * <summary>The matching function has a support interval. This method gives us its lower bound.</summary>
			 */
			public abstract double GetSupportMinValue ();

			/**
			 * <summary>The matching function has a support interval. This method gives us its upper bound.</summary>
			 */
			public abstract double GetSupportMaxValue ();

			/**
			 * <summary>
			 * Returns us the number of data points used to construct the matching function. It returns -1 if the inner
			 * implementation is analytical and there aren't data points.
			 * </summary>
			 */
			public abstract int GetNumberOfDataPoints();

			#endregion
		}
	}
}

