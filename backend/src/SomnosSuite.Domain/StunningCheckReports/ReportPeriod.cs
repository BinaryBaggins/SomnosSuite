using SomnosSuite.Domain.SharedKernel;

namespace SomnosSuite.Domain.StunningCheckReports
{
    public sealed record ReportPeriod : IValueObject
    {
        public DateTimeOffset? StartInclusive { get; }
        public DateTimeOffset? EndExclusive { get; }

        private ReportPeriod(DateTimeOffset? startInclusive, DateTimeOffset? endExclusive)
        {
            StartInclusive = startInclusive;
            EndExclusive = endExclusive;
        }

        public static Result<ReportPeriod> Create(DateTimeOffset? startInclusive, DateTimeOffset? endExclusive)
        {
            if (startInclusive is null)
                return Result<ReportPeriod>.Failure(ReportPeriodErrors.StartRequired);

            if (endExclusive is null)
                return Result<ReportPeriod>.Failure(ReportPeriodErrors.EndRequired);

            if (startInclusive >= endExclusive)
                return Result<ReportPeriod>.Failure(ReportPeriodErrors.EndMustBeAfterStart);

            return Result<ReportPeriod>.Success(new ReportPeriod(startInclusive, endExclusive));
        }
    }
}