using SomnosSuite.Domain.SharedKernel;

namespace SomnosSuite.Domain.StunningCheckReports
{
    public class ReportPeriodErrors
    {
        public static readonly Error EndMustBeAfterStart = new(
            "ReportPeriod.EndMustBeAfterStart",
            "Report period end must be after start.");

        public static readonly Error StartRequired = new(
            "ReportPeriod.StartRequired",
            "Report period start is required.");

        public static readonly Error EndRequired = new(
            "ReportPeriod.EndRequired",
            "Report period end is required.");
    }
}