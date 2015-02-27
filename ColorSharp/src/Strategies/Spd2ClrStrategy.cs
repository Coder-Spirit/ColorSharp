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
	namespace Strategies
	{
		/**
		 * <summary>Enum type used to specify alternative strategies in spd->color conversion.</summary>
		 */
		[Flags]
		public enum Spd2ClrStrategy
		{
			/// <summary>Use the 1nm step matching functions to obtain the chromaticity components of the spectrum.</summary>
			WaveLength1NmStep = 0,

			/// <summary>Use the 5nm step matching functions to obtain the chromaticity components of the spectrum.</summary>
			WaveLength5NmStep = 1,

			/// <summary>Use the 2º matching functions to obtain the chromaticity components of the spectrum.</summary>
			TwoDegs = 2,

			/// <summary>Use the 10º matching functions to obtain the chromaticity components of the spectrum.</summary>
			TenDegs = 4,

			/// <summary>Combination of WaveLength1NmStep and TwoDegs</summary>
			Nm1Deg2 = WaveLength1NmStep | TwoDegs,

			/// <summary>Combination of WaveLength5NmStep and TwoDegs</summary>
			Nm5Deg2 = WaveLength5NmStep | TwoDegs,

			/// <summary>Combination of WaveLength1NmStep and TenDegs</summary>
			Nm1Deg10 = WaveLength1NmStep | TenDegs,

			/// <summary>Combination of WaveLength5NmStep and TenDegs</summary>
			Nm5Deg10 = WaveLength5NmStep | TenDegs,

			/// <summary>Nm5Deg2</summary>
			Default = Nm5Deg2
		}
	}
}

