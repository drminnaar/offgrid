# Shop API

This is the primary API used by the Shop website.

---

## 📐 Design

### High Level Solution Design

See [design documentation](../docs/design/version-1/README.md):

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

## 🛠️ Initial Installation

This is a [.NET 10](https://dotnet.microsoft.com/en-us/) - [Minimal API project](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-10.0) created with [`dotnet CLI`](https://learn.microsoft.com/en-us/dotnet/core/tools/).

The initial project was created as follows:

```bash

dotnet new sln --name Offgrid
dotnet new webapi --no-https --no-openapi --use-minimal-apis --output ./src/ShopApi
dotnet sln ./Offgrid add ./src/ShopApi

```

---

## 🚀 Getting Started

### 1. Run Infra Services

Ensure that you have followed the project infrastructure [README](../../infra/local/README.md) guide and have the required services running on your local machine.

The following requirements must be satisfied before running Shop API:

- ✅️ Postgresql service is running
- ✅️ Flyway migrations applied

### 2. Start API

```bash

dotnet watch run ./src/ShopApi

```

### 3. Run Requests

The [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) VSCode extension is required to run requests.

- Root:
  - See [`./requests/root.http`](./requests/root.http)

- Customers:
  - Upsert - See [`./requests/customers-upsert.http`](./requests/customers-upsert.http)

---
