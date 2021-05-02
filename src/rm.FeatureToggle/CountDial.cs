using System;

namespace rm.FeatureToggle
{
	/// <inheritdoc cref="ICountDial"/>
	public class CountDial : ICountDial
	{
		private readonly object _lock = new object();
		private long i;

		/// <inheritdoc/>
		public bool ToDial(long count)
		{
			if (count <= 0)
			{
				return false;
			}
			lock (_lock)
			{
				if (i == count)
				{
					return false;
				}
				else if (i < count)
				{
					i++;
					return true;
				}
				else // i > count
				{
					throw new InvalidOperationException($"{nameof(i)} > {nameof(count)}");
				}
			}
		}
	}
}
