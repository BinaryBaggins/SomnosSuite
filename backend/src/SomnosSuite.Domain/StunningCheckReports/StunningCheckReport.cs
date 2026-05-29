using SomnosSuite.Domain.SharedKernel;

namespace SomnosSuite.Domain.StunningCheckReports
{
    public sealed class StunningCheckReport : BaseEntity, IAggregateRoot
    {
        public ReportPeriod Period { get; private set; } = null!;
        public Guid CreatedByUserId { get; private set; }
        public Guid? ModifiedByUserId { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? ModifiedAt { get; private set; }
        private readonly HashSet<Guid> _stunningCheckIds = [];
        public IReadOnlySet<Guid> StunningCheckIds => _stunningCheckIds;
        public StunningCheckAnalysis? Analysis { get; private set; }
        public StunningCheckReportStatus Status { get; private set; }
        public bool IsDeleted { get; private set; }

        private StunningCheckReport() { }

        private StunningCheckReport(ReportPeriod period, Guid createdByUserId, DateTimeOffset createdAt)
        {
            Period = period;
            CreatedByUserId = createdByUserId;
            CreatedAt = createdAt;
            Status = StunningCheckReportStatus.Draft;
        }

        private StunningCheckReport(
            Guid id,
            ReportPeriod period,
            Guid createdByUserId,
            Guid? modifiedByUserId,
            DateTimeOffset createdAt,
            DateTimeOffset? modifiedAt,
            IEnumerable<Guid> stunningCheckIds,
            StunningCheckAnalysis? analysis,
            StunningCheckReportStatus status,
            bool isDeleted)
        : base(id)
        {
            Period = period;
            CreatedByUserId = createdByUserId;
            ModifiedByUserId = modifiedByUserId;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            _stunningCheckIds.UnionWith(stunningCheckIds);
            Analysis = analysis;
            Status = status;
            IsDeleted = isDeleted;
        }

        public static Result<StunningCheckReport> Create(
            ReportPeriod reportPeriod,
            Guid createdByUserId,
            DateTimeOffset createdAt)
        {
            ArgumentNullException.ThrowIfNull(reportPeriod, nameof(reportPeriod));

            if (createdByUserId == Guid.Empty)
                return Result<StunningCheckReport>.Failure(StunningCheckReportErrors.CreatedByUserIdRequiredError);

            return new StunningCheckReport(reportPeriod, createdByUserId, createdAt);
        }

        public static Result<StunningCheckReport> Rehydrate(
            Guid id,
            ReportPeriod reportPeriod,
            Guid createdByUserId,
            Guid? modifiedByUserId,
            DateTimeOffset createdAt,
            DateTimeOffset? modifiedAt,
            IEnumerable<Guid> stunningCheckIds,
            StunningCheckAnalysis? analysis,
            StunningCheckReportStatus status,
            bool isDeleted)
        {
            if (id == Guid.Empty)
                return Result<StunningCheckReport>.Failure(StunningCheckReportErrors.InvalidIdError);

            ArgumentNullException.ThrowIfNull(reportPeriod, nameof(reportPeriod));
            ArgumentNullException.ThrowIfNull(stunningCheckIds, nameof(stunningCheckIds));

            if (createdByUserId == Guid.Empty)
                return Result<StunningCheckReport>.Failure(StunningCheckReportErrors.CreatedByUserIdRequiredError);

            if (!Enum.IsDefined(status))
                return Result<StunningCheckReport>.Failure(StunningCheckReportErrors.StunningCheckReportStatusIsInvalidError);

            var checkIds = stunningCheckIds.ToArray();

            if (checkIds.Any(stunningCheckId => stunningCheckId == Guid.Empty))
                return Result<StunningCheckReport>.Failure(StunningCheckReportErrors.StunningCheckIdRequiredError);

            if (checkIds.Length != checkIds.Distinct().Count())
                return Result<StunningCheckReport>.Failure(StunningCheckReportErrors.DuplicateStunningCheckIdError);

            var auditValidation = ValidateRehydratedAuditState(
                modifiedByUserId,
                modifiedAt,
                createdAt,
                analysis,
                status,
                isDeleted);

            if (auditValidation.IsFailure)
                return Result<StunningCheckReport>.Failure(auditValidation.Error);

            var finalizedStateValidation = ValidateFinalizedState(status, checkIds.Length, analysis);
            if (finalizedStateValidation.IsFailure)
                return Result<StunningCheckReport>.Failure(finalizedStateValidation.Error);

            return new StunningCheckReport(
                id,
                reportPeriod,
                createdByUserId,
                modifiedByUserId,
                createdAt,
                modifiedAt,
                checkIds,
                analysis,
                status,
                isDeleted);
        }

        public Result UpdateStunningCheckAnalysis(
            StunningCheckAnalysis analysis,
            Guid modifiedByUserId,
            DateTimeOffset modifiedAt)
        {
            ArgumentNullException.ThrowIfNull(analysis, nameof(analysis));

            var changeValidation = ValidateCanChange();
            if (changeValidation.IsFailure)
                return changeValidation;

            var modifiedInfoResult = UpdateModifiedInfo(modifiedByUserId, modifiedAt);
            if (modifiedInfoResult.IsFailure)
                return modifiedInfoResult;

            Analysis = analysis;

            return Result.Success();
        }

        public Result AddStunningCheckId(
            Guid stunningCheckId,
            Guid modifiedByUserId,
            DateTimeOffset modifiedAt)
        {
            var changeValidation = ValidateCanChange();
            if (changeValidation.IsFailure)
                return changeValidation;

            if (stunningCheckId == Guid.Empty)
                return Result.Failure(StunningCheckReportErrors.StunningCheckIdRequiredError);

            if (_stunningCheckIds.Contains(stunningCheckId))
                return Result.Success();

            var modifiedInfoResult = UpdateModifiedInfo(modifiedByUserId, modifiedAt);
            if (modifiedInfoResult.IsFailure)
                return modifiedInfoResult;

            _stunningCheckIds.Add(stunningCheckId);

            return Result.Success();
        }

        public Result RemoveStunningCheckId(
            Guid stunningCheckId,
            Guid modifiedByUserId,
            DateTimeOffset modifiedAt)
        {
            var changeValidation = ValidateCanChange();
            if (changeValidation.IsFailure)
                return changeValidation;

            if (stunningCheckId == Guid.Empty)
                return Result.Failure(StunningCheckReportErrors.StunningCheckIdRequiredError);

            if (!_stunningCheckIds.Contains(stunningCheckId))
                return Result.Failure(StunningCheckReportErrors.FindAndRemoveStunningCheckIdError);

            var modifiedInfoResult = UpdateModifiedInfo(modifiedByUserId, modifiedAt);
            if (modifiedInfoResult.IsFailure)
                return modifiedInfoResult;

            _stunningCheckIds.Remove(stunningCheckId);

            return Result.Success();
        }

        public Result Finalize(Guid modifiedByUserId, DateTimeOffset modifiedAt)
        {
            if (IsDeleted)
                return Result.Failure(StunningCheckReportErrors.StunningCheckReportIsDeletedError);

            if (Status == StunningCheckReportStatus.Finalized)
                return Result.Failure(StunningCheckReportErrors.StunningCheckReportIsAlreadyFinalizedError);

            var finalizedStateValidation = ValidateFinalizedState(
                StunningCheckReportStatus.Finalized,
                _stunningCheckIds.Count,
                Analysis);
            if (finalizedStateValidation.IsFailure)
                return finalizedStateValidation;

            var modifiedInfoResult = UpdateModifiedInfo(modifiedByUserId, modifiedAt);
            if (modifiedInfoResult.IsFailure)
                return modifiedInfoResult;

            Status = StunningCheckReportStatus.Finalized;

            return Result.Success();
        }

        public Result MarkAsDeleted(Guid modifiedByUserId, DateTimeOffset modifiedAt)
        {
            if (IsDeleted)
                return Result.Success();

            var modifiedInfoResult = UpdateModifiedInfo(modifiedByUserId, modifiedAt);
            if (modifiedInfoResult.IsFailure)
                return modifiedInfoResult;

            IsDeleted = true;
            return Result.Success();
        }

        private Result ValidateCanChange()
        {
            if (IsDeleted)
                return Result.Failure(StunningCheckReportErrors.StunningCheckReportIsDeletedError);

            if (Status == StunningCheckReportStatus.Finalized)
                return Result.Failure(StunningCheckReportErrors.StunningCheckReportIsFinalizedError);

            return Result.Success();
        }

        private Result UpdateModifiedInfo(Guid modifiedByUserId, DateTimeOffset modifiedAt)
        {
            if (modifiedByUserId == Guid.Empty)
                return Result.Failure(StunningCheckReportErrors.ModifiedByUserIdRequiredError);

            if (modifiedAt < CreatedAt)
                return Result.Failure(StunningCheckReportErrors.ModifiedAtCannotBeBeforeCreatedAtError);

            ModifiedByUserId = modifiedByUserId;
            ModifiedAt = modifiedAt;

            return Result.Success();
        }

        private static Result ValidateRehydratedAuditState(
            Guid? modifiedByUserId,
            DateTimeOffset? modifiedAt,
            DateTimeOffset createdAt,
            StunningCheckAnalysis? analysis,
            StunningCheckReportStatus status,
            bool isDeleted)
        {
            if (modifiedByUserId.HasValue != modifiedAt.HasValue)
                return Result.Failure(StunningCheckReportErrors.ModifiedInfoIsIncompleteError);

            if (modifiedByUserId == Guid.Empty)
                return Result.Failure(StunningCheckReportErrors.ModifiedByUserIdRequiredError);

            if (modifiedAt.HasValue && modifiedAt.Value < createdAt)
                return Result.Failure(StunningCheckReportErrors.ModifiedAtCannotBeBeforeCreatedAtError);

            if ((isDeleted || analysis is not null || status == StunningCheckReportStatus.Finalized) &&
                (!modifiedByUserId.HasValue || !modifiedAt.HasValue))
            {
                return Result.Failure(StunningCheckReportErrors.ModifiedInfoIsRequiredError);
            }

            return Result.Success();
        }

        private static Result ValidateFinalizedState(
            StunningCheckReportStatus status,
            int stunningCheckIdsCount,
            StunningCheckAnalysis? analysis)
        {
            if (status != StunningCheckReportStatus.Finalized)
                return Result.Success();

            if (stunningCheckIdsCount == 0)
                return Result.Failure(StunningCheckReportErrors.StunningCheckIdsAreRequiredForFinalizationError);

            if (analysis is null)
                return Result.Failure(StunningCheckReportErrors.AnalysisIsRequiredForFinalizationError);

            return Result.Success();
        }
    }
}
