using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using rm.FeatureToggle;

namespace rm.FeatureToggleTest
{
	[TestFixture]
	public class GuidDialTests
	{
		private const int iterations = 1_000_000;

		[TestFixture]
		public class ToDial
		{
			[Test]
			[TestCase(0d, false)]
			[TestCase(100d, true)]
			public void Verify_ToDial_For_Edge_Values(double percentage, bool toDial)
			{
				var dial = new GuidDial();
				Assert.AreEqual(toDial, dial.ToDial(Guid.NewGuid(), percentage));
			}

			[Test]
			[TestCase(0.001d, false)]
			[TestCase(-1d, false)]
			[TestCase(100.1d, true)]
			public void Verify_ToDial_For_Out_Of_Bound_Values(double percentage, bool toDial)
			{
				var dial = new GuidDial();
				Assert.AreEqual(toDial, dial.ToDial(Guid.NewGuid(), percentage));
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
				var dial = new GuidDial();
				var count = 0;
				for (int i = 0; i < iterations; i++)
				{
					if (dial.ToDial(Guid.NewGuid(), percentage))
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
				var dial = new GuidDial();
				var guid = Guid.NewGuid();
				var stopwatch = Stopwatch.StartNew();
				Parallel.For(0, iterations, loop =>
				{
					dial.ToDial(guid, percentage);
				});
				stopwatch.Stop();
				Console.WriteLine(stopwatch.ElapsedMilliseconds);
			}
		}

		[TestFixture]
		public class ToByteArrayMatchingStringRepresentation
		{
			[Test]
			public void Verify_ToByteArrayMatchingStringRepresentation()
			{
				Console.WriteLine($"IsLittleEndian: {BitConverter.IsLittleEndian}");

				var guid = Guid.NewGuid();
				var guidN = guid.ToString("N");
				Console.WriteLine($"                                          guid: {guidN}");

				var bytes = guid.ToByteArray();
				var hex = BitConverter.ToString(bytes).ToLower().Replace("-", "");
				Console.WriteLine($"                            guid.ToByteArray(): {hex}");
				if (BitConverter.IsLittleEndian)
				{
					Assert.IsFalse(hex == guidN);
				}
				else
				{
					Assert.IsTrue(hex == guidN);
				}

				var bytesStringRepresentation = guid.ToByteArrayMatchingStringRepresentation();
				var hexStringRepresentation = BitConverter.ToString(bytesStringRepresentation).ToLower().Replace("-", "");
				Console.WriteLine($"guid.ToByteArrayMatchingStringRepresentation(): {hexStringRepresentation}");
				Assert.IsTrue(hexStringRepresentation == guidN);
			}
		}
	}
}
