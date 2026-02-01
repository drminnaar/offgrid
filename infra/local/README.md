# Local Infrastructure Guide

This folder contains the local infrastructure used for development and testing. It includes Docker Compose files, and helper scripts to bring the stack up/down and to access services.

## Purpose

- Provide a reproducible local environment for services used by the project.

---

## 🗂️ Folder Structure

```yaml

infra
│
└── local
    ├── postgres             # postgres service config
    ├── flyway               # flyway service config
    ├── keycloak             # keycloak service config
    ├── scripts              # collection of bash scripts to manage stack and connect to services
    │    ├── compose.sh      # manage the stack (up, down, exec, ps, logs, recreate)
    │    ├── flyway.sh       # manage the flyway migrations (migrate, info, validate etc)
    │    ├── psql.sh         # open a psql session for the local Postgres
    │    ├── psql-test.sh    # test postgres connectivity
    │    ├── .env            # local environment settings (must be created based on `.env.example` file)
    │    └── .env.example    # example environment settings that are required to correctly run the local stack
    └── compose.yaml         # top-level compose file that coordinates local services.

```

> [!NOTE]
> &nbsp;  
> Postgres:
> - [`./postgres/compose.yaml`](./postgres/compose.yaml) — Defines postgres compose service for local development.
>
> Flyway:
> - [`./flyway/compose.yaml`](./flyway/compose.yaml) — Define Flyway compose service to manage Flyway databse migrations.
> - [`./flyway/migrations/*.pgsl`](./flyway/migrations) - Migrations folder that contains all database migration definitions
>
> Keycloak:  
> - [`./keycloak/compose.yaml`](./keycloak/compose.yaml) — Keycloak compose file and overrides.
> - [`./keycloak/realms/offgrid-public-realm.json`](./keycloak/realms/offgrid-public-realm.json) — exported realm configuration used to initialize Keycloak.
> - [`./keycloak/requests/auth.http`](./keycloak/requests/auth.http) — example HTTP requests demonstrating auth. 

---

## 🚀 Getting Started

> [!IMPORTANT]
> - Run these commands from a Git Bash / POSIX shell (on Windows use Git Bash or WSL).
>
> - Ensure that all scripts have execute (`x`) permissions. Run `chmod +x my-script.sh` to add execute permissions.

- Step 1 - Set environment variables:

  Ensure that a `.env` file is created at the root of the local infrastructure scripts folder (`./infra/local/scripts/.env`). These environment variables are used by the scripts that manage various services.

  See  [`./infra/local/scripts/.env.example`](./scripts/.env.example).

  ```yaml
  # postgres environment variables
  OG_POSTGRES_USER=
  OG_POSTGRES_PASSWORD=
  OG_POSTGRES_DB=

  # postgres pgadmin4 environment variables
  OG_PGADMIN_DEFAULT_EMAIL=
  OG_PGADMIN_DEFAULT_PASSWORD=

  # keycloak environment variables
  OG_KC_BOOTSTRAP_ADMIN_USERNAME=
  OG_KC_BOOTSTRAP_ADMIN_PASSWORD=
  ```

- Step 2 - Start the stack:

  ```bash

  ./infra/local/scripts/compose.sh up
  
  ```

  Alternatively, run the bundled VS Code tasks (Tasks menu or `Ctrl+Shft+b`) which calls the same script: `bash: compose up`

- Step 3 - Access services:

  - **Keycloak UI:** `http://localhost:8080`
  - **Keycloak Admin CLI:** `./infra/local/scripts/kcadm.sh`

---

## ☰ Common Commands

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

- Use Flyway CLI:

  ```bash

  ./infra/local/scripts/flyway.sh info

  ```

- Use Keycloak admin CLI:

  ```bash

  ./infra/local/scripts/kcadm.sh

  ```

---

## 🗝️ About Keycloak Service

[Keycloak](https://www.keycloak.org) is an open-source identity and access management server that provides login, user management, and OAuth2/OpenID Connect authentication for apps and APIs.

See [Keycloak Guide](https://github.com/drminnaar/tech-notes/tree/main/guides/keycloak) for more information on setup.

As part of container initialization, the following `realm` files are imported into keycloak to provide basic realm setup with users, groups, and roles.

- [offgrid-public.json](./keycloak/realms/offgrid-public-realm.json). See [below](#keycloak-realm-configuration-offgrid-public) for more details.

### Keycloak Realm Configuration (offgrid-public)

Visually, the keycloak configuration can be viewed as follows:

```mermaid
---
title: Keycloak Realm Configuration - Entity Relationships
config:
    layout: elk
---
erDiagram
    CLIENT ||--o{ REALM_ROLE : defines
    CLIENT ||--o{ CLIENT_ROLE : defines
    REALM_ROLE ||--o{ REALM_ROLE : "composite"
    USER_GROUP ||--o{ REALM_ROLE : "has"
    USER_GROUP ||--o{ CLIENT_ROLE : "has"
    USER ||--o{ USER_GROUP : "member of"
    
    CLIENT {
        string clientId
        string name
        string description
    }
    REALM_ROLE {
        string name
        string description
        boolean composite
    }
    CLIENT_ROLE {
        string name
        string description
    }
    USER_GROUP {
        string name
        string description
        string path
    }
    USER {
        string username
        string email
        string firstName
        string lastName
        boolean enabled
    }
```

- Key Relationships:

  - Clients define both Realm Roles and Client Roles
  - Realm Roles can be composite (contain other realm roles) - like customer-gold → customer-silver → customer-standard hierarchy
  - User Groups have assigned Realm Roles and Client Roles
  - Users are members of User Groups

- Specific Structure for Shop app:

- Clients: shop-app (confidential client), shop-api (bearer token only)
- Groups: customer-standard, customer-silver, customer-gold
- Roles: Hierarchical composite roles where customer-gold inherits from customer-silver, which inherits from customer-standard
- Users: johndoe (standard), janedoe (silver), alicewonder (gold)

#### Purpose

Defines a Keycloak realm used by the Next.js shop frontend and the .NET 10 Shop API.

#### Realm Settings

- Registration enabled  
- Email login allowed  
- Password reset allowed  
- Default group: `/customer-standard`

#### Roles

##### Realm Roles (Customer Tiers)

- `customer-standard` (base tier)  
- `customer-silver` (composite: includes standard)  
- `customer-gold` (composite: includes silver)

##### Client Roles

- `shop-api` → `api-access` (API access gate)

#### Groups

- `customer-standard`, `customer-silver`, `customer-gold`  
  - Each group assigns the corresponding realm role  
  - Each group also grants `shop-api` → `api-access`  
  - Descriptions clarify tier benefits

#### Clients

##### shop-app

- Public client for browser logins  
- Audience mapper adds `shop-api` to `aud` in tokens

##### shop-api

- Bearer-only client used for API token validation

#### Seed Users

- `johndoe` → `/customer-standard`  
- `janedoe` → `/customer-silver`  
- `alicewonder` → `/customer-gold`

#### Token Behavior

- Customer tiers appear in `realm_access.roles`  
- API should validate `aud = shop-api` and authorize via realm roles

---

## 📝 Notes & Recommendations

- On Windows, prefer running scripts with Git Bash or WSL to avoid shell incompatibilities.

- If ports conflict, inspect compose.yaml and service-specific compose files for port mappings.

- For debugging, inspect logs with:
  - `compose.sh logs` or `compose.sh logs <<service_name>>`
  - open DB shells with `psql.sh`

---
