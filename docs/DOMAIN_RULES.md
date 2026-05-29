# Domain Rules

This document records the detailed business rules implemented in `backend/src/SomnosSuite.Domain`. Keep this file aligned with domain tests in `backend/tests/SomnosSuite.Domain.Tests`.

## Shared Policies

| Area | Rule |
| --- | --- |
| Aggregate construction | Aggregate roots expose explicit `Create(...)` factories for new instances. |
| Aggregate rehydration | Aggregate roots expose explicit `Rehydrate(...)` factories for persisted state validation. |
| Value objects | Value objects use `Create(...)`; separate rehydration factories are not required. |
| Time | Domain methods receive current dates and timestamps from callers. Domain code does not call system time directly. |
| Expected failures | Invalid domain input, invalid persisted state, and invalid transitions return `Result.Failure(...)`. |
| Contract failures | Required domain objects passed as `null` throw exceptions. |
| Soft delete | `MarkAsDeleted(...)` returns `Result`, rejects empty modifier ids, and repeated delete is success/no-op. |
| Deleted aggregates | Deleted aggregates reject behavior changes other than idempotent delete. |
| Audit metadata | Mutating aggregate behavior requires modifier audit where implemented. |
| Rehydrated deleted state | Deleted persisted aggregates require complete modifier audit. |

## Animal

`Animal` is a value object.

| Rule | Behavior |
| --- | --- |
| Kind | `AnimalKind` must be a defined enum value. |
| Category | `AnimalCategory` is derived from `AnimalKind` through `AnimalClassification`. |
| Large cattle | Requires ear tag number and supplier name. |
| Calf | Requires ear tag number and supplier name. |
| Other small livestock | Can be created without ear tag number and supplier name. |
| Optional text | Optional text values are trimmed. Blank optional values normalize to `null`. |

## User

`User` is an aggregate root.

| Rule | Behavior |
| --- | --- |
| Required fields | Name, email, password hash, role, status, and created timestamp are required. |
| Name | Trimmed and must not be blank. |
| Email | Trimmed, required, and validated as an address string. |
| Password hash | Required and stored exactly as supplied; it is not trimmed. |
| Role | `UserRole` must be a defined enum value. |
| Status | `UserStatus` must be a defined enum value. |
| Allowed transitions | `Invited -> Active`, `Invited -> Deactivated`, `Active -> Deactivated`, `Deactivated -> Active`. |
| Updates | Name, email, password hash, role, and status updates require modifier audit. |
| Rehydration | Requires non-empty id, valid fields, consistent modifier audit, and `UpdatedAt >= CreatedAt` when updated. |
| Soft delete | Deleted users reject non-delete changes. Rehydrated deleted users require modifier audit. |

## StunningDevice

`StunningDevice` is an aggregate root.

| Rule | Behavior |
| --- | --- |
| Required fields | Device type, manufacturer, serial number, model, animal category, and caller-supplied `today` are required. |
| Text | Manufacturer, serial number, and model are trimmed and must not be blank. |
| Enums | `StunningDeviceType` and `AnimalCategory` must be defined enum values. |
| Inspection date | Last inspection date cannot be in the future relative to caller-supplied `today`. |
| Record inspection | New inspection date cannot be in the future or older than the current last inspection date. |
| Audit | Recording an inspection requires modifier audit. |
| Rehydration | Requires non-empty id, valid fields, and consistent modifier audit. |
| Soft delete | Deleted devices reject inspection changes. Rehydrated deleted devices require modifier audit. |
| Uniqueness | Serial-number uniqueness is required, but enforced outside the entity in application or persistence. |

## StunningCheck

`StunningCheck` is an aggregate root for one stunning control.

| Rule | Behavior |
| --- | --- |
| Creation | Requires a non-null `Animal`, non-empty initial stunning device id, and created timestamp. |
| Initial state | New checks start in `Created` status. |
| Recording | `RecordOutcome(...)` records the first outcome and moves the check to `Confirmed`. |
| Recording audit | Recording requires non-empty recorded-by user id and `RecordedAt >= CreatedAt`. |
| Re-recording | Confirmed checks cannot be recorded again. |
| Correction | `CorrectOutcome(...)` is the only correction path and requires the check to already be confirmed. |
| Correction audit | Corrections require modifier audit and `ModifiedAt >= CreatedAt`. |
| Successful outcome | Allows no failure indicator, restunning timing, or restunning device id. |
| Failed outcome | Requires failure indicator, restunning timing, and non-empty restunning device id. |
| Rehydration | Requires non-empty id, valid lifecycle state, valid outcome rules, consistent audit state, and minimum chronology. |
| Soft delete | Deleted checks reject recording and correction. Rehydrated deleted checks require modifier audit. |

## ReportPeriod

`ReportPeriod` is a value object.

| Rule | Behavior |
| --- | --- |
| Start | Start is required. |
| End | End is required. |
| Chronology | End must be after start. |
| Semantics | The implemented names are `StartInclusive` and `EndExclusive`. |

## StunningCheckReport

`StunningCheckReport` is an aggregate root.

| Rule | Behavior |
| --- | --- |
| Creation | Requires non-null report period, non-empty created-by user id, and created timestamp. |
| Initial state | Reports start in `Draft`. |
| Draft changes | Draft reports can add/remove stunning check ids and update analysis. |
| Add duplicate | Adding an already included check id is success/no-op. |
| Remove missing | Removing a missing check id fails. |
| Finalization | Requires draft status, not deleted, at least one stunning check id, analysis, and modifier audit. |
| Finalized state | Finalized reports reject check id and analysis changes. |
| Rehydration | Requires non-empty id, valid status, valid audit, no empty check ids, no duplicate check ids, and `ModifiedAt >= CreatedAt` when modified. |
| Analysis audit | Rehydrated reports with analysis require modifier audit. |
| Finalized audit | Rehydrated finalized reports require modifier audit. |
| Soft delete | Deleted reports reject behavior changes except idempotent delete. |

## External Mapping Reference

String mapping from external data sources belongs outside the domain, in import, API, or application mapping code. These values are preserved as neutral integration examples.

| External value | Domain value |
| --- | --- |
| `gut` | `StunningOutcome.Successful` |
| `(Augen-)Reflexe` | `StunningFailureIndicator.Reflex` |
| `Reflex des Tieres` | `StunningFailureIndicator.Reflex` |
| `Lautaeusserung` | `StunningFailureIndicator.Vocalization` |
| `Schnappatmung` | `StunningFailureIndicator.Gasping` |
| `Bolzenschuss vor Entblutung` | `RestunningTiming.BeforeBleeding` |
| `Bolzenschuss nach Entblutung` | `RestunningTiming.AfterBleeding` |
| `CO2` | `StunningDeviceType.CarbonDioxide` |
| `Bolzenschuss` | `StunningDeviceType.CaptiveBolt` |
