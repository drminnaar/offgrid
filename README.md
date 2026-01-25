![](./docs/images/offgrid-cover.png)

# Offgrid

This project provides demonstration code for an online adventure store built using .NET 10, Nextjs, and React. The intention of this project is to be used as a sample project for learning and demonstration purposes. It is based on a fictitous e-commerce business called **_Offgrid_** that sells adventure goods via it's e-commerce website.

---

## ☰ Overview

**_Offgrid_** is an e-commerce business. It operates as an online retail destination for adventure enthusiasts, offering a curated selection of adventure gear for exploration and outdoor pursuits. It specializes in biking, winter sports, and water sports equipment.

This project will illustrate how to build 2 primary systems in order to fulfill Offgrid business requirements. The 2 primary systems are as follows:

1. <u>Shopping Website</u>
   
   A Nextjs web application frontend with a .NET 10 API backend.

2. <u>Staff Portal</u>
   
   A React web application frontend with a .NET 10 API backend.

This repository follows the [Monorepo] strategy, and is comprised of design, documentation, application code, and infrastructure code. It showcases a variety of practices for integrating and orchestrating various components of a modern software ecosystem. The objective is to provide a practical, hands-on example that can be used to explore a multitude of concepts, including (and not limited to) the following:

- Provide an organizational design
- Implement specific standard, patterns and practices used across a variety of areas such as _Frontend_, _Backend_, _Devops_, _Docker_, _Git_
- Design and build REST based API's using the latest version of [C# 14] and [.NET 10] Framework
- Design and build a customer facing e-commerce website using [Next.js]
- Design and build an internal administration application using [React]
- Provision infrastructure (platform and applications) into containers using [Docker Desktop] and [Docker Compose]
- Define devops design using [GitHub Actions] to setup CI/CD workflows

---

## 🏦 Organizational Design

As mentioned above, this project is intended as a demonstration project to illustrate many different ideas and concepts and evolve it over time. Therefore, for interest sakes, an organizational design has also been provided for _Offgrid_. The implementation of the various applications and services will take influence from the design but not follow it strictly.

The design documentation is available as follows:

- [Offgrid Organizational Design](./docs/design/org-design.md)
- [Offgrid Strategic Design](./docs/design/strategic-design.md)

An accompanying guide on Domain Driven Design (DDD) is also provided as part of design documentation:

- [Domain Driven Design Guide](./docs/design/domain-driven-design-guide.md)

---

## 🧱 Project Structure

The project structure will evolve over time. However, this section provides an example of how the project will be generally structured.

```text

offgrid
├── apps                                  # Web applications
│   ├── shop                              # Customer facing shopping e-commerce website (Next.js)
│   │   └── Dockerfile                    # Docker container image for shop app
│   └── portal                            # Staff portal website (React) to manage backoffice
│       └── Dockerfile                    # Docker container image for portal app
│
├── apis                                  # .NET domain APIs
│   ├── shared                            # .NET shared libraries used by API's
│   └── shop-api
│   │   ├── compose.yaml                  # Shop API compose file
│   │   └── Dockerfile                    # Docker container image for API
│   ├── portal-api
│   │   ├── compose.yaml                  # Shop API compose file
│   │   └── Dockerfile                    # Docker container image for API
│   ├── compose.yaml                      # Docker Compose file to manage API's (using include directive for multi-compose-file support)
│   └── ...                               # other API's
│
├── infra
│   └── local                             # Local development infrastructure
│       ├── compose.yaml                  # Main compose file (using include directive for multi-compose-file support)
│       │
│       ├── postgres/                     # Postgres docker config
│       │   └── compose.postgres.yaml     # Custom Postgres compose file
│       │
│       ├── mongo                         # Mongo docker config
│       │   └── compose.mongo.yaml        # Custom Mongo compose file
│       │
│       ├── keycloak                      # Keycloak docker config
│       │   └── compose.keycloak.yaml     # Custom Keycloak compose file
│       │
│       ├── typesense                     # Typesense docker config
│       │   └── compose.typesense.yaml    # Custom Typesense compose file
│       │
│       ├── rabbitmq                      # RabbitMQ docker config
│       │   └── compose.rabbitmq.yaml     # Custom RabbitMQ compose file
│       │
│       └── .env.example                  # Example environment variables for local services
│
├── docs
│   ├── decision-register                 # A simple wiki to capture key project decisions
│   ├── designs                           # Collection of design diagrams
│   ├── git                               # Collection of git standards and practices for this repo
│   └── org                               # Organizational description and design
│
├── .github                               # Workflows, etc.
├── .gitignore
└── README.md

```

---

## 🤖 Technology Stack

![](./docs/images/tech-stack.png)

<br />

Below is a table that summarises the technology stack that has been chosen to implement and manage the various applications and API's.

<br />


| Category                 | Technology       | Badge                                                                                                                                                                      | Description                                                                               |
| ------------------------ | ---------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- |
| **Languages**            | C#               | [![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=flat-square&logo=c-sharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)                        | Primary backend language for robust, type-safe enterprise applications                    |
|                          | TypeScript       | [![TypeScript](https://img.shields.io/badge/typescript-%23007ACC.svg?style=flat-square&logo=typescript&logoColor=white)](https://www.typescriptlang.org/)                  | Typed superset of JavaScript used for frontend, Node.js backend, and tooling              |
|                          | Bash             | [![Bash](https://img.shields.io/badge/Bash-4EAA25?style=flat-square&logo=gnu-bash&logoColor=white)](https://www.gnu.org/software/bash/)                                    | Shell scripting for automation, CI/CD scripts, and local dev workflows                    |
| **Backend & Frameworks** | .NET 10          | [![.NET](https://img.shields.io/badge/.NET_10-5C2D91?style=flat-square&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)                                          | Modern cross-platform framework for high-performance APIs, services & microservices       |
|                          | Node.js          | [![Node.js](https://img.shields.io/badge/node.js-6DA55F?style=flat-square&logo=node.js&logoColor=white)](https://nodejs.org/)                                              | JavaScript runtime for server-side logic, real-time features & lightweight APIs           |
|                          | Next.js          | [![Next.js](https://img.shields.io/badge/Next-black?style=flat-square&logo=next.js&logoColor=white)](https://nextjs.org/)                                                  | React framework with SSR, SSG, API routes & full-stack capabilities                       |
|                          | React            | [![React](https://img.shields.io/badge/React-20232A?style=flat-square&logo=react&logoColor=61DAFB)](https://react.dev/)                                                    | Component-based UI library powering interactive frontends                                 |
| **Identity & Auth**      | Keycloak         | [![Keycloak](https://img.shields.io/badge/Keycloak-4d1c47?style=flat-square&logo=keycloak&logoColor=white)](https://www.keycloak.org/)                                     | Open-source identity & access management (OIDC/OAuth2/SAML, SSO, user federation)         |
| **Databases & Search**   | PostgreSQL       | [![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=flat-square&logo=postgresql&logoColor=white)](https://www.postgresql.org/)                             | Powerful, standards-compliant relational SQL database with strong ACID guarantees         |
|                          | MongoDB          | [![MongoDB](https://img.shields.io/badge/MongoDB-%2347A248.svg?style=flat-square&logo=mongodb&logoColor=white)](https://www.mongodb.com/)                                  | Flexible document-oriented NoSQL database for schemaless or semi-structured data          |
|                          | Typesense        | [![Typesense](https://img.shields.io/badge/Typesense-F50A4C?style=flat-square&logoColor=white)](https://typesense.org/)                                                    | Fast, typo-tolerant, open-source search engine (Algolia alternative)                      |
| **Messaging**            | RabbitMQ         | [![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?style=flat-square&logo=rabbitmq&logoColor=white)](https://www.rabbitmq.com/)                                     | Reliable message broker for queues, pub/sub, task distribution & microservices decoupling |
| **Migrations**           | Flyway (Red Hat) | [![Flyway](https://img.shields.io/badge/Flyway-CC2233?style=flat-square&logoColor=white)](https://flywaydb.org/)                                                           | Version-controlled SQL-based database migrations (schema evolution tool)                  |
| **DevOps & Infra**       | Docker / Compose | [![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=flat-square&logo=docker&logoColor=white)](https://www.docker.com/)                                      | Containerization & multi-container local orchestration for consistent environments        |
|                          | Terraform        | [![Terraform](https://img.shields.io/badge/terraform-%235835CC.svg?style=flat-square&logo=terraform&logoColor=white)](https://www.terraform.io/)                           | Infrastructure as Code for provisioning & managing cloud resources declaratively          |
|                          | GitHub Actions   | [![GitHub Actions](https://img.shields.io/badge/github%20actions-%232671E5.svg?style=flat-square&logo=githubactions&logoColor=white)](https://github.com/features/actions) | CI/CD pipelines, automation workflows & deployment orchestration directly in GitHub       |
| **Tools**                | Git              | [![Git](https://img.shields.io/badge/git-%23F05033.svg?style=flat-square&logo=git&logoColor=white)](https://git-scm.com/)                                                  | Distributed version control system for source code management                             |
|                          | VS Code          | [![VS Code](https://img.shields.io/badge/VS%20Code-007ACC?style=flat-square&logo=visual-studio-code&logoColor=white)](https://code.visualstudio.com/)                      | Lightweight, extensible code editor with excellent support for this entire stack          |

<br />

The following diagram provides a very high level overview of where various technology stack choices are used:

<br />

![](./docs/images/tech-stack-overview.png)

<br />

- Next.js and Typescript are used to build the public facing shopping website
- React and Typescript are used to build the internal facing staff portal that manages the backoffice
- C# 14 .NET 10 is used to build API's and Background Services (Workers/Producers/Consumers)
- RabbitMQ is used as a message bus
- Keycloak provide authentication and authorization to the web applications and API's
- MongoDB is used for the product catalog
- Typesense is used for searching products
- Postgresql is used for data requiring ACID (Atomicity, Consistency, Isolation, Durability) compliance
- Redgate Flyway is used to manage and version database migrations
- Docker and Docker Compose are used to manage and host infrastructure services, applications, and API's
- Bash script are used to provide utility scripts to help manage and automate tasks

---

## 📋 Prerequisites

The following software will be required to be installed on your device in order to open and run the applications and API's:

- Node.js 24
- .NET 10
- Git
- Windows Subsystem for Linux (WSL) to use shell scripts. Alternatively, if on Windows, Git Bash.
- Docker Desktop

<br />

📜 NOTE: Run the following script from your terminal to get a "Tool Installation Report". 

- [./scripts/prereq-check.sh](./scripts/prereq-check.sh)

The script checks against a list of required and optional tools to verify the installation status of each tool.

```text
➜ chmod +x ./prereq-check.sh
➜ ./prereq-check.sh

Checking installed tools...

=========================== Tool Installation Report ===========================    

 Required Tools:
  ✔️  node: Installed (Version: 24.12.0)
  ✔️  git: Installed (Version: 2.51.2.windows.1)
  ✔️  docker: Installed (Version: Docker version 29.1.3, build f52814d)
  ✔️  npm: Installed (Version: 11.6.4)
  ✔️  dotnet: Installed (Version: 10.0.102)


 Optional Tools:
  ✔️  az: Installed
  ✔️  terraform: Installed
  ✔️  aws: Installed (Version: 2.32.30)
  ✔️  vim: Installed (Version: 9.1)
  ✔️  jq: Installed (Version: jq-1.8.1)
  ✔️  gh: Installed (Version: 2.83.2)
  ✔️  yq: Installed (Version: yq (https://github.com/mikefarah/yq/) version v4.48.1)

================================================================================
```

---

## 🏛️ Standards

- [Git Commit Convention](./docs/standards/git/git-commit-convention.md)
  
  Specifies the standard convention for writing Git commit messages.

- [Git Setup Guide](./docs/standards/git/git-setup.md)
  
  Provides details on the approach and standards relating to git setup and use.

---

## 🏷️ Versioning

I use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/drminnaar/offgrid/tags).

---

## ✍🏼 Authors

* **Douglas Minnaar** - *Sole and primary maintainer* - [drminnaar](https://github.com/drminnaar)

---

[.NET 10]: https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/overview
[C# 14]: https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14
[.NET Introduction]: https://learn.microsoft.com/en-us/dotnet/core/introduction
[C# Documentation]: https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp
[C#]: https://learn.microsoft.com/en-us/dotnet/csharp
[.NET SDK (Software Development Kit)]: https://learn.microsoft.com/en-us/dotnet/core/sdk
[Next.js]: https://nextjs.org
[React]: https://react.dev/
[Docker Desktop]: https://www.docker.com/products/docker-desktop
[Docker]: https://www.docker.com
[Docker Compose]: https://docs.docker.com/compose
[GitHub Actions]: https://github.com/features/actions
[Monorepo]: https://grokipedia.com/page/Monorepo
[Node.js]: https://nodejs.org/en
[TypeScript]: https://www.typescriptlang.org/
[Bash]: https://grokipedia.com/page/Bash_(Unix_shell)
[RabbitMQ]: https://www.rabbitmq.com/
[PostgreSQL]: https://www.postgresql.org/
[Typesense]: https://typesense.org/
[MongoDB]: https://www.mongodb.com/
[Keycloak]: https://www.keycloak.org/
[GitHub Actions]: https://github.com/features/actions
[Redgate Flyway]: https://www.red-gate.com/products/flyway/