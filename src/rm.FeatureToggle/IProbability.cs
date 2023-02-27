namespace rm.FeatureToggle;

/// <summary>
/// Defines probability methods.
/// </summary>
public interface IProbability
{
	/// <summary>
	/// Returns true with a <paramref name="percentage"/> probability.
	/// </summary>
	/// <param name="percentage">Number [0.00, 100.00] with 2 decimal places precision.</param>
	bool IsTrue(double percentage);
}
