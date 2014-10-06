namespace Litipk.ColorSharp
{
	public class CIEXYZ : AConvertibleColor
	{
		#region private properties

		double X, Y, Z;

		#endregion

		#region constructors

		/**
		 * This constructor "installs" the conversor methods into the instance
		 */
		protected CIEXYZ(AConvertibleColor dataSource=null) : base(dataSource) {
			// TODO: Add conversors
		}

		// Constructor
		public CIEXYZ (double X, double Y, double Z, AConvertibleColor dataSource=null) : this(dataSource)
		{
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}

		#endregion

		#region conversors

		public LightSpectrumSample toLightSpectrumSample ()
		{
			// TODO
			return null;
		}

		#endregion
	}
}

