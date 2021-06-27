namespace rm.FeatureToggle
{
	/// <summary>
	/// Defines methods to deterministically calculate percent value.
	/// </summary>
	public interface IPercentValueCalculator
	{
		/// <summary>
		/// Returns a deterministic value [0.01, 100.00] for given <paramref name="bytes"/>.
		/// </summary>
		double Calculate(byte[] bytes);
	}
}
