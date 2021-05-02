namespace rm.FeatureToggle
{
	/// <summary>
	/// Defines dial methods with count.
	/// </summary>
	public interface ICountDial
	{
		/// <summary>
		/// Returns true for first <paramref name="count"/> calls, and false after.
		/// </summary>
		bool ToDial(long count);
	}
}
