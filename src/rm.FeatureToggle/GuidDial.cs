using System;

namespace rm.FeatureToggle
{
	/// <inheritdoc cref="IGuidDial"/>
	public class GuidDial : IGuidDial
	{
		private readonly IPercentValueCalculator percentValueCalculator = new PercentValueCalculator();

		/// <inheritdoc/>
		public bool ToDial(Guid guid, double percentage)
		{
			if (percentage <= 0)
			{
				return false;
			}
			if (percentage >= 100)
			{
				return true;
			}

			return GetPercentValue(guid) <= percentage;
		}

		private double GetPercentValue(Guid guid)
		{
			// 0.01-100.00
			// note: int abs(guid.GetHashCode()) seems like an ideal candidate
			// but hashCode could be different across app domains, processes,
			// platforms, .net implementations, etc.
			var bytes = guid.ToByteArrayMatchingStringRepresentation();
			return percentValueCalculator.Calculate(bytes);
		}
	}
}
