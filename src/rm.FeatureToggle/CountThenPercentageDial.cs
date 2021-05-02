using System;

namespace rm.FeatureToggle
{
	/// <inheritdoc cref="ICountThenPercentageDial"/>
	public class CountThenPercentageDial : ICountThenPercentageDial
	{
		private readonly ICountDial countDial;
		private readonly IPercentageDial percentageDial;

		public CountThenPercentageDial(ICountDial countDial, IPercentageDial percentageDial)
		{
			this.countDial = countDial
				?? throw new ArgumentNullException(nameof(countDial));
			this.percentageDial = percentageDial
				?? throw new ArgumentNullException(nameof(percentageDial));
		}

		/// <inheritdoc/>
		public bool ToDial(long count, double percentage)
		{
			return countDial.ToDial(count)
				|| percentageDial.ToDial(percentage);
		}
	}
}
