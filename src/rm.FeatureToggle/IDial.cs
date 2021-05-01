namespace rm.FeatureToggle
{
	/// <summary>
	/// Defines dial methods.
	/// </summary>
	public interface IDial
	{
		/// <summary>
		/// Returns true with a <paramref name="percentage"/> probability.
		/// </summary>
		/// <param name="percentage">Number [0.00, 100.00] with 2 decimal places precision.</param>
		bool ToDial(double percentage);
	}
}
