using SomnosSuite.Domain.SharedKernel;

namespace SomnosSuite.Domain.StunningCheckReports
{
    public class StunningCheckReportErrors
    {
        public static readonly Error InvalidIdError = new(
            "StunningCheckReport.InvalidId",
            "Id must be a non-empty GUID.");

        public static readonly Error CreatedByUserIdRequiredError = new(
            "StunningCheckReport.CreatedByUserIdRequired",
            "Created by user's id is required.");

        public static readonly Error ModifiedByUserIdRequiredError = new(
            "StunningCheckReport.ModifiedByUserIdRequired",
            "Modified by user's id is required.");

        public static readonly Error ModifiedInfoIsIncompleteError = new(
            "StunningCheckReport.ModifiedInfoIsIncomplete",
            "Modified by user id and modified at must both be provided or both be omitted.");

        public static readonly Error ModifiedInfoIsRequiredError = new(
            "StunningCheckReport.ModifiedInfoIsRequired",
            "Modification audit information is required.");

        public static readonly Error ModifiedAtCannotBeBeforeCreatedAtError = new(
            "StunningCheckReport.ModifiedAtCannotBeBeforeCreatedAt",
            "Modified at can not be before created at.");

        public static readonly Error StunningCheckIdRequiredError = new(
            "StunningCheckReport.StunningCheckIdRequired",
            "Stunning check id is required.");

        public static readonly Error DuplicateStunningCheckIdError = new(
            "StunningCheckReport.DuplicateStunningCheckId",
            "Rehydrated stunning check ids must not contain duplicates.");

        public static readonly Error FindAndRemoveStunningCheckIdError = new(
            "StunningCheckReport.RemoveStunningCheckId",
            "Could not remove or find stunning check id.");

        public static readonly Error StunningCheckReportStatusIsInvalidError = new(
            "StunningCheckReport.StunningCheckReportStatusIsInvalid",
            "Stunning check report status must be a defined value.");

        public static readonly Error StunningCheckReportIsDeletedError = new(
            "StunningCheckReport.StunningCheckReportIsDeleted",
            "Deleted stunning check reports can not be changed.");

        public static readonly Error StunningCheckReportIsFinalizedError = new(
            "StunningCheckReport.StunningCheckReportIsFinalized",
            "Finalized stunning check reports can not be changed.");

        public static readonly Error StunningCheckReportIsAlreadyFinalizedError = new(
            "StunningCheckReport.StunningCheckReportIsAlreadyFinalized",
            "Stunning check report is already finalized.");

        public static readonly Error AnalysisIsRequiredForFinalizationError = new(
            "StunningCheckReport.AnalysisIsRequiredForFinalization",
            "Analysis is required before finalizing a stunning check report.");

        public static readonly Error StunningCheckIdsAreRequiredForFinalizationError = new(
            "StunningCheckReport.StunningCheckIdsAreRequiredForFinalization",
            "At least one stunning check id is required before finalizing a stunning check report.");
    }
}
