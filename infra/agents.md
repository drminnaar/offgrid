# agents.md

## Scope
Guidance for local infrastructure and scripts.

## Quick start
- Create env file: ./infra/local/scripts/.env based on .env.example
- Start stack: ./infra/local/scripts/compose.sh up
- Stop stack: ./infra/local/scripts/compose.sh down
- Inspect stack: ./infra/local/scripts/compose.sh ps
- Run Flyway: ./infra/local/scripts/flyway.sh migrate

## Conventions
- Run scripts from Git Bash or WSL on Windows.
- Prefer compose.sh wrapper over direct docker compose commands.

## Structure
- local/ contains compose files and scripts for dev infra
- local/flyway/migrations contains database migrations
- local/keycloak/realms contains realm bootstrap config

## Docs
- Infra README: ./infra/local/README.md
