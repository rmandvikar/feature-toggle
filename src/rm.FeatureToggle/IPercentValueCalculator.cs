namespace rm.FeatureToggle;

/// <summary>
/// Defines methods to deterministically calculate percent value.
/// </summary>
internal interface IPercentValueCalculator
{
	/// <summary>
	/// Returns a deterministic value [0.01, 100.00] for given <paramref name="id"/>.
	/// </summary>
	double Calculate(string id);
}
