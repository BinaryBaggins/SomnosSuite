using FluentAssertions;
using SomnosSuite.Domain.Users;
using Xunit;

namespace SomnosSuite.Domain.Tests.Users;

public sealed class UserTests
{
    private static readonly Guid UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    private static readonly Guid ModifierId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
    private static readonly DateTimeOffset CreatedAt = new(2026, 5, 1, 8, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset UpdatedAt = new(2026, 5, 2, 8, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Create_Should_Trim_Name_And_Email_But_Not_PasswordHash()
    {
        const string passwordHash = "  hash-with-padding  ";

        var result = User.Create(
            "  Max Mustermann  ",
            "  user@example.com  ",
            passwordHash,
            UserRole.Admin,
            UserStatus.Invited,
            CreatedAt);

        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be("Max Mustermann");
        result.Value.Email.Should().Be("user@example.com");
        result.Value.PasswordHash.Should().Be(passwordHash);
        result.Value.Role.Should().Be(UserRole.Admin);
        result.Value.Status.Should().Be(UserStatus.Invited);
    }

    [Fact]
    public void Create_Should_Reject_Invalid_Email()
    {
        User.Create("Max Mustermann", "not-an-email", "hash", UserRole.Admin, UserStatus.Active, CreatedAt).Error
            .Should().Be(UserErrors.EmailIsInvalidError);
    }

    [Fact]
    public void Create_Should_Reject_Invalid_Role_And_Status()
    {
        User.Create("Max Mustermann", "user@example.com", "hash", (UserRole)999, UserStatus.Active, CreatedAt).Error
            .Should().Be(UserErrors.UserRoleIsInvalidError);

        User.Create("Max Mustermann", "user@example.com", "hash", UserRole.Admin, (UserStatus)999, CreatedAt).Error
            .Should().Be(UserErrors.UserStatusIsInvalidError);
    }

    [Fact]
    public void Rehydrate_Should_Reject_Invalid_Id_Deleted_User_Without_Audit_And_Invalid_Chronology()
    {
        Rehydrate(id: Guid.Empty).Error.Should().Be(UserErrors.InvalidIdError);

        Rehydrate(isDeleted: true, modifiedByUserId: null, updatedAt: null).Error
            .Should().Be(UserErrors.ModifiedInfoIsRequiredForDeletedUserError);

        Rehydrate(updatedAt: CreatedAt.AddTicks(-1), modifiedByUserId: ModifierId).Error
            .Should().Be(UserErrors.UpdatedAtCannotBeBeforeCreatedAtError);
    }

    [Fact]
    public void Update_Methods_Should_Require_Modifier_Audit_And_Reject_Deleted_Users()
    {
        var user = ValidUser();

        user.UpdateName("New Name", Guid.Empty, UpdatedAt).Error
            .Should().Be(UserErrors.ModifiedByUserIdIsRequiredError);

        user.MarkAsDeleted(ModifierId, UpdatedAt).IsSuccess.Should().BeTrue();

        user.UpdateEmail("new@example.com", ModifierId, UpdatedAt.AddDays(1)).Error
            .Should().Be(UserErrors.UserIsDeletedError);
    }

    [Fact]
    public void UpdateStatus_Should_Allow_Only_Agreed_Transitions()
    {
        var invited = ValidUser(status: UserStatus.Invited);
        invited.UpdateStatus(UserStatus.Active, ModifierId, UpdatedAt).IsSuccess.Should().BeTrue();

        var active = ValidUser(status: UserStatus.Active);
        active.UpdateStatus(UserStatus.Invited, ModifierId, UpdatedAt).Error
            .Should().Be(UserErrors.UserStatusTransitionIsInvalidError);

        var deactivated = ValidUser(status: UserStatus.Deactivated);
        deactivated.UpdateStatus(UserStatus.Active, ModifierId, UpdatedAt).IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void MarkAsDeleted_Should_Be_Idempotent()
    {
        var user = ValidUser();

        user.MarkAsDeleted(ModifierId, UpdatedAt).IsSuccess.Should().BeTrue();
        user.MarkAsDeleted(Guid.Empty, UpdatedAt.AddDays(1)).IsSuccess.Should().BeTrue();

        user.IsDeleted.Should().BeTrue();
        user.ModifiedByUserId.Should().Be(ModifierId);
        user.UpdatedAt.Should().Be(UpdatedAt);
    }

    private static User ValidUser(UserStatus status = UserStatus.Active)
    {
        return Rehydrate(status: status, modifiedByUserId: null, updatedAt: null).Value;
    }

    private static SomnosSuite.Domain.SharedKernel.Result<User> Rehydrate(
        Guid? id = null,
        UserStatus status = UserStatus.Active,
        Guid? modifiedByUserId = null,
        DateTimeOffset? updatedAt = null,
        bool isDeleted = false)
    {
        return User.Rehydrate(
            id ?? UserId,
            "Max Mustermann",
            "user@example.com",
            "hash",
            UserRole.Admin,
            status,
            modifiedByUserId,
            CreatedAt,
            updatedAt,
            isDeleted);
    }
}
