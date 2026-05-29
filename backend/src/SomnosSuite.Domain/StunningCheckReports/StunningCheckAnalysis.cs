using SomnosSuite.Domain.SharedKernel;

namespace SomnosSuite.Domain.StunningCheckReports
{
    public sealed record StunningCheckAnalysis : IValueObject
    {
        public int TotalChecks { get; }
        public int StunningSuccessfulChecks { get; }
        public int StunningFailedChecks { get; }
        public int StunningFailedDueToReflex { get; }
        public int StunningFailedDueToVocalization { get; }
        public int StunningFailedDueToGasping { get; }
        public int RestunningCount { get; }
        public decimal FailureRate { get; }
    }
}