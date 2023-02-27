using System;
using System.Numerics;
using System.Text;

namespace rm.FeatureToggle;

/// <inheritdoc cref="IPercentValueCalculator"/>
internal class PercentValueCalculator : IPercentValueCalculator
{
	/// <inheritdoc/>
	public double Calculate(string id)
	{
		var bytes = Encoding.ASCII.GetBytes(id);

		// 0.01-100.00
		// note: GetHashCode()) seems like an ideal candidate
		// but hashCode could be different across app domains, processes,
		// platforms, .net implementations, etc
		var n = new BigInteger(bytes);
		n = BigInteger.Abs(n);
		return ((int)(n % 100_00) + 1) / (double)100;
	}
}
