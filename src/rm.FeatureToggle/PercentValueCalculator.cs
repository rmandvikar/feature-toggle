using System.Text;
using K4os.Hash.xxHash;

namespace rm.FeatureToggle;

/// <inheritdoc cref="IPercentValueCalculator"/>
internal class PercentValueCalculator : IPercentValueCalculator
{
	/// <inheritdoc/>
	public double Calculate(string id)
	{
		var bytes = Encoding.ASCII.GetBytes(id);

		var bytesLength = bytes.Length;
		if (bytesLength == 0)
		{
			return 0d;
		}

		// 0.01-100.00
		// note: GetHashCode()) seems like an ideal candidate
		// but hashCode could be different across app domains, processes,
		// platforms, .net implementations, etc
		var n = XXH64.DigestOf(bytes);
		return ((int)(n % 100_00) + 1) / (double)100;
	}
}
