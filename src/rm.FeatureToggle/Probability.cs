using System;

namespace rm.FeatureToggle
{
	/// <inheritdoc cref="IProbability"/>
	public class Probability : IProbability
	{
		private readonly Random rng;

		/// <param name="rng">
		/// rng.
		/// <para>Note: <paramref name="rng"/> needs to be thread-safe.</para>
		/// </param>
		public Probability(Random rng)
		{
			this.rng = rng
				?? throw new ArgumentNullException(nameof(rng));
		}

		/// <inheritdoc/>
		public bool IsTrue(double percentage)
		{
			if (percentage <= 0)
			{
				return false;
			}
			if (percentage >= 100)
			{
				return true;
			}

			return rng.NextPercentage() <= percentage;
		}
	}
}
