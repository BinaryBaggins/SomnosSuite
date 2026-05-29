using FluentAssertions;
using SomnosSuite.Domain.StunningCheckReports;
using Xunit;

namespace SomnosSuite.Domain.Tests.StunningCheckReports;

public sealed class ReportPeriodTests
{
    private static readonly DateTimeOffset Start = new(2026, 5, 1, 0, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset End = new(2026, 6, 1, 0, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Create_Should_Reject_Missing_Start()
    {
        ReportPeriod.Create(null, End).Error
            .Should().Be(ReportPeriodErrors.StartRequired);
    }

    [Fact]
    public void Create_Should_Reject_Missing_End()
    {
        ReportPeriod.Create(Start, null).Error
            .Should().Be(ReportPeriodErrors.EndRequired);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_Should_Reject_End_That_Is_Not_After_Start(int daysToAdd)
    {
        ReportPeriod.Create(Start, Start.AddDays(daysToAdd)).Error
            .Should().Be(ReportPeriodErrors.EndMustBeAfterStart);
    }

    [Fact]
    public void Create_Should_Accept_Valid_Period()
    {
        var result = ReportPeriod.Create(Start, End);

        result.IsSuccess.Should().BeTrue();
        result.Value.StartInclusive.Should().Be(Start);
        result.Value.EndExclusive.Should().Be(End);
    }
}
