using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using rm.FeatureToggle;
using rm.Random2;

namespace rm.FeatureToggleTest;

[TestFixture]
public class CountThenPercentageDialTests
{
	private static readonly Random rng = RandomFactory.GetThreadStaticRandom();
	private const int iterations = 1_000_000;

	[TestFixture]
	public class ToDial
	{
		[Test]
		[TestCase(0, 0d, false)]
		[TestCase(0, 100d, true)]
		public void Verify_ToDial_For_Edge_Values(long count, double percentage, bool toDial)
		{
			var dial = new CountThenPercentageDial(rng);
			Assert.AreEqual(toDial, dial.ToDial(count, percentage));
		}

		[Test]
		[TestCase(1, 0d)]
		public void Verify_ToDial(long count, double percentage)
		{
			var dial = new CountThenPercentageDial(rng);
			Assert.IsTrue(dial.ToDial(count, percentage));
			Assert.IsFalse(dial.ToDial(count, percentage));
		}

		[Test]
		[TestCase(iterations / 2, 80d)] // 90%
		[TestCase(iterations / 4, 20d)] // 40%
		public void Sample_ToDial(long count, double percentage)
		{
			var dial = new CountThenPercentageDial(rng);
			var locker = new object();
			int i = 0;
			Parallel.For(0, iterations, loop =>
			{
				if (dial.ToDial(count, percentage))
				{
					lock (locker)
					{
						i++;
					}
				}
			});
			var dialedPercentage = (double)i / iterations * 100;
			Console.WriteLine($"count: {count}, percentage: {percentage}, " +
				$"i: {i}, dialedPercentage: {Math.Round(dialedPercentage, 4)}, iterations: {iterations}");
		}

		[Test]
		[TestCase(0, 90d)]
		[TestCase(10, 90d)]
		[TestCase(1_000, 90d)]
		[TestCase(iterations, 90d)]
		public void Perf_ToDial(long count, double percentage)
		{
			var dial = new CountThenPercentageDial(rng);
			var stopwatch = Stopwatch.StartNew();
			Parallel.For(0, iterations, loop =>
			{
				dial.ToDial(count, percentage);
			});
			stopwatch.Stop();
			Console.WriteLine(stopwatch.ElapsedMilliseconds);
		}
	}
}
