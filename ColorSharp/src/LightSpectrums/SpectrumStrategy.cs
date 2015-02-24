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


namespace Litipk.ColorSharp
{
	namespace LightSpectrums
	{
		/**
		 * <summary>Enum type used to specify alternative strategies in color conversion.</summary>
		 */
		[Flags]
		public enum SpectrumStrategy
		{
			WaveLength1NmStep = 0,
			WaveLength5NmStep = 1,

			TwoDegs = 2,
			TenDegs = 4,

			Nm1Deg2 = WaveLength1NmStep | TwoDegs,
			Nm5Deg2 = WaveLength5NmStep | TwoDegs,
			Nm1Deg10 = WaveLength1NmStep | TenDegs,
			Nm5Deg10 = WaveLength5NmStep | TenDegs,

			Default = WaveLength5NmStep | TwoDegs
		}
	}
}

