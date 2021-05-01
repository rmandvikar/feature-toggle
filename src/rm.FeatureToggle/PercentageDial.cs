﻿using System;

namespace rm.FeatureToggle
{
	/// <inheritdoc cref="IPercentageDial"/>
	public class PercentageDial : IPercentageDial
	{
		private readonly Random rng;

		/// <param name="rng">
		/// rng.
		/// <para>Note: <paramref name="rng"/> needs to be thread-safe.</para>
		/// </param>
		public PercentageDial(Random rng)
		{
			this.rng = rng
				?? throw new ArgumentNullException(nameof(rng));
		}

		/// <inheritdoc/>
		public bool ToDial(double percentage)
		{
			if (percentage <= 0)
			{
				return false;
			}
			if (percentage >= 100)
			{
				return true;
			}

			// .01-100.00, exclude 0
			// in case of int percentage (0-100), it will be rng.Next(1, 101)
			//	if -99, then percentage 99 will be 100%, so -100
			//	if 0-, then percentage 1 will be 2 parts for 0, 1, so 1-
			//	so 1-100, i.e. rng.Next(1-101)
			var randomDialValue = (double)rng.Next(1, 100_01) / 100;
			return randomDialValue <= percentage;
		}
	}
}
