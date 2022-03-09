using System;

namespace rm.FeatureToggle
{
	/// <summary>
	/// Defines dial extensions.
	/// </summary>
	public static class DialExtensions
	{
		/// <summary>
		/// Returns a random percentage value in range [.01-100.00].
		/// </summary>
		public static double NextPercentage(this Random rng)
		{
			_ = rng
				?? throw new ArgumentNullException(nameof(rng));

			// .01-100.00, exclude 0
			// in case of int percentage (0-100), it will be rng.Next(1, 101)
			//	if -99, then percentage 99 will be 100%, so -100
			//	if 0-, then percentage 1 will be 2 parts for 0, 1, so 1-
			//	so 1-100, i.e. rng.Next(1-101)
			var randomValue = (double)rng.Next(1, 100_01) / 100;
			return randomValue;
		}
	}
}
