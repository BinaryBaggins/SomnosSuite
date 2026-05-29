# Domain Plan

This document is the canonical source of truth for the current SomnosSuite domain model. It describes the implemented domain in `backend/src/SomnosSuite.Domain`.

Detailed business rules live in [DOMAIN_RULES.md](DOMAIN_RULES.md). Follow-up work lives in [DOMAIN_ROADMAP.md](DOMAIN_ROADMAP.md).

## Backend Context

The active backend is the layered solution in `backend/backend.sln`:

- `src/SomnosSuite.Domain`
- `src/SomnosSuite.Application`
- `src/SomnosSuite.Infrastructure`
- `src/SomnosSuite.Persistence`
- `src/SomnosSuite.Presentation`
- `src/SomnosSuite.WebApi`
- `tests/SomnosSuite.Domain.Tests`

## Domain Boundaries

The domain project owns business invariants, lifecycle rules, value-object construction, aggregate behavior, and domain error definitions. It does not own persistence, transport DTOs, API validation, import mapping, authorization, report export, mail delivery, or system-clock access.

Current aggregate roots:

- `User`
- `StunningDevice`
- `StunningCheck`
- `StunningCheckReport`

Current value objects:

- `Animal`
- `ReportPeriod`
- `StunningCheckAnalysis`

Current shared kernel concepts:

- `Result` and `Result<T>` for expected domain failures
- `Error` for stable error identity
- `BaseEntity`, `IEntity`, `IAggregateRoot`, and `IValueObject` markers

## Validation Policy

Exceptions are reserved for programming errors and contract violations. Use them when the caller has supplied an invalid object graph or broken a method contract.

Examples:

- required domain object is `null`
- required dependency is `null`
- impossible internal state

`Result.Failure(...)` is used for invalid domain input, invalid persisted state, and invalid state transitions.

Examples:

- required text is missing or whitespace
- `Guid.Empty`
- invalid enum values
- invalid dates or chronology
- invalid lifecycle transitions
- incomplete persisted audit state

Domain methods receive current dates and timestamps from callers. Domain objects do not read system time directly.

## Current Domain Areas

### Animals

`Animal` is a value object. `AnimalKind` and `AnimalCategory` define classification. Large cattle and calves require ear tag number and supplier name; other small livestock can omit those values. Optional text is trimmed and blank optional values normalize to `null`.

### Users

`User` is an aggregate root. It owns identity, name, email, password hash, role, lifecycle status, soft-delete state, and modifier audit metadata. User status transitions are constrained to the implemented workflow.

### Stunning Devices

`StunningDevice` is an aggregate root. It owns device type, manufacturer, serial number, model, animal category, last inspection date, soft-delete state, and modifier audit metadata. Serial-number uniqueness is required by the product but enforced outside the entity by application or persistence code.

### Stunning Checks

`StunningCheck` is an aggregate root for one stunning control. It owns the checked animal, the initial stunning device id, outcome data, restunning data, confirmation state, audit metadata, and soft-delete state. Confirmed checks can only be corrected through `CorrectOutcome(...)`.

### Reports

`ReportPeriod` defines the reporting time range. `StunningCheckReport` is an aggregate root that owns the period, creator audit, included stunning check ids, analysis, draft/finalized lifecycle, soft-delete state, and modifier audit metadata.

## Tested State

The current domain test suite covers the implemented value-object and aggregate rules for animals, users, stunning devices, stunning checks, report periods, and stunning check reports.

Last verified command:

```powershell
dotnet test tests\SomnosSuite.Domain.Tests\SomnosSuite.Domain.Tests.csproj
```

Result on 2026-05-28: 37 tests passed, 0 failed, 0 skipped.

## Documentation Set

- [DOMAIN_PLAN.md](DOMAIN_PLAN.md): current source-of-truth overview.
- [DOMAIN_RULES.md](DOMAIN_RULES.md): detailed rule tables and external mapping reference values.
- [DOMAIN_ROADMAP.md](DOMAIN_ROADMAP.md): next implementation work.
