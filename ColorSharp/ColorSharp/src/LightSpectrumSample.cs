namespace Litipk.ColorSharp
{
	public class LightSpectrumSample : AConvertibleColor
	{
		#region private properties

		// Needed values to interpret the data points
		double nmPerStep;
		double minWaveLength;
		double maxWaveLength;

		// Data points
		double[] amplitudes;
		#endregion

		#region constructors

		/**
		 * This constructor "installs" the conversor methods into the instance
		 */
		protected LightSpectrumSample(AConvertibleColor dataSource=null) : base(dataSource) {
			// TODO: Add conversors
		}

		// Constructor
		public LightSpectrumSample (double minWaveLength, double maxWaveLength, double[] amplitudes, AConvertibleColor dataSource=null) : this(dataSource)
		{
			this.minWaveLength = minWaveLength;
			this.maxWaveLength = maxWaveLength;
			nmPerStep = (maxWaveLength - minWaveLength) / (amplitudes.Length - 1);
			this.amplitudes = amplitudes;
		}

		// Constructor
		public LightSpectrumSample (double minWaveLength, double[] amplitudes, double nmPerStep, AConvertibleColor dataSource=null) : this(dataSource)
		{
			this.nmPerStep = nmPerStep;
			this.minWaveLength = minWaveLength;
			maxWaveLength = minWaveLength + (nmPerStep) * (amplitudes.Length - 1);
			this.amplitudes = amplitudes;
		}

		#endregion

		#region conversors

		public CIEXYZ toCIEXYZ ()
		{
			// TODO
			return null;
		}

		#endregion
	}
}
