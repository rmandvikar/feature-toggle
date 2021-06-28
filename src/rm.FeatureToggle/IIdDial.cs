namespace rm.FeatureToggle
{
	/// <summary>
	/// Defines dial methods for id.
	/// </summary>
	public interface IIdDial
	{
		/// <summary>
		/// Returns true if the calculated value of <paramref name="id"/> [0.01-100.00]
		/// is less than or equal to <paramref name="percentage"/>. The calculated value
		/// is deterministic. <paramref name="id"/> needs to be UTF8-encoded.
		/// </summary>
		bool ToDial(string id, double percentage);
	}
}
