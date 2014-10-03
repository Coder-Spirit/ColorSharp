using System;

namespace Litipk
{
	namespace ColorSharp
	{
		public interface IConvertibleColor
		{
			// Tells us the number of needed steps to convert the object to another color space.
			int conversionPathLength(Type[] visited=null);

			// Converts the object to another color space
			T convertTo<T> () where T : IConvertibleColor;
		}
	}
}
