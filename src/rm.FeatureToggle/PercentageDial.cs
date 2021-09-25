using System;

namespace rm.FeatureToggle
{
	/// <inheritdoc cref="IPercentageDial"/>
	public class PercentageDial : IPercentageDial
	{
		private readonly IProbability probability;

		/// <param name="rng">
		/// rng.
		/// <para>Note: <paramref name="rng"/> needs to be thread-safe.</para>
		/// </param>
		public PercentageDial(Random rng)
		{
			_ = rng
				?? throw new ArgumentNullException(nameof(rng));
			probability = new Probability(rng);
		}

		/// <inheritdoc/>
		public bool ToDial(double percentage)
		{
			return probability.IsTrue(percentage);
		}
	}
}
