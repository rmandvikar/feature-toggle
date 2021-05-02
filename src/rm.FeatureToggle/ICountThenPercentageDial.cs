namespace rm.FeatureToggle
{
	/// <summary>
	/// Defines dial methods with count and then percentage.
	/// </summary>
	public interface ICountThenPercentageDial
	{
		/// <summary>
		/// Returns true for first <paramref name="count"/> calls, and then with a
		/// <paramref name="percentage"/> probability.
		/// </summary>
		bool ToDial(long count, double percentage);
	}
}
