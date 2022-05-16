using System;
using NUnit.Framework;
using rm.FeatureToggle;

namespace rm.FeatureToggleTest
{
	[TestFixture]
	public class PercentValueCalculatorTests
	{
		[TestFixture]
		public class Calculate
		{
			[Test]
			[TestCase("ascii", 98.73d)]
			[TestCase("boom!", 46.88d)]
			[TestCase("bond. james bond.", 57.89d)]
			[TestCase("roll roll roll", 19.02d)]
			[TestCase("\0\0", 0.98d)]
			public void Verify_Calculate(string id, double percentage)
			{
				var percentValueCalculator = new PercentValueCalculator();
				Assert.AreEqual(percentage, percentValueCalculator.Calculate(id));
			}

			[Test]
			[TestCase((string)null)]
			public void Verify_Calculate_For_Edge_Values(string id)
			{
				var percentValueCalculator = new PercentValueCalculator();
				Assert.Throws<ArgumentNullException>(() =>
					percentValueCalculator.Calculate(id));
			}
		}
	}
}
