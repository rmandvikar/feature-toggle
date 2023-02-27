using System;
using rm.Clock;

namespace rm.FeatureToggle;

/// <inheritdoc cref="IDateRangeDial"/>
public class DateRangeDial : IDateRangeDial
{
	private readonly ISystemClock clock;

	public DateRangeDial(
		ISystemClock clock)
	{
		this.clock = clock
			?? throw new ArgumentNullException(nameof(clock));
	}

	/// <inheritdoc/>
	public bool ToDial(DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
	{
		// don't throw for convenience if startDate > endDate, return false
		if (startDate != null && endDate != null
			&& startDate > endDate)
		{
			return false;
		}
		var utcNow = clock.UtcNow;
		return
			(startDate == null || startDate <= utcNow) &&
			(endDate == null || utcNow <= endDate);
	}
}
