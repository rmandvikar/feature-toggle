using System;
using System.Text;

namespace rm.FeatureToggle
{
	/// <inheritdoc cref="IIdDial"/>
	public class IdDial : IIdDial
	{
		private readonly IPercentValueCalculator percentValueCalculator = new PercentValueCalculator();

		/// <inheritdoc/>
		public bool ToDial(string id, double percentage)
		{
			_ = id
				?? throw new ArgumentNullException(nameof(id));

			if (percentage <= 0)
			{
				return false;
			}
			if (percentage >= 100)
			{
				return true;
			}

			return GetPercentValue(id) <= percentage;
		}

		private double GetPercentValue(string id)
		{
			// 0.01-100.00
			var bytes = Encoding.UTF8.GetBytes(id);
			return percentValueCalculator.Calculate(bytes);
		}
	}
}
