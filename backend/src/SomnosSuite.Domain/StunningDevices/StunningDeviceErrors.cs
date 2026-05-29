using SomnosSuite.Domain.SharedKernel;

namespace SomnosSuite.Domain.StunningDevices
{
    public class StunningDeviceErrors
    {
        public static readonly Error InvalidIdError = new(
            "StunningDevice.InvalidId",
            "Id must be a non-empty GUID.");

        public static readonly Error ManufacturerIsRequiredError = new(
            "StunningDevice.ManufacturerIsRequired",
            "Manufacturer is required.");

        public static readonly Error SerialNumberIsRequiredError = new(
            "StunningDevice.SerialNumberIsRequired",
            "Serial number is required.");

        public static readonly Error ModelIsRequiredError = new(
            "StunningDevice.ModelIsRequired",
            "Model is required.");

        public static readonly Error AnimalCategoryIsInvalidError = new(
            "StunningDevice.AnimalCategoryIsInvalid",
            "Animal category must be a defined value.");

        public static readonly Error StunningDeviceTypeIsInvalidError = new(
            "StunningDevice.StunningDeviceTypeIsInvalid",
            "Animal category must be a defined value.");

        public static readonly Error LastInspectionDateCannotBeInFutureError = new(
            "StunningDevice.LastInspectionDateCannotBeInFuture",
            "Last inspection date can not be in the future."
        );

        public static readonly Error NewLastInspectionDateCannotBeOlderThanCurrentLastInspectionDateError = new(
            "StunningDevice.NewLastInspectionDateCannotBeOlderThanCurrentLastInspectionDate",
            "New last inspection date can not be older than the current last inspection date."
        );

        public static readonly Error StunningDeviceIsDeletedError = new(
            "StunningDevice.StunningDeviceIsDeleted",
            "Deleted stunning devices can not be changed.");

        public static readonly Error ModifiedByUserIdIsRequiredError = new(
            "StunningDevice.ModifiedByUserIdIsRequired",
            "Modified by user id is required.");

        public static readonly Error ModifiedInfoIsIncompleteError = new(
            "StunningDevice.ModifiedInfoIsIncomplete",
            "Modified by user id and modified at must both be provided or both be omitted.");

        public static readonly Error ModifiedInfoIsRequiredForDeletedDeviceError = new(
            "StunningDevice.ModifiedInfoIsRequiredForDeletedDevice",
            "Deleted stunning devices must have modification audit information.");
    }
}
