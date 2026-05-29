using FluentAssertions;
using SomnosSuite.Domain.StunningCheckReports;
using Xunit;

namespace SomnosSuite.Domain.Tests.StunningCheckReports;

public sealed class StunningCheckReportTests
{
    private static readonly Guid ReportId = Guid.Parse("77777777-7777-7777-7777-777777777777");
    private static readonly Guid CreatedByUserId = Guid.Parse("88888888-8888-8888-8888-888888888888");
    private static readonly Guid ModifiedByUserId = Guid.Parse("99999999-9999-9999-9999-999999999999");
    private static readonly Guid CheckId = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");
    private static readonly Guid OtherCheckId = Guid.Parse("ffffffff-eeee-dddd-cccc-bbbbbbbbbbbb");
    private static readonly DateTimeOffset CreatedAt = new(2026, 5, 1, 8, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset ModifiedAt = new(2026, 5, 1, 9, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Create_Should_Start_As_Draft_And_Reject_Empty_Creator()
    {
        StunningCheckReport.Create(Period(), Guid.Empty, CreatedAt).Error
            .Should().Be(StunningCheckReportErrors.CreatedByUserIdRequiredError);

        var result = StunningCheckReport.Create(Period(), CreatedByUserId, CreatedAt);

        result.IsSuccess.Should().BeTrue();
        result.Value.Status.Should().Be(StunningCheckReportStatus.Draft);
    }

    [Fact]
    public void Draft_Report_Should_Allow_Audited_CheckId_And_Analysis_Changes()
    {
        var report = ValidDraftReport();

        report.AddStunningCheckId(CheckId, ModifiedByUserId, ModifiedAt).IsSuccess.Should().BeTrue();
        report.AddStunningCheckId(CheckId, ModifiedByUserId, ModifiedAt.AddMinutes(1)).IsSuccess.Should().BeTrue();

        report.StunningCheckIds.Should().ContainSingle().Which.Should().Be(CheckId);

        report.UpdateStunningCheckAnalysis(Analysis(), ModifiedByUserId, ModifiedAt.AddMinutes(2)).IsSuccess.Should().BeTrue();
        report.Analysis.Should().NotBeNull();

        report.RemoveStunningCheckId(CheckId, ModifiedByUserId, ModifiedAt.AddMinutes(3)).IsSuccess.Should().BeTrue();
        report.StunningCheckIds.Should().BeEmpty();
    }

    [Fact]
    public void RemoveStunningCheckId_Should_Fail_When_Id_Is_Missing()
    {
        var report = ValidDraftReport();

        report.RemoveStunningCheckId(CheckId, ModifiedByUserId, ModifiedAt).Error
            .Should().Be(StunningCheckReportErrors.FindAndRemoveStunningCheckIdError);
    }

    [Fact]
    public void Finalize_Should_Require_CheckIds_Analysis_Draft_NotDeleted_And_Audit()
    {
        var report = ValidDraftReport();

        report.Finalize(ModifiedByUserId, ModifiedAt).Error
            .Should().Be(StunningCheckReportErrors.StunningCheckIdsAreRequiredForFinalizationError);

        report.AddStunningCheckId(CheckId, ModifiedByUserId, ModifiedAt).IsSuccess.Should().BeTrue();
        report.Finalize(ModifiedByUserId, ModifiedAt.AddMinutes(1)).Error
            .Should().Be(StunningCheckReportErrors.AnalysisIsRequiredForFinalizationError);

        report.UpdateStunningCheckAnalysis(Analysis(), ModifiedByUserId, ModifiedAt.AddMinutes(2)).IsSuccess.Should().BeTrue();
        report.Finalize(Guid.Empty, ModifiedAt.AddMinutes(3)).Error
            .Should().Be(StunningCheckReportErrors.ModifiedByUserIdRequiredError);

        report.Finalize(ModifiedByUserId, ModifiedAt.AddMinutes(3)).IsSuccess.Should().BeTrue();
        report.Status.Should().Be(StunningCheckReportStatus.Finalized);
    }

    [Fact]
    public void Finalized_And_Deleted_Reports_Should_Reject_Behavior_Changes()
    {
        var finalized = Rehydrate(
            checkIds: [CheckId],
            analysis: Analysis(),
            status: StunningCheckReportStatus.Finalized,
            modifiedByUserId: ModifiedByUserId,
            modifiedAt: ModifiedAt).Value;

        finalized.AddStunningCheckId(OtherCheckId, ModifiedByUserId, ModifiedAt.AddMinutes(1)).Error
            .Should().Be(StunningCheckReportErrors.StunningCheckReportIsFinalizedError);

        finalized.Finalize(ModifiedByUserId, ModifiedAt.AddMinutes(1)).Error
            .Should().Be(StunningCheckReportErrors.StunningCheckReportIsAlreadyFinalizedError);

        var deleted = Rehydrate(
            isDeleted: true,
            modifiedByUserId: ModifiedByUserId,
            modifiedAt: ModifiedAt).Value;

        deleted.UpdateStunningCheckAnalysis(Analysis(), ModifiedByUserId, ModifiedAt.AddMinutes(1)).Error
            .Should().Be(StunningCheckReportErrors.StunningCheckReportIsDeletedError);
    }

    [Fact]
    public void Rehydrate_Should_Reject_Duplicate_CheckIds_Invalid_Status_Missing_Audit_And_Invalid_Chronology()
    {
        Rehydrate(checkIds: [CheckId, CheckId]).Error
            .Should().Be(StunningCheckReportErrors.DuplicateStunningCheckIdError);

        Rehydrate(status: (StunningCheckReportStatus)999).Error
            .Should().Be(StunningCheckReportErrors.StunningCheckReportStatusIsInvalidError);

        Rehydrate(isDeleted: true, modifiedByUserId: null, modifiedAt: null).Error
            .Should().Be(StunningCheckReportErrors.ModifiedInfoIsRequiredError);

        Rehydrate(modifiedByUserId: ModifiedByUserId, modifiedAt: CreatedAt.AddTicks(-1)).Error
            .Should().Be(StunningCheckReportErrors.ModifiedAtCannotBeBeforeCreatedAtError);
    }

    [Fact]
    public void MarkAsDeleted_Should_Be_Idempotent()
    {
        var report = ValidDraftReport();

        report.MarkAsDeleted(ModifiedByUserId, ModifiedAt).IsSuccess.Should().BeTrue();
        report.MarkAsDeleted(Guid.Empty, ModifiedAt.AddDays(1)).IsSuccess.Should().BeTrue();

        report.IsDeleted.Should().BeTrue();
        report.ModifiedByUserId.Should().Be(ModifiedByUserId);
    }

    private static StunningCheckReport ValidDraftReport()
    {
        return Rehydrate().Value;
    }

    private static ReportPeriod Period()
    {
        return ReportPeriod.Create(CreatedAt, CreatedAt.AddDays(1)).Value;
    }

    private static StunningCheckAnalysis Analysis() => new();

    private static SomnosSuite.Domain.SharedKernel.Result<StunningCheckReport> Rehydrate(
        IEnumerable<Guid>? checkIds = null,
        StunningCheckAnalysis? analysis = null,
        StunningCheckReportStatus status = StunningCheckReportStatus.Draft,
        Guid? modifiedByUserId = null,
        DateTimeOffset? modifiedAt = null,
        bool isDeleted = false)
    {
        return StunningCheckReport.Rehydrate(
            ReportId,
            Period(),
            CreatedByUserId,
            modifiedByUserId,
            CreatedAt,
            modifiedAt,
            checkIds ?? [],
            analysis,
            status,
            isDeleted);
    }
}
