# Copilot Instructions (Offgrid)

Use these repo-wide rules for all Copilot suggestions.

## Repository Overview

- Full-stack monorepo: .NET API services + React SPA + Next.js SSR/SSG app + Docker Compose for local dev

## Primary guidance

- Read and follow the nearest agents.md first (root or local). Start at ./agents.md.
- Keep changes scoped to the app/service you are working on.

## Workflows

- Use Git Bash or WSL for shell scripts on Windows.
- Prefer repo scripts over ad-hoc commands (e.g., ./scripts/prereq-check.sh, ./infra/local/scripts/compose.sh).

## Standards

- Git commits follow Conventional Commits: ./docs/standards/git/git-commit-convention.md

## Tech stack

- Frontend: Next.js (shop-app), React + Vite (portal-app), TypeScript
- Backend: .NET 10 (C# 14)
- Infra: Docker Compose, Keycloak, Postgres, RabbitMQ, Flyway
