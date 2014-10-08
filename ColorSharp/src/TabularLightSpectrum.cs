namespace Litipk.ColorSharp
{
	public class TabularLightSpectrum : ALightSpectrum
	{
		#region inherited methods

		public override double GetAmplitudeAt(double waveLength)
		{
			return 0.0;
		}

		/**
		 * Supposing the light spectrum we have is a discrete sample, this gives us the next data point.
		 * If the method returns -1.0 , then we suppose we have an "analytic" spectrum, so we don't have samples.
		 */
		public override double GetNextAmplitudeSample (double waveLength)
		{
			return 0.0;
		}

		#endregion
	}
}

