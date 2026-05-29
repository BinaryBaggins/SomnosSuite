# Domain Roadmap

This document tracks follow-up work after the current domain stabilization pass. [DOMAIN_PLAN.md](DOMAIN_PLAN.md) remains the current-state source of truth, and [DOMAIN_RULES.md](DOMAIN_RULES.md) records detailed implemented rules.

## Next Integration Work

| Area | Work |
| --- | --- |
| Application layer | Introduce use cases that call domain factories and behavior methods instead of constructing state directly. |
| API layer | Map request DTOs into domain inputs and translate `Result` failures into stable API responses. |
| Time handling | Supply current dates and timestamps from application/API services rather than reading time inside the domain. |
| Persistence | Rehydrate aggregates through explicit `Rehydrate(...)` factories when loading persisted state. |
| Serial numbers | Enforce stunning-device serial-number uniqueness in application or persistence. |

## Reporting Work

| Area | Work |
| --- | --- |
| Analysis calculation | Implement real `StunningCheckAnalysis` calculation from included stunning checks. |
| Export | Add PDF/export generation outside the domain model. |
| Delivery | Add mail integration outside the domain model. |
| Finalization flow | Wire finalized reports into application/API workflows once persistence exists. |

## Audit Work

Current aggregates store only the latest modifier metadata. Future audit work should add explicit audit history where the product needs traceability beyond the current modified-by/modified-at fields.

Keep the current rule that behavior methods require caller-supplied audit data. Do not let domain objects read the current user or current time directly.

## Integration Notes

Mappings from external strings to domain enums belong in import, API, or application mapping code. They should not be embedded in domain entities or value objects.

## Later Cleanup

| Area | Work |
| --- | --- |
| WebApi surface | Replace placeholder endpoints with real application endpoints as API work starts. |
| Tests | Add application/API tests when domain integration begins. |
| Documentation | Keep this roadmap focused on remaining work; move implemented behavior into `DOMAIN_PLAN.md` or `DOMAIN_RULES.md`. |
