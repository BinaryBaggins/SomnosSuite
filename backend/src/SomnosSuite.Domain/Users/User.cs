using System.Net.Mail;
using SomnosSuite.Domain.SharedKernel;

namespace SomnosSuite.Domain.Users
{
    public sealed class User : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public UserRole Role { get; private set; }
        public UserStatus Status { get; private set; }
        public Guid? ModifiedByUserId { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }

        private User() { }

        private User(
            string name,
            string email,
            string passwordHash,
            UserRole role,
            UserStatus status,
            DateTimeOffset createdAt)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            Status = status;
            CreatedAt = createdAt;
            IsDeleted = false;
        }

        private User(
            Guid id,
            string name,
            string email,
            string passwordHash,
            UserRole role,
            UserStatus status,
            Guid? modifiedByUserId,
            DateTimeOffset createdAt,
            DateTimeOffset? updatedAt,
            bool isDeleted)
        : base(id)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            Status = status;
            ModifiedByUserId = modifiedByUserId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsDeleted = isDeleted;
        }

        public static Result<User> Create(
            string? name,
            string? email,
            string? passwordHash,
            UserRole role,
            UserStatus status,
            DateTimeOffset createdAt)
        {
            var validation = Validate(
                name,
                email,
                passwordHash,
                role,
                status,
                out var trimmedName,
                out var trimmedEmail);

            if (validation.IsFailure)
                return Result<User>.Failure(validation.Error);

            return new User(trimmedName, trimmedEmail, passwordHash!, role, status, createdAt);
        }

        public static Result<User> Rehydrate(
            Guid id,
            string? name,
            string? email,
            string? passwordHash,
            UserRole role,
            UserStatus status,
            Guid? modifiedByUserId,
            DateTimeOffset createdAt,
            DateTimeOffset? updatedAt,
            bool isDeleted)
        {
            if (id == Guid.Empty)
                return Result<User>.Failure(UserErrors.InvalidIdError);

            var validation = Validate(
                name,
                email,
                passwordHash,
                role,
                status,
                out var trimmedName,
                out var trimmedEmail);

            if (validation.IsFailure)
                return Result<User>.Failure(validation.Error);

            var modifiedInfoValidation = ValidateModifiedInfo(modifiedByUserId, updatedAt, createdAt, isDeleted);
            if (modifiedInfoValidation.IsFailure)
                return Result<User>.Failure(modifiedInfoValidation.Error);

            return new User(
                id,
                trimmedName,
                trimmedEmail,
                passwordHash!,
                role,
                status,
                modifiedByUserId,
                createdAt,
                updatedAt,
                isDeleted);
        }

        public Result UpdateName(string? name, Guid modifiedByUserId, DateTimeOffset updatedAt)
        {
            if (IsDeleted)
                return Result.Failure(UserErrors.UserIsDeletedError);

            var trimmedName = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(trimmedName))
                return Result.Failure(UserErrors.NameIsRequiredError);

            var modifiedInfoResult = UpdateModifiedInfo(modifiedByUserId, updatedAt);
            if (modifiedInfoResult.IsFailure)
                return modifiedInfoResult;

            Name = trimmedName;
            return Result.Success();
        }

        public Result UpdateEmail(string? email, Guid modifiedByUserId, DateTimeOffset updatedAt)
        {
            if (IsDeleted)
                return Result.Failure(UserErrors.UserIsDeletedError);

            var trimmedEmail = email?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(trimmedEmail))
                return Result.Failure(UserErrors.EmailIsRequiredError);

            if (!IsValidEmail(trimmedEmail))
                return Result.Failure(UserErrors.EmailIsInvalidError);

            var modifiedInfoResult = UpdateModifiedInfo(modifiedByUserId, updatedAt);
            if (modifiedInfoResult.IsFailure)
                return modifiedInfoResult;

            Email = trimmedEmail;
            return Result.Success();
        }

        public Result UpdatePasswordHash(string? passwordHash, Guid modifiedByUserId, DateTimeOffset updatedAt)
        {
            if (IsDeleted)
                return Result.Failure(UserErrors.UserIsDeletedError);

            if (string.IsNullOrWhiteSpace(passwordHash))
                return Result.Failure(UserErrors.PasswordHashIsRequiredError);

            var modifiedInfoResult = UpdateModifiedInfo(modifiedByUserId, updatedAt);
            if (modifiedInfoResult.IsFailure)
                return modifiedInfoResult;

            PasswordHash = passwordHash;
            return Result.Success();
        }

        public Result UpdateRole(UserRole role, Guid modifiedByUserId, DateTimeOffset updatedAt)
        {
            if (IsDeleted)
                return Result.Failure(UserErrors.UserIsDeletedError);

            var roleValidation = ValidateRole(role);
            if (roleValidation.IsFailure)
                return roleValidation;

            var modifiedInfoResult = UpdateModifiedInfo(modifiedByUserId, updatedAt);
            if (modifiedInfoResult.IsFailure)
                return modifiedInfoResult;

            Role = role;
            return Result.Success();
        }

        public Result UpdateStatus(UserStatus status, Guid modifiedByUserId, DateTimeOffset updatedAt)
        {
            if (IsDeleted)
                return Result.Failure(UserErrors.UserIsDeletedError);

            var statusValidation = ValidateStatus(status);
            if (statusValidation.IsFailure)
                return statusValidation;

            if (!IsAllowedStatusTransition(Status, status))
                return Result.Failure(UserErrors.UserStatusTransitionIsInvalidError);

            var modifiedInfoResult = UpdateModifiedInfo(modifiedByUserId, updatedAt);
            if (modifiedInfoResult.IsFailure)
                return modifiedInfoResult;

            Status = status;
            return Result.Success();
        }

        public Result MarkAsDeleted(Guid modifiedByUserId, DateTimeOffset updatedAt)
        {
            if (IsDeleted)
                return Result.Success();

            var modifiedInfoResult = UpdateModifiedInfo(modifiedByUserId, updatedAt);
            if (modifiedInfoResult.IsFailure)
                return modifiedInfoResult;

            IsDeleted = true;
            return Result.Success();
        }

        private Result UpdateModifiedInfo(Guid modifiedByUserId, DateTimeOffset updatedAt)
        {
            if (modifiedByUserId == Guid.Empty)
                return Result.Failure(UserErrors.ModifiedByUserIdIsRequiredError);

            if (updatedAt < CreatedAt)
                return Result.Failure(UserErrors.UpdatedAtCannotBeBeforeCreatedAtError);

            ModifiedByUserId = modifiedByUserId;
            UpdatedAt = updatedAt;
            return Result.Success();
        }

        private static Result Validate(
            string? name,
            string? email,
            string? passwordHash,
            UserRole role,
            UserStatus status,
            out string trimmedName,
            out string trimmedEmail)
        {
            trimmedName = name?.Trim() ?? string.Empty;
            trimmedEmail = email?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(trimmedName))
                return Result.Failure(UserErrors.NameIsRequiredError);

            if (string.IsNullOrWhiteSpace(trimmedEmail))
                return Result.Failure(UserErrors.EmailIsRequiredError);

            if (!IsValidEmail(trimmedEmail))
                return Result.Failure(UserErrors.EmailIsInvalidError);

            if (string.IsNullOrWhiteSpace(passwordHash))
                return Result.Failure(UserErrors.PasswordHashIsRequiredError);

            var roleValidation = ValidateRole(role);
            if (roleValidation.IsFailure)
                return roleValidation;

            return ValidateStatus(status);
        }

        private static Result ValidateRole(UserRole role)
        {
            if (!Enum.IsDefined(role))
                return Result.Failure(UserErrors.UserRoleIsInvalidError);

            return Result.Success();
        }

        private static Result ValidateStatus(UserStatus status)
        {
            if (!Enum.IsDefined(status))
                return Result.Failure(UserErrors.UserStatusIsInvalidError);

            return Result.Success();
        }

        private static Result ValidateModifiedInfo(
            Guid? modifiedByUserId,
            DateTimeOffset? updatedAt,
            DateTimeOffset createdAt,
            bool isDeleted)
        {
            if (modifiedByUserId.HasValue != updatedAt.HasValue)
                return Result.Failure(UserErrors.ModifiedInfoIsRequiredForDeletedUserError);

            if (modifiedByUserId == Guid.Empty)
                return Result.Failure(UserErrors.ModifiedByUserIdIsRequiredError);

            if (updatedAt < createdAt)
                return Result.Failure(UserErrors.UpdatedAtCannotBeBeforeCreatedAtError);

            if (isDeleted && (!modifiedByUserId.HasValue || !updatedAt.HasValue))
                return Result.Failure(UserErrors.ModifiedInfoIsRequiredForDeletedUserError);

            return Result.Success();
        }

        private static bool IsAllowedStatusTransition(UserStatus currentStatus, UserStatus newStatus)
        {
            return currentStatus switch
            {
                UserStatus.Invited => newStatus is UserStatus.Active or UserStatus.Deactivated,
                UserStatus.Active => newStatus == UserStatus.Deactivated,
                UserStatus.Deactivated => newStatus == UserStatus.Active,
                _ => false
            };
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var emailAddress = new MailAddress(email);
                return emailAddress.Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
