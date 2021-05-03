using System;

namespace rm.FeatureToggle
{
	/// <summary>
	/// Defines dial methods for guid.
	/// </summary>
	public interface IGuidDial
	{
		/// <summary>
		/// Returns true if the calculated value of <paramref name="guid"/> [0.01-100.00]
		/// is less than or equal to <paramref name="percentage"/>. The calculated value
		/// is deterministic.
		/// </summary>
		bool ToDial(Guid guid, double percentage);
	}
}
