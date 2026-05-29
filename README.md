# SomnosSuite

SomnosSuite is a web/API suite for livestock stunning-control workflows. It models animals, stunning devices, stunning checks, outcomes, user audit metadata, and reporting concepts for a modern, maintainable control and documentation system.

The current implementation focuses on the core domain model and a lightweight application shell. Domain behavior is documented in [docs/DOMAIN_PLAN.md](docs/DOMAIN_PLAN.md), with detailed rules in [docs/DOMAIN_RULES.md](docs/DOMAIN_RULES.md) and upcoming work in [docs/DOMAIN_ROADMAP.md](docs/DOMAIN_ROADMAP.md).

## Repository Structure

```text
.
├── backend/
│   ├── backend.sln
│   ├── src/
│   │   ├── SomnosSuite.Domain/
│   │   ├── SomnosSuite.Application/
│   │   ├── SomnosSuite.Infrastructure/
│   │   ├── SomnosSuite.Persistence/
│   │   ├── SomnosSuite.Presentation/
│   │   └── SomnosSuite.WebApi/
│   └── tests/
│       └── SomnosSuite.Domain.Tests/
├── docs/
│   ├── DOMAIN_PLAN.md
│   ├── DOMAIN_RULES.md
│   └── DOMAIN_ROADMAP.md
└── frontend/
    ├── src/
    ├── static/
    ├── package.json
    └── .env.example
```

- `backend/` contains the layered .NET solution, ASP.NET Core Web API entry point, and domain tests.
- `frontend/` contains the SvelteKit/Vite frontend.
- `docs/` contains product and domain documentation shared across the repository.

## Prerequisites

- .NET SDK `10.0.300` as pinned in `global.json`
- Node.js `22.13.0` or newer as pinned in `.nvmrc`
- npm `10` or newer

## Backend

From the repository root:

```powershell
cd backend
dotnet restore backend.sln
dotnet build backend.sln
dotnet test backend.sln
```

Run only the current domain test project:

```powershell
cd backend
dotnet test tests\SomnosSuite.Domain.Tests\SomnosSuite.Domain.Tests.csproj
```

Run the Web API locally:

```powershell
cd backend
dotnet run --project src\SomnosSuite.WebApi\SomnosSuite.WebApi.csproj --launch-profile https
```

The current HTTPS launch profile serves the API at `https://localhost:7086` and also exposes `http://localhost:5063`.

## Frontend

Requires Node.js and npm. From the repository root:

```powershell
cd frontend
npm install
```

Use `npm ci` instead of `npm install` when you want a clean install from `package-lock.json`.

Common frontend commands:

```powershell
npm run check
npm run lint
npm run build
npm run dev
```

## Environment Setup

Frontend environment values are documented in `frontend\.env.example`.

Create your local frontend env file:

```powershell
cd frontend
Copy-Item .env.example .env
```

The public frontend API base URL is configured with:

```env
PUBLIC_API_BASE_URL="https://localhost:7086"
```

Adjust this value if you run the backend on a different local URL. Do not commit local `.env` files or secrets; only example files should be tracked.
