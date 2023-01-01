using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoFixture.AutoMoq;
using AutoFixture;
using NUnit.Framework;
using rm.FeatureToggle;
using rm.Clock;
using Moq;

namespace rm.FeatureToggleTest
{
	[TestFixture]
	public class DateRangeDialTests
	{
		private const int iterations = 1_000_000;

		[TestFixture]
		public class ToDial
		{
			[Test]
			[TestCase("2023-01-01T00:00:00-0800", "2023-01-01T04:00:00-0800")]
			[TestCase("2023-01-01T00:00:00-0800", null)]
			[TestCase(null, "2023-01-01T04:00:00-0800")]
			[TestCase(null, null)]
			[TestCase("2023-01-01T02:00:00-0800", "2023-01-01T02:00:00-0800")]
			public void Verify_ToDial_True(string startDateStr, string endDateStr)
			{
				var fixture = new Fixture().Customize(new AutoMoqCustomization());

				var clockMock = fixture.Freeze<Mock<ISystemClock>>();
				var utcNow = DateTimeOffset.Parse("2023-01-01T02:00:00-0800");
				clockMock.Setup(x => x.UtcNow).Returns(utcNow);
				var dial = new DateRangeDial(clockMock.Object);
				var startDate = startDateStr != null ? DateTimeOffset.Parse(startDateStr) : (DateTimeOffset?)null;
				var endDate = endDateStr != null ? DateTimeOffset.Parse(endDateStr) : (DateTimeOffset?)null;

				Assert.IsTrue(dial.ToDial(startDate, endDate));
			}

			[Test]
			[TestCase("2023-01-01T04:00:00-0800", "2023-01-01T00:00:00-0800")]
			[TestCase("2023-01-01T02:00:01-0800", null)]
			[TestCase(null, "2023-01-01T01:59:59-0800")]
			[TestCase("2023-01-01T00:00:00-0800", "2023-01-01T01:00:00-0800")]
			[TestCase("2023-01-01T03:00:00-0800", "2023-01-01T04:00:00-0800")]
			public void Verify_ToDial_False(string startDateStr, string endDateStr)
			{
				var fixture = new Fixture().Customize(new AutoMoqCustomization());

				var clockMock = fixture.Freeze<Mock<ISystemClock>>();
				var utcNow = DateTimeOffset.Parse("2023-01-01T02:00:00-0800");
				clockMock.Setup(x => x.UtcNow).Returns(utcNow);
				var dial = new DateRangeDial(clockMock.Object);
				var startDate = startDateStr != null ? DateTimeOffset.Parse(startDateStr) : (DateTimeOffset?)null;
				var endDate = endDateStr != null ? DateTimeOffset.Parse(endDateStr) : (DateTimeOffset?)null;

				Assert.IsFalse(dial.ToDial(startDate, endDate));
			}

			[Test]
			public void Perf_ToDial()
			{
				var fixture = new Fixture().Customize(new AutoMoqCustomization());

				fixture.Register<ISystemClock>(() => new SystemClock());
				var dial = fixture.Create<DateRangeDial>();
				var startDate = DateTimeOffset.Parse("2023-01-01T00:00:00-0800");
				var endDate = DateTimeOffset.Parse("2023-01-01T04:00:00-0800");
				var stopwatch = Stopwatch.StartNew();
				Parallel.For(0, iterations, loop =>
				{
					dial.ToDial(startDate, endDate);
				});
				stopwatch.Stop();
				Console.WriteLine(stopwatch.ElapsedMilliseconds);
			}
		}
	}
}
