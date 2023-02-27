using System;
using NUnit.Framework;
using rm.FeatureToggle;

namespace rm.FeatureToggleTest;

[TestFixture]
public class PercentValueCalculatorTests
{
	[TestFixture]
	public class Calculate
	{
		[Test]
		[TestCase("ascii", 13.62d)]
		[TestCase("boom!", 3.23d)]
		[TestCase("bond. james bond.", 21.63d)]
		[TestCase("roll roll roll", 56.83d)]
		[TestCase("\0\0", 0.01d)]
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
