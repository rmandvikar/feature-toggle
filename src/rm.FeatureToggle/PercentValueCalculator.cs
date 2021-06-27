using System;
using System.Numerics;

namespace rm.FeatureToggle
{
	/// <inheritdoc cref="IPercentValueCalculator"/>
	public class PercentValueCalculator : IPercentValueCalculator
	{
		/// <inheritdoc/>
		public double Calculate(byte[] bytes)
		{
			_ = bytes
				?? throw new ArgumentNullException(nameof(bytes));

			// 0.01-100.00
			// note: GetHashCode()) seems like an ideal candidate
			// but hashCode could be different across app domains, processes,
			// platforms, .net implementations, etc
			var n = new BigInteger(bytes);
			n = n >= 0 ? n : -n;
			return ((int)(n % 100_00) + 1) / (double)100;
		}
	}
}
