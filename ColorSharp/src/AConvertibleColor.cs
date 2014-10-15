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
	// TODO: Add optional parameter (ConversionStrategy)
	public delegate T Conversor<out T> (Dictionary<KeyValuePair<Type, Type>, object> strategies=null) where T : AConvertibleColor;

	public abstract class AConvertibleColor
	{
		#region constants

		const int MAX_ACCEPTABLE_PATH_LENGH = 32;

		#endregion


		#region properties

		// If this "color" comes from another data source, then we keep the original data.
		protected AConvertibleColor DataSource = null;

		// Conversor delegates
		protected readonly Dictionary<Type, Conversor<AConvertibleColor>> Conversors = new Dictionary<Type, Conversor<AConvertibleColor>>();

		#endregion


		#region constructors

		// TODO: Check if this should be public in order to work as expected with reflection.
		protected AConvertibleColor(AConvertibleColor dataSource = null)
		{
			DataSource = dataSource;
		}

		#endregion


		#region conversion skeleton

		// Tells us the number of needed steps to convert the object to another color space.
		int ConversionPathLength(Type target, List<Type> visited=null)
		{
			if (Conversors.ContainsKey (target)) {
				// TODO: This should depend on the inclusion relations between color spaces
				return 1;
			}

			int pathLength = MAX_ACCEPTABLE_PATH_LENGH + 1;

			Type tt = this.GetType ();

			if (visited == null) {
				visited = new List<Type> { tt };
			} else {
				// We make a copy to avoid problems
				visited = new List<Type> (visited);
				if (!visited.Contains (tt)) {
					visited.Add (tt);
				}
			}

			foreach (Type k in Conversors.Keys) {
				if (visited.Contains (k)) {
					continue;
				}

				int tmpCpl = 1 + ((AConvertibleColor)Activator.CreateInstance(k)).ConversionPathLength (
					target, visited
				);

				pathLength = Math.Min (pathLength, tmpCpl);
			}

			return pathLength;
		}

		public T ConvertTo<T> (List<Type> visited = null) where T : AConvertibleColor
		{
			int basePathLength, dataSourcePathLength;

			Type t = typeof(T);
			Type tt = this.GetType ();
			Type ist = null; // Intermediate color space type

			if (t == tt) {
				// Dumb conversion
				return (T)(AConvertibleColor)this;
			}

			// We are going to compare the conversion path lengths in order to chose the best option
			// (with least precission loss).
			if (DataSource != null) {
				if (DataSource.GetType () == t) {
					// Recovering original data
					return (T)DataSource;
				}

				dataSourcePathLength = DataSource.ConversionPathLength (t, new List<Type> {tt});
			} else {
				dataSourcePathLength = MAX_ACCEPTABLE_PATH_LENGH;
			}

			if (visited == null) {
				visited = new List<Type> { tt };
			} else {
				// We make a copy to avoid problems
				visited = new List<Type> (visited);
				if (!visited.Contains (tt)) {
					visited.Add (tt);
				}
			}

			if (Conversors.ContainsKey (t)) {
				// We don't use the direct conversor because maybe we can use the dataSource direct conversor
				basePathLength = 1;
			} else {
				basePathLength = MAX_ACCEPTABLE_PATH_LENGH + 1;

				foreach(Type k in Conversors.Keys)
				{
					if (visited.Contains (k)) {
						continue;
					}

					int tmpCpl = 1 + ((AConvertibleColor)Activator.CreateInstance(k)).ConversionPathLength (
						t, visited
					);

					if (tmpCpl < basePathLength) {
						ist = k;
						basePathLength = tmpCpl;
					}
				}
			}

			if (dataSourcePathLength > MAX_ACCEPTABLE_PATH_LENGH && basePathLength > MAX_ACCEPTABLE_PATH_LENGH) {
				throw new InvalidCastException ("Unable to find a conversion path to this color space.");
			}

			if (DataSource != null && dataSourcePathLength <= basePathLength + 1) {
				return DataSource.ConvertTo<T> ();
			}

			if (basePathLength == 1) {
				return (T)Conversors [ist] ();
			}

			visited.Add (ist);
			return Conversors [ist] ().ConvertTo<T>(visited);
		}

		#endregion


		#region abstract methods

		public abstract bool IsInsideColorSpace ();

		#endregion
	}
}
