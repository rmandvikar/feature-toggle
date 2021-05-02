using System;

namespace rm.FeatureToggle
{
	/// <inheritdoc cref="ICountThenPercentageDial"/>
	public class CountThenPercentageDial : ICountThenPercentageDial
	{
		private readonly ICountDial countDial;
		private readonly IPercentageDial percentageDial;

		public CountThenPercentageDial(Random rng)
		{
			_ = rng
				?? throw new ArgumentNullException(nameof(rng));
			countDial = new CountDial();
			percentageDial = new PercentageDial(rng);
		}

		/// <inheritdoc/>
		public bool ToDial(long count, double percentage)
		{
			return countDial.ToDial(count)
				|| percentageDial.ToDial(percentage);
		}
	}
}
