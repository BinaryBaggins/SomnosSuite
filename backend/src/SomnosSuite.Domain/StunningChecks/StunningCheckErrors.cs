using SomnosSuite.Domain.SharedKernel;

namespace SomnosSuite.Domain.StunningChecks
{
    public class StunningCheckErrors
    {
        public static readonly Error InvalidIdError = new(
            "StunningCheck.InvalidId",
            "Id must be a non-empty GUID.");

        public static readonly Error InitialStunningDeviceIdIsRequiredError = new(
            "StunningCheck.InitialStunningDeviceIdIsRequired",
            "Initial stunning device id is required.");

        public static readonly Error RecordedChecksCannotBeRecordedAgainError = new(
            "StunningCheck.RecordedChecksCannotBeRecordedAgain",
            "Outcome has already been recorded for this stunning check.");

        public static readonly Error ConfirmedCheckIsRequiredForCorrectionError = new(
            "StunningCheck.ConfirmedCheckIsRequiredForCorrection",
            "Only confirmed stunning checks can be corrected.");

        public static readonly Error StunningCheckIsDeletedError = new(
            "StunningCheck.StunningCheckIsDeleted",
            "Deleted stunning checks can not be changed.");

        public static readonly Error StunningOutcomeIsInvalidError = new(
            "StunningCheck.StunningOutcomeIsInvalid",
            "Stunning outcome is invalid.");

        public static readonly Error FailureIndicatorIsNotAllowedError = new(
            "StunningCheck.FailureIndicatorIsNotAllowed",
            "Failure indicator is not allowed for a successful stunning outcome.");

        public static readonly Error RestunningTimingIsNotAllowedError = new(
            "StunningCheck.RestunningTimingIsNotAllowed",
            "Restunning timing is not allowed for a successful stunning outcome.");

        public static readonly Error RestunningDeviceIdIsNotAllowedError = new(
            "StunningCheck.RestunningDeviceIdIsNotAllowed",
            "Restunning device id is not allowed for a successful stunning outcome.");

        public static readonly Error FailureIndicatorIsRequiredError = new(
            "StunningCheck.FailureIndicatorIsRequired",
            "Failure indicator is required for a failed stunning outcome.");

        public static readonly Error RestunningTimingIsRequiredError = new(
            "StunningCheck.RestunningTimingIsRequired",
            "Restunning timing is required for a failed stunning outcome.");

        public static readonly Error RestunningDeviceIdIsRequiredError = new(
            "StunningCheck.RestunningDeviceIdIsRequired",
            "Restunning device id is required for a failed stunning outcome.");

        public static readonly Error RecordedByUserIdIsRequiredError = new(
            "StunningCheck.RecordedByUserIdIsRequired",
            "Recorded by user id is required.");

        public static readonly Error StunningCheckStatusIsInvalidError = new(
            "StunningCheck.StunningCheckStatusIsInvalid",
            "Stunning check status is invalid.");

        public static readonly Error OutcomeIsNotAllowedError = new(
            "StunningCheck.OutcomeIsNotAllowed",
            "Outcome is not allowed for the current stunning check status.");

        public static readonly Error RecordedByUserIdIsNotAllowedError = new(
            "StunningCheck.RecordedByUserIdIsNotAllowed",
            "Recorded by user id is not allowed for the current stunning check status.");

        public static readonly Error RecordedAtIsNotAllowedError = new(
            "StunningCheck.RecordedAtIsNotAllowed",
            "Recorded at is not allowed for the current stunning check status.");

        public static readonly Error StunningOutcomeIsRequiredError = new(
            "StunningCheck.StunningOutcomeIsRequired",
            "Stunning outcome is required for the current stunning check status.");

        public static readonly Error RecordedAtIsRequiredError = new(
            "StunningCheck.RecordedAtIsRequired",
            "Recorded at is required for the current stunning check status.");

        public static readonly Error ModifiedByUserIdIsRequiredError = new(
            "StunningCheck.ModifiedByUserIdIsRequired",
            "Modified by user id is required.");

        public static readonly Error ModifiedInfoIsIncompleteError = new(
            "StunningCheck.ModifiedInfoIsIncomplete",
            "Modified by user id and modified at must both be provided or both be omitted.");

        public static readonly Error ModifiedInfoIsRequiredForDeletedCheckError = new(
            "StunningCheck.ModifiedInfoIsRequiredForDeletedCheck",
            "Deleted stunning checks must have modification audit information.");

        public static readonly Error RecordedAtCannotBeBeforeCreatedAtError = new(
            "StunningCheck.RecordedAtCannotBeBeforeCreatedAt",
            "Recorded at can not be before created at.");

        public static readonly Error ModifiedAtCannotBeBeforeCreatedAtError = new(
            "StunningCheck.ModifiedAtCannotBeBeforeCreatedAt",
            "Modified at can not be before created at.");
    }
}
