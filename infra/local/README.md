# Local Infrastructure Guide

This folder contains the local infrastructure used for development and testing. It includes Docker Compose files, and helper scripts to bring the stack up/down and to access services.

## Purpose

- Provide a reproducible local environment for services used by the project.

---

## Folder Structure

```yaml

infra
│
└── local
    ├── postgres             # postgres service config
    ├── scripts              # collection of bash scripts to manage stack and connect to services
    │    ├── compose.sh      # manage the stack (up, down, exec, ps, logs)
    │    ├── psql.sh         # open a psql session for the local Postgres
    │    ├── psql-test.sh    # test postgres connectivity
    │    ├── .env            # local environment settings (must be created based on `.env.example` file)
    │    └── .env.example    # example environment settings that are required to correctly run the local stack
    └── compose.yaml         # top-level compose file that coordinates local services.

```

> [!NOTE]
> &nbsp;  
> Postgres:
> - `compose.postgres.yaml` — Defines postgres compose service for local development.

---

## Getting Started

> [!IMPORTANT]
> - Run these commands from a Git Bash / POSIX shell (on Windows use Git Bash or WSL).
>
> - Ensure that all scripts have execute (`x`) permissions. Run `chmod +x my-script.sh` to add execute permissions.

- Step 1 - Set environment variables:

  Ensure that a `.env` file is created at the root of the local infrastructure scripts folder (`./infra/local/scripts/.env`). These environment variables are used by the scripts that manage various services.

  See  [`./infra/local/scripts/.env.example`](../../infra/local/scripts/.env.example).

  ```yaml
  # postgres environment variables
  OG_POSTGRES_USER=
  OG_POSTGRES_PASSWORD=
  OG_POSTGRES_DB=

  # postgres pgadmin4 environment variables
  OG_PGADMIN_DEFAULT_EMAIL=
  OG_PGADMIN_DEFAULT_PASSWORD=
  ```

- Step 2 - Start the stack:

  ```bash

  ./infra/local/scripts/compose.sh up
  
  ```

  Alternatively, run the bundled VS Code tasks (Tasks menu or `Ctrl+Shft+b`) which calls the same script: `bash: compose up`

---

## Common Commands

> [!IMPORTANT]
> - Run these commands from a Git Bash / POSIX shell (on Windows use Git Bash or WSL).
>
> - Ensure that all scripts have execute (`x`) permissions. Run `chmod +x my-script.sh` to add execute permissions.

### Manage Stack

- Start the stack:
  
  ```bash

  ./infra/local/scripts/compose.sh up
  
  ```

  Alternatively, run the bundled VS Code tasks (Tasks menu or `Ctrl+Shft+b`) which calls the same script: `bash: compose up`

- Stop the stack:
  
  ```bash
  
  ./infra/local/scripts/compose.sh down
  
  ```
- Show running services:
  
  ```bash
  
  ./infra/local/scripts/compose.sh ps

  ```
- Show logs for either all services, or specific services:
  
  ```bash

  # show all logs for all services
  ./infra/local/scripts/compose.sh logs

  # show all logs for specific services
  ./infra/local/scripts/compose.sh logs postgres

  ```

### Connect to Services

- Connect to postgres database using `psql`:

  ```bash

  ./infra/local/scripts/psql.sh

  ```

- Test Connection to postgres
  
  ```bash

  ./infra/local/scripts/psql-test.sh

  ```

---

## Notes & Recommendations

- On Windows, prefer running scripts with Git Bash or WSL to avoid shell incompatibilities.

- If ports conflict, inspect compose.yaml and service-specific compose files for port mappings.

- For debugging, inspect logs with:
  - `compose.sh logs` or `compose.sh logs <<service_name>>`
  - open DB shells with `psql.sh`

---
