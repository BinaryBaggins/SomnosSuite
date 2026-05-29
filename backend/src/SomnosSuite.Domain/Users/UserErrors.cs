using SomnosSuite.Domain.SharedKernel;

namespace SomnosSuite.Domain.Users
{
    public class UserErrors
    {
        public static readonly Error InvalidIdError = new(
            "User.InvalidId",
            "Id must be a non-empty GUID.");

        public static readonly Error NameIsRequiredError = new(
            "User.NameIsRequired",
            "Name is required.");

        public static readonly Error EmailIsRequiredError = new(
            "User.EmailIsRequired",
            "Email is required.");

        public static readonly Error EmailIsInvalidError = new(
            "User.EmailIsInvalid",
            "Email must be a valid email address.");

        public static readonly Error PasswordHashIsRequiredError = new(
            "User.PasswordHashIsRequired",
            "Password hash is required.");

        public static readonly Error ModifiedByUserIdIsRequiredError = new(
            "User.ModifiedByUserIdIsRequired",
            "Modified by user id is required.");

        public static readonly Error ModifiedInfoIsRequiredForDeletedUserError = new(
            "User.ModifiedInfoIsRequiredForDeletedUser",
            "Deleted users must have modification audit information.");

        public static readonly Error UpdatedAtCannotBeBeforeCreatedAtError = new(
            "User.UpdatedAtCannotBeBeforeCreatedAt",
            "Updated at can not be before created at.");

        public static readonly Error UserIsDeletedError = new(
            "User.UserIsDeleted",
            "Deleted users can not be changed.");

        public static readonly Error UserRoleIsInvalidError = new(
            "User.UserRoleIsInvalid",
            "User role must be a defined value.");

        public static readonly Error UserRoleIsRequiredError = new(
            "User.UserRoleIsRequired",
            "User role is required.");

        public static readonly Error UserStatusIsInvalidError = new(
            "User.UserStatusIsInvalid",
            "User status must be a defined value.");

        public static readonly Error UserStatusTransitionIsInvalidError = new(
            "User.UserStatusTransitionIsInvalid",
            "User status transition is invalid.");
    }
}
