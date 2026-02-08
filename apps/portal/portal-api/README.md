# Portal API

This is the primary API used by the Portal admin app.

---

## 📐 Design

### High Level Solution Design

See [design documentation](../docs/design):

- [Version 1 - README (Current)](../docs/design/version-1/README.md) - Represents `version 1` target state.

### High Level API Design

The overall API design follows a layered architecture using a [Modular Monolith](https://www.thoughtworks.com/en-us/insights/blog/microservices/modular-monolith-better-way-build-software) design. Each module uses a layered architecture similar to the [Clean Architecture](https://grokipedia.com/page/Clean_Architecture).

The following diagram illustrates that an API will use one or more modules to satisfy the the API resource requirements:

<br />

![](./docs/design/hld-level-1.png)

<br />

The following diagram illustrates the various layers of the architecture, along with the direction of dependencies:

<br />

![](./docs/design/hld-level-2.png)

<br />

---

## 🚀 Getting Started

### 1. Run Infra Services

Ensure that you have followed the project infrastructure [README](../../../infra/local/README.md) guide and have the required services running on your local machine.

The following requirements must be satisfied before running Portal API:

- ✅️ Postgresql service is running
- ✅️ Keycloak service is running
- ✅️ Flyway migrations applied

### 2. Start API

```bash
# ./apps/portal/portal-api

dotnet watch run ./src/PortalApi

```

### 3. Run Requests

The [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) VSCode extension is required to run requests.

- Root:
  - See [`./requests/root.http`](./requests/root.http)
  - See [`./requests/customers-reinstate.http`](./requests/customers-reinstate.http)
  - See [`./requests/customers-suspend.http`](./requests/customers-suspend.http)

---
