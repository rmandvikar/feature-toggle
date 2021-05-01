using System;
using NUnit.Framework;
using rm.FeatureToggle;
using rm.Random2;

namespace rm.FeatureToggleTest.csproj
{
	[TestFixture]
	public class DialTests
	{
		private static readonly Random rng = RandomFactory.GetThreadStaticRandom();
		private const int iterations = 1_000_000;

		[TestFixture]
		public class ToDial
		{
			[Test]
			[TestCase(0d, false)]
			[TestCase(100d, true)]
			public void Verify_ToDial_For_Edge_Values(double percentage, bool toDial)
			{
				var dial = new Dial(rng);
				Assert.AreEqual(toDial, dial.ToDial(percentage));
			}

			[Test]
			[TestCase(0.001d, false)]
			[TestCase(-1d, false)]
			[TestCase(100.1d, true)]
			public void Verify_ToDial_For_Out_Of_Bound_Values(double percentage, bool toDial)
			{
				var dial = new Dial(rng);
				Assert.AreEqual(toDial, dial.ToDial(percentage));
			}

			[Test]
			[TestCase(0.001d)]
			[TestCase(0.01d)]
			[TestCase(0.10d)]
			[TestCase(1.10d)]
			[TestCase(73.10d)]
			[TestCase(60.10d)]
			[TestCase(99.10d)]
			[TestCase(100.00d)]
			public void Sample_ToDial(double percentage)
			{
				var dial = new Dial(rng);
				var count = 0;
				for (int i = 0; i < iterations; i++)
				{
					if (dial.ToDial(percentage))
					{
						count++;
					}
				}
				var dialedPercentage = (double)count / iterations * 100;
				Console.WriteLine($"percentage: {percentage}, dialedPercentage: {Math.Round(dialedPercentage, 4)}");
			}
		}
	}
}
