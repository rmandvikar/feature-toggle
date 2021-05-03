using System;
using System.Numerics;

namespace rm.FeatureToggle
{
	/// <inheritdoc cref="IGuidDial"/>
	public class GuidDial : IGuidDial
	{
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
			// note: guid.GetHashCode() does not give good results
			var guidNumber = new BigInteger(guid.ToByteArray());
			guidNumber = guidNumber >= 0 ? guidNumber : -guidNumber;
			return ((int)(guidNumber % 100_00) + 1) / (double)100;
		}
	}
}
