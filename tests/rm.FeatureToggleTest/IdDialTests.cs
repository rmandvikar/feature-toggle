using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using rm.FeatureToggle;

namespace rm.FeatureToggleTest;

[TestFixture]
public class IdDialTests
{
	private static readonly Func<string> idFactory = () => Guid.NewGuid().ToString();
	private const int iterations = 1_000_000;

	[TestFixture]
	public class ToDial
	{
		[Test]
		[TestCase("ascii", 13.62d, true)]
		[TestCase("\0\0", 0.01d, true)]
		public void Verify_ToDial(string id, double percentage, bool toDial)
		{
			var dial = new IdDial();
			Assert.AreEqual(toDial, dial.ToDial(id, percentage));
		}

		[Test]
		[TestCase("ɹ")]
		public void Verify_ToDial_Throws_For_Invalid_Input(string id)
		{
			var dial = new IdDial();
			Assert.Throws<ArgumentException>(() =>
				dial.ToDial(id, 0d));
		}

		[Test]
		[TestCase(0d, false)]
		[TestCase(100d, true)]
		public void Verify_ToDial_For_Edge_Values(double percentage, bool toDial)
		{
			var dial = new IdDial();
			Assert.AreEqual(toDial, dial.ToDial(idFactory(), percentage));
		}

		[Test]
		public void Verify_ToDial_For_Edge_Input_Values_Null()
		{
			var dial = new IdDial();
			Assert.Throws<ArgumentNullException>(() =>
			{
				dial.ToDial(null, 50d);
			});
		}

		[Test]
		public void Verify_ToDial_For_Edge_Input_Values_WhiteSpace()
		{
			var dial = new IdDial();
			Assert.DoesNotThrow(() =>
			{
				dial.ToDial("", 50d);
				dial.ToDial(" ", 50d);
			});
		}

		[Test]
		[TestCase(0.001d, false)]
		[TestCase(-1d, false)]
		[TestCase(100.1d, true)]
		public void Verify_ToDial_For_Out_Of_Bound_Values(double percentage, bool toDial)
		{
			var dial = new IdDial();
			Assert.AreEqual(toDial, dial.ToDial(idFactory(), percentage));
		}

		[Test]
		[TestCase(0d)]
		[TestCase(1d)]
		[TestCase(25d)]
		[TestCase(40d)]
		[TestCase(50d)]
		[TestCase(75d)]
		[TestCase(60d)]
		[TestCase(99d)]
		[TestCase(100d)]
		public void Sample_ToDial(double percentage)
		{
			var dial = new IdDial();
			var count = 0;
			for (int i = 0; i < iterations; i++)
			{
				if (dial.ToDial(idFactory(), percentage))
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
			var dial = new IdDial();
			var guidString = idFactory();
			var stopwatch = Stopwatch.StartNew();
			Parallel.For(0, iterations, loop =>
			{
				dial.ToDial(guidString, percentage);
			});
			stopwatch.Stop();
			Console.WriteLine(stopwatch.ElapsedMilliseconds);
		}
	}
}
