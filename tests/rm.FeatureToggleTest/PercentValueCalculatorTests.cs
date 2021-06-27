using System;
using NUnit.Framework;
using rm.FeatureToggle;
using rm.Random2;

namespace rm.FeatureToggleTest
{
	[TestFixture]
	public class PercentValueCalculatorTests
	{
		private static readonly Random rng = RandomFactory.GetThreadStaticRandom();
		private const int iterations = 5;

		[TestFixture]
		public class Calculate
		{
			[Test]
			[TestCase(0)]
			[TestCase(int.MaxValue)]
			[TestCase(int.MinValue)]
			public void Verify_Calculate_For_Determinism_Int(int intValue)
			{
				var percentValueCalculator = new PercentValueCalculator();
				// no need to worry about endianness
				var bytes = BitConverter.GetBytes(intValue);
				var expectedValue = percentValueCalculator.Calculate(bytes);

				for (int i = 0; i < iterations; i++)
				{
					var value = percentValueCalculator.Calculate(bytes);
					Assert.AreEqual(expectedValue, value);
				}
			}

			[Test]
			[TestCase(short.MaxValue)]
			[TestCase(short.MinValue)]
			public void Verify_Calculate_For_Determinism_Short(short shortValue)
			{
				var percentValueCalculator = new PercentValueCalculator();
				// no need to worry about endianness
				var bytes = BitConverter.GetBytes(shortValue);
				var expectedValue = percentValueCalculator.Calculate(bytes);

				for (int i = 0; i < iterations; i++)
				{
					var value = percentValueCalculator.Calculate(bytes);
					Assert.AreEqual(expectedValue, value);
				}
			}

			[Test]
			public void Verify_Calculate_For_Determinism_Random()
			{
				var percentValueCalculator = new PercentValueCalculator();
				var n = rng.Next();
				// no need to worry about endianness
				var bytes = BitConverter.GetBytes(n);
				var expectedValue = percentValueCalculator.Calculate(bytes);

				for (int i = 0; i < iterations; i++)
				{
					var value = percentValueCalculator.Calculate(bytes);
					Assert.AreEqual(expectedValue, value);
				}
			}

			[Test]
			[TestCase((byte[])null)]
			public void Verify_Calculate_For_Edge_Values(byte[] bytes)
			{
				var percentValueCalculator = new PercentValueCalculator();
				Assert.Throws<ArgumentNullException>(() =>
					percentValueCalculator.Calculate(bytes));
			}
		}
	}
}
