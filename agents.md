# Role: E-Commerce Solutions Architect and expert on React, Nextjs, Typescript, .NET 10 and C# 14

## Scope

Repo-wide guidance for working in the Offgrid monorepo.

## Quick start

### Infrastructure and local environment

- Check prerequisites: `./scripts/prereq-check.sh`
- Start infra: `./infra/local/scripts/compose.sh up`

### Portal Suite/System

- Run portal app: `npm run dev --prefix ./apps/portal-app`
- Run portal API: `dotnet watch run --project ./services/portal/src/Offgrid.Portal.Api`
- Run portal outbox processor: `dotnet watch run --project ./services/portal/src/Offgrid.Portal.Csutomers.OutboxProcessor`
- Run portal outbox processor: `dotnet watch run --project ./services/portal/src/Offgrid.Portal.Csutomers.EventProcessor`

### Shop Suite/System

- Run shop app: `npm run dev --prefix ./apps/shop-app`
- Run shop API: `dotnet watch run --project ./services/shop/src/Offgrid.Shop.Api`

## Conventions

- Git commits follow Conventional Commits. See ./docs/standards/git/git-commit-convention.md
- Prefer Git Bash or WSL for shell scripts on Windows.
- Keep changes scoped to the app/service you are working on.

## Structure

- `apps/` contains frontends (shop-app and portal-app)
- `services/` contains .NET backends (shop and portal)
- `infra/local` contains local Docker Compose stack and scripts
- `docs/` contains design and standards
- `libs/` contains shared libraries
- `scripts/` contains general monorepo helper scripts

## Docs

- Repo overview: `./README.md`
- Infra guide: `./infra/local/README.md`
- General design docs: `./docs/design/`
- Portal suite design docs: `./docs/portal/design`
- Shop suite design docs: `./docs/shop/design`
- App docs: `./apps/shop-app/README.md` and `./apps/portal-app/README.md`
- Service docs: `./services/shop/README.md` and `./services/portal/README.md`
