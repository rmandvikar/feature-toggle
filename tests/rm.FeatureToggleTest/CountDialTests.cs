using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using rm.FeatureToggle;

namespace rm.FeatureToggleTest
{
	[TestFixture]
	public class CountDialTests
	{
		private const int iterations = 1_000_000;

		[TestFixture]
		public class ToDial
		{
			[Test]
			[TestCase(0)]
			[TestCase(-1)]
			public void Verify_ToDial_Edge_Case(long count)
			{
				var dial = new CountDial();
				Assert.IsFalse(dial.ToDial(count));
			}

			[Test]
			[TestCase(0)]
			[TestCase(10)]
			[TestCase(1_000)]
			[TestCase(iterations)]
			public void Verify_ToDial(long count)
			{
				Assume.That(count <= iterations);
				var dial = new CountDial();
				var locker = new object();
				int i = 0;
				Parallel.For(0, iterations, loop =>
				{
					if (dial.ToDial(count))
					{
						lock (locker)
						{
							i++;
						}
					}
				});
				Console.WriteLine($"count: {count}, i: {i}");
				Assert.AreEqual(count, i);
			}

			[Test]
			[TestCase(0)]
			[TestCase(10)]
			[TestCase(100)]
			[TestCase(1_000)]
			public void Verify_ToDial_False_After_Count(long count)
			{
				var dial = new CountDial();
				for (int i = 0; i < count; i++)
				{
					Assert.IsTrue(dial.ToDial(count));
				}
				Assert.IsFalse(dial.ToDial(count));
				Assert.IsFalse(dial.ToDial(count - 1));
			}

			[Test]
			[TestCase(0)]
			[TestCase(10)]
			[TestCase(1_000)]
			[TestCase(iterations)]
			public void Perf_ToDial(long count)
			{
				var dial = new CountDial();
				var stopwatch = Stopwatch.StartNew();
				Parallel.For(0, iterations, loop =>
				{
					dial.ToDial(count);
				});
				stopwatch.Stop();
				Console.WriteLine(stopwatch.ElapsedMilliseconds);
			}
		}
	}
}
