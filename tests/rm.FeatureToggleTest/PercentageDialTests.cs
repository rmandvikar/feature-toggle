using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using rm.FeatureToggle;
using rm.Random2;

namespace rm.FeatureToggleTest;

[TestFixture]
public class PercentageDialTests
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
			var dial = new PercentageDial(rng);
			Assert.AreEqual(toDial, dial.ToDial(percentage));
		}

		[Test]
		[TestCase(0.001d, false)]
		[TestCase(-1d, false)]
		[TestCase(100.1d, true)]
		public void Verify_ToDial_For_Out_Of_Bound_Values(double percentage, bool toDial)
		{
			var dial = new PercentageDial(rng);
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
			var dial = new PercentageDial(rng);
			var count = 0;
			for (int i = 0; i < iterations; i++)
			{
				if (dial.ToDial(percentage))
				{
					count++;
				}
			}
			var dialedPercentage = (double)count / iterations * 100;
			Console.WriteLine($"percentage: {percentage}, dialedPercentage: {Math.Round(dialedPercentage, 4)}, " +
				$"count: {count}, iterations: {iterations}");
		}

		[Test]
		[TestCase(0d)]
		[TestCase(50d)]
		[TestCase(100d)]
		public void Perf_ToDial(double percentage)
		{
			var dial = new PercentageDial(rng);
			var stopwatch = Stopwatch.StartNew();
			Parallel.For(0, iterations, loop =>
			{
				dial.ToDial(percentage);
			});
			stopwatch.Stop();
			Console.WriteLine(stopwatch.ElapsedMilliseconds);
		}
	}
}
