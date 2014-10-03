using System;
using System.Collections.Generic;

namespace Litipk
{
	namespace ColorSharp
	{
		// TODO: Add optional parameter (ConversionStrategy)
		delegate T Conversor<out T> () where T : IConvertibleColor;

		public class LightSpectrumSample : IConvertibleColor
		{
			// Needed values to interpret the data points
			double nmPerStep;
			double minWaveLength;
			double maxWaveLength;

			// Data points
			double[] amplitudes;

			// If this "color" comes from another data source, then we keep the original data.
			IConvertibleColor dataSource = null;

			// Conversors to other "color spaces".
			Dictionary<Type, Conversor<IConvertibleColor>> conversors;

			// Constructor
			public LightSpectrumSample (double minWaveLength, double maxWaveLength, double[] amplitudes, IConvertibleColor dataSource=null)
			{
				this.minWaveLength = minWaveLength;
				this.maxWaveLength = maxWaveLength;
				nmPerStep = (maxWaveLength - minWaveLength) / (amplitudes.Length - 1);
				this.amplitudes = amplitudes;

				this.dataSource = dataSource;
			}

			// Constructor
			public LightSpectrumSample (double minWaveLength, double[] amplitudes, double nmPerStep, IConvertibleColor dataSource=null)
			{
				this.nmPerStep = nmPerStep;
				this.minWaveLength = minWaveLength;
				maxWaveLength = minWaveLength + (nmPerStep) * (amplitudes.Length - 1);
				this.amplitudes = amplitudes;

				this.dataSource = dataSource;
			}

			public int conversionPathLength (Type[] visited = null)
			{
				throw new NotImplementedException ();
			}

			public T convertTo<T>() where T : IConvertibleColor
			{
				Type t = typeof(T);

				//if (conversors.ContainsKey (t)) {
					return (T)conversors [t]();
				//}
			}
		}
	}
}
