using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using rm.FeatureToggle;
using rm.Random2;

namespace rm.FeatureToggleTest;

[TestFixture]
public class ProbabilityTests
{
	private static readonly Random rng = RandomFactory.GetThreadStaticRandom();
	private const int iterations = 1_000_000;

	[TestFixture]
	public class IsTrue
	{
		[Test]
		[TestCase(0d, false)]
		[TestCase(100d, true)]
		public void Verify_IsTrue_For_Edge_Values(double percentage, bool toDial)
		{
			var probability = new Probability(rng);
			Assert.AreEqual(toDial, probability.IsTrue(percentage));
		}

		[Test]
		[TestCase(0.001d, false)]
		[TestCase(-1d, false)]
		[TestCase(100.1d, true)]
		public void Verify_IsTrue_For_Out_Of_Bound_Values(double percentage, bool toDial)
		{
			var probability = new Probability(rng);
			Assert.AreEqual(toDial, probability.IsTrue(percentage));
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
		public void Sample_IsTrue(double percentage)
		{
			var probability = new Probability(rng);
			var count = 0;
			for (int i = 0; i < iterations; i++)
			{
				if (probability.IsTrue(percentage))
				{
					count++;
				}
			}
			var actualPercentage = (double)count / iterations * 100;
			Console.WriteLine($"percentage: {percentage}, actualPercentage: {Math.Round(actualPercentage, 4)}, " +
				$"count: {count}, iterations: {iterations}");
		}

		[Test]
		[TestCase(0d)]
		[TestCase(50d)]
		[TestCase(100d)]
		public void Perf_IsTrue(double percentage)
		{
			var probability = new Probability(rng);
			var stopwatch = Stopwatch.StartNew();
			Parallel.For(0, iterations, loop =>
			{
				probability.IsTrue(percentage);
			});
			stopwatch.Stop();
			Console.WriteLine(stopwatch.ElapsedMilliseconds);
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
		public void Sample_Parallel_IsTrue(double percentage)
		{
			var probability = new Probability(rng);
			var stopwatch = Stopwatch.StartNew();
			var count = 0;
			Parallel.For(0, iterations, loop =>
			{
				if (probability.IsTrue(percentage))
				{
					Interlocked.Increment(ref count);
				}
			});
			stopwatch.Stop();
			Console.WriteLine(stopwatch.ElapsedMilliseconds);

			var actualPercentage = (double)count / iterations * 100;
			Console.WriteLine($"percentage: {percentage}, actualPercentage: {Math.Round(actualPercentage, 4)}, " +
				$"count: {count}, iterations: {iterations}");
		}
	}
}
