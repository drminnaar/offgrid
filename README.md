![](https://github.com/user-attachments/assets/5e3722c9-0cfb-4ecf-a2cf-e0a551465f70)

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

[.NET 10]: https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/overview
[C# 14]: https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14
[Next.js]: https://nextjs.org
[React]: https://react.dev/
[Docker Desktop]: https://www.docker.com/products/docker-desktop
[Docker Compose]: https://docs.docker.com/compose
[GitHub Actions]: https://github.com/features/actions
[Monorepo]: https://grokipedia.com/page/Monorepo