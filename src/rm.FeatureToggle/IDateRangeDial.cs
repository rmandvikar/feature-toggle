using System;

namespace rm.FeatureToggle
{
	/// <summary>
	/// Defines dial methods with date range.
	/// </summary>
	public interface IDateRangeDial
	{
		/// <summary>
		/// Returns true for date range <paramref name="startDate"/> and <paramref name="endDate"/>,
		/// and false otherwise.
		/// </summary>
		bool ToDial(DateTimeOffset? startDate = null, DateTimeOffset? endDate = null);
	}
}
