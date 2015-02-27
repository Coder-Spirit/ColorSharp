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
		 * <summary>Enum type used to specify alternative strategies in color conversion.</summary>
		 */
		[Flags]
		public enum ToSmallSpaceStrategy
		{
			/// <summary>If the color can't be represented in the new color space, an exception will be thrown.</summary>
			NoForce           = 0,

			/// <summary>
			/// "Truncates" component values in the new color space if they're lower than the new color space's lower boundaries.
			/// </summary>
			ForceLowTruncate  = 2,

			/// <summary>
			/// "Stretches" component values in the new color space if they're lower than the new color space's lower boundaries.
			/// </summary>
			ForceLowStretch   = 4,

			/// <summary>
			/// "Truncates" component values in the new color space if they're lower than the new color space's upper boundaries.
			/// </summary>
			ForceHighTruncate = 8,

			/// <summary>
			/// "Stretches" component values in the new color space if they're lower than the new color space's upper boundaries.
			/// </summary>
			ForceHighStretch  = 16,

			/// <summary>Combination of ForceLowStretch and ForceLowTruncate</summary>
			ForceLow          = ForceLowStretch | ForceLowTruncate,

			/// <summary>Combination of ForceHighStretch and ForceHighTruncate</summary>
			ForceHigh         = ForceHighStretch | ForceHighTruncate,

			/// <summary>Combination of ForceLowTruncate and ForceHighTruncate</summary>
			ForceTruncate     = ForceLowTruncate | ForceHighTruncate,

			/// <summary>Combination of ForceLowStretch and ForceHighStretch</summary>
			ForceStretch      = ForceLowStretch | ForceHighStretch,

			/// <summary>Combination of ForceLow and ForceHigh</summary>
			Force             = ForceLow | ForceHigh,

			/// <summary>NoForce</summary>
			Default = NoForce
		}
	}
}
