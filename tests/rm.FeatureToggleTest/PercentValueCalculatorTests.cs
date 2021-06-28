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
			[TestCase(0, 0.01d)]
			[TestCase(int.MaxValue, 36.48d)]
			[TestCase(int.MinValue, 36.49d)]
			public void Verify_Calculate_For_Determinism_Int_LittleEndian(int value, double expectedValue)
			{
				var percentValueCalculator = new PercentValueCalculator();
				// no need to worry about endianness
				var bytes = BitConverter.GetBytes(value);
				if (BitConverter.IsLittleEndian)
				{
					// noop
				}
				else
				{
					Array.Reverse(bytes);
				}

				Assert.AreEqual(expectedValue, percentValueCalculator.Calculate(bytes));
			}

			[Test]
			[TestCase(0, 0.01d)]
			[TestCase(int.MaxValue, 1.30d)]
			[TestCase(int.MinValue, 1.29d)]
			public void Verify_Calculate_For_Determinism_Int_BigEndian(int value, double expectedValue)
			{
				var percentValueCalculator = new PercentValueCalculator();
				// no need to worry about endianness
				var bytes = BitConverter.GetBytes(value);
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(bytes);
				}
				else
				{
					// noop
				}

				Assert.AreEqual(expectedValue, percentValueCalculator.Calculate(bytes));
			}

			[Test]
			[TestCase(0, 0.01d)]
			[TestCase(short.MaxValue, 27.68d)]
			[TestCase(short.MinValue, 27.69d)]
			public void Verify_Calculate_For_Determinism_Short_LittleEndian(short value, double expectedValue)
			{
				var percentValueCalculator = new PercentValueCalculator();
				// no need to worry about endianness
				var bytes = BitConverter.GetBytes(value);
				if (BitConverter.IsLittleEndian)
				{
					// noop
				}
				else
				{
					Array.Reverse(bytes);
				}

				Assert.AreEqual(expectedValue, percentValueCalculator.Calculate(bytes));
			}

			[Test]
			[TestCase(0, 0.01d)]
			[TestCase(short.MaxValue, 1.30d)]
			[TestCase(short.MinValue, 1.29d)]
			public void Verify_Calculate_For_Determinism_Short_BigEndian(short value, double expectedValue)
			{
				var percentValueCalculator = new PercentValueCalculator();
				// no need to worry about endianness
				var bytes = BitConverter.GetBytes(value);
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(bytes);
				}
				else
				{
					// noop
				}

				Assert.AreEqual(expectedValue, percentValueCalculator.Calculate(bytes));
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
