---
title: V1.0.0 - Initial Stable Release
author: Douglas Minnaar
tags: ["v1.0.0"]
date: 2026-02-03 (YYYY-MM-DD)
---

# Offgrid v1.0.0 Release Notes

## 🎉 Initial Release

This is the initial release of **Offgrid**, a comprehensive demonstration e-commerce platform for an adventure gear retail business. This release establishes the foundational architecture, infrastructure, and application framework for building modern, scalable e-commerce systems.

---

## 📋 Overview

Offgrid is an e-commerce monorepo demonstrating various practices for building applications using .NET 10, Next.js, and React. The platform includes a customer-facing shopping website and infrastructure for future staff portal development.

**What is Offgrid?**

Offgrid is a fictitious e-commerce business operating as an online retail destination for adventure enthusiasts, offering a curated selection of adventure gear for exploration and outdoor pursuits. It specializes in biking, winter sports, and water sports equipment.

---

## 🏗️ Architecture & Design

### Organizational Design

- **Domain-Driven Design (DDD)** approach with comprehensive documentation:
  - [Domain-Driven Design Guide](docs/design/domain-driven-design-guide.md)
  - [Organizational Design](docs/design/org-design.md)
  - [Strategic Design](docs/design/strategic-design.md)

### Monorepo Structure

The project follows a well-organized monorepo strategy with clear separation of concerns:

```text
offgrid/
├── apps/                    # Primary applications
│   ├── shop/                # Customer-facing e-commerce website
│   │   ├── shop-app/        # Next.js web application
│   │   ├── shop-api/        # .NET 10 API
│   │   └── docs/            # Shop system design documentation
│   └── portal/              # Staff portal (framework ready)
│
├── libs/                    # Shared libraries
│   ├── dotnet/              # .NET shared libraries
│   └── typescript/          # TypeScript shared libraries
│
├── infra/                   # Infrastructure as code
│   └── local/               # Local development environment
│       ├── postgres/        # PostgreSQL configuration
│       ├── keycloak/        # Keycloak configuration
│       └── scripts/         # Infrastructure automation
│
├── docs/                    # Comprehensive documentation
│   ├── design/              # Design documentation
│   ├── standards/           # Standards and conventions
│   └── images/              # Documentation images
│
└── scripts/                 # Automation and utility scripts
```

---

## 🤖 Technology Stack

### Languages & Frameworks

- **C# 14** with **.NET 10** - High-performance backend APIs and services
- **TypeScript** - Type-safe frontend development
- **Next.js** - React framework with SSR/SSG for the shopping website
- **React** - Component-based UI library (framework established for staff portal)
- **Node.js** - JavaScript runtime for Next.js and tooling
- **Bash** - Shell scripting for automation and DevOps workflows

### Infrastructure & Services

- **PostgreSQL** - Primary relational database with ACID compliance
- **Keycloak** - Identity and access management (OIDC/OAuth2/SAML support)
- **Flyway (Red Hat)** - Database migration and version control

### DevOps & Tooling

- **Docker** & **Docker Compose** - Containerization and local orchestration
- **Git** - Version control with established conventions
- **VS Code** - Recommended IDE with full stack support

---

## 🚀 Features & Components

### Infrastructure Services

#### Docker Compose Orchestration

- Multi-service orchestration using Docker Compose
- Modular compose file structure with include directives
- Automated service lifecycle management

#### Database Services

- **PostgreSQL** - Production-ready relational database
- **Flyway** - Automated database migrations

#### Identity & Access Management

- **Keycloak** - Full-featured identity provider
  - Realm configuration for Offgrid
  - SSO, user federation, and role-based access control

#### Automation Scripts

- **`compose.sh`** - Docker Compose wrapper for service management
- **`psql.sh`** - PostgreSQL client access script
- **`flyway.sh`** - Database migration management

### Shop Application

#### Shop Web Application

- **Next.js** framework with TypeScript
- Responsive design
- Docker containerization support
- Environment-based configuration

#### Shop API

- **.NET 10** RESTful API
- C# 14 language features
- Clean architecture patterns
- Docker containerization support

#### Design Documentation

- Version 1 architecture and design
- API specifications
- Database schema design

### Development Experience

#### Prerequisite Validation

Comprehensive validation scripts to ensure proper development environment:

- **`prereq-check.sh`** - Master script running all prerequisite checks
- **`tool-installation-check.sh`** - Verifies installation of required tools:
  - Node.js 24
  - .NET 10
  - Git
  - Docker Desktop
  - npm
  - Optional tools (Azure CLI, Terraform, AWS CLI, etc.)
- **`env-file-check.sh`** - Validates environment configuration files
- **`host-file-entry-check.sh`** - Checks required host file entries

#### Service Management Scripts

- **`run-infra-services.sh`** - One-command infrastructure startup
  - Starts PostgreSQL, Keycloak, and all infrastructure services
  - Runs Flyway migrations automatically
- **`run-shop-services.sh`** - One-command shop services startup
  - Starts shop-app (Next.js) and shop-api (.NET) in Docker

#### Standards & Conventions

**Git Standards**

- **Commit Convention** - Structured commit message format
  - Format: `<type>(<scope>): <subject>`
  - Types: feat, fix, docs, style, refactor, test, chore, etc.
  - Detailed guidelines for writing meaningful commits
- **Git Setup Guide** - Repository configuration and best practices
  - Branch naming conventions
  - Pull request workflows
  - Code review guidelines

**Documentation Standards**

- Comprehensive README files at every level
- Architecture decision records
- Design documentation with diagrams
- Getting started guides

---

## 📦 What's Included

### ✅ Delivered in v1.0.0

**Applications**

- ✅ Shop web application (Next.js + TypeScript)
- ✅ Shop API (.NET 10 + C# 14)
- ✅ Docker containerization for both services

**Infrastructure**

- ✅ PostgreSQL database with Docker configuration
- ✅ Keycloak identity provider with realm setup
- ✅ Flyway database migration framework
- ✅ Docker Compose multi-service orchestration
- ✅ Automated infrastructure management scripts

**Development Tools**

- ✅ Comprehensive prerequisite validation scripts
- ✅ One-command service startup scripts
- ✅ Database access and migration utilities
- ✅ Environment configuration templates

**Documentation**

- ✅ Main project README with getting started guide
- ✅ Domain-Driven Design comprehensive guide
- ✅ Organizational and strategic design documents
- ✅ Git standards and commit conventions
- ✅ Infrastructure setup and configuration guide
- ✅ Application-specific README files
- ✅ Technology stack overview with badges

**Design Assets**

- ✅ Cover image and branding
- ✅ Technology stack diagrams
- ✅ Architecture overview diagrams

---

## 🎯 Getting Started

### Prerequisites

**Required Tools**

- Node.js 24
- .NET 10
- Git
- Docker Desktop
- npm (comes with Node.js)
- WSL or Git Bash (Windows users)

**Optional Tools**

- Azure CLI (for Azure deployments)
- Terraform (for IaC)
- AWS CLI (for AWS deployments)
- jq (JSON parsing in scripts)
- yq (YAML parsing in scripts)

### Quick Start

#### 1. Verify Prerequisites

```bash
# Run comprehensive prerequisite check
./scripts/prereq-check.sh

# Or run individual checks:
./scripts/tool-installation-check.sh
./scripts/env-file-check.sh
./scripts/host-file-entry-check.sh
```

#### 2. Start Infrastructure Services

```bash
# One-command infrastructure startup
./scripts/run-infra-services.sh

# Or manually:
./infra/local/scripts/compose.sh up
./infra/local/scripts/flyway.sh migrate
```

#### 3. Start Shop Services (Optional - Docker)

```bash
# Run shop services in Docker
./scripts/run-shop-services.sh

# Or run outside Docker (recommended for development):
# - See apps/shop/shop-app/README.md
# - See apps/shop/shop-api/README.md
```

### Access Points

Once services are running, access them at:

- **Shop Website**: http://localhost:3000
- **Shop API**: http://localhost:7000
- **Keycloak Admin Console**: http://localhost:8080

### Database Access

```bash
# Connect to PostgreSQL
./infra/local/scripts/psql.sh

# Check Flyway migration status
./infra/local/scripts/flyway.sh info

# Run migrations
./infra/local/scripts/flyway.sh migrate
```

---

## 📚 Documentation

### Main Documentation

- [**Project README**](README.md) - Project overview, getting started, and quick reference
- [**Infrastructure Guide**](infra/local/README.md) - Local development infrastructure setup

### Application Documentation

- [**Shop App README**](apps/shop/shop-app/README.md) - Next.js application details
- [**Shop API README**](apps/shop/shop-api/README.md) - .NET API documentation
- [**Shop Design v1**](apps/shop/docs/design/version-1/README.md) - Architecture and design

### Design & Architecture

- [**Domain-Driven Design Guide**](docs/design/domain-driven-design-guide.md) - DDD principles and patterns
- [**Organizational Design**](docs/design/org-design.md) - Business context and structure
- [**Strategic Design**](docs/design/strategic-design.md) - Strategic DDD patterns

### Standards & Conventions

- [**Git Commit Convention**](docs/standards/git/git-commit-convention.md) - Structured commit messages
- [**Git Setup Guide**](docs/standards/git/git-setup.md) - Repository configuration and workflows

---

## 🔧 Configuration

### Environment Files

All services use environment-based configuration:

- `.env` files for service-specific configuration
- Docker Compose environment variable injection
- Validation via `env-file-check.sh` script

### Docker Compose

- Modular compose file architecture
- Service-specific compose files with include directives
- Health checks and dependency management
- Volume mounts for local development

### Host Configuration

Required host file entries (verified by `host-file-entry-check.sh`):

- Local service DNS resolution
- Keycloak realm configuration

---

## 💡 Key Capabilities

### For Learning & Demonstration

This project showcases:

- **Modern Architecture** - Modular Monolith (Modulith), DDD, clean architecture patterns
- **Full-Stack Development** - .NET backend + Next.js/React frontend
- **DevOps Practices** - Infrastructure as Code, containerization, automation
- **Security** - Identity management, OAuth2/OIDC integration
- **Database Management** - ACID transactions, migrations, version control
- **Development Workflow** - Git conventions, documentation standards, tooling

### Production-Ready Patterns

- Automated prerequisite validation
- One-command environment setup
- Health monitoring and checks
- Configuration management
- Service orchestration
- Database migration strategies

---

## 🔍 Project Highlights

### Automation First

- Comprehensive validation scripts ensure consistent developer experience
- One-command startup for infrastructure and applications
- Automated database migrations
- Tool installation verification

### Documentation Excellence

- README files at every project level
- Comprehensive getting started guides
- Architectural decision records
- Visual diagrams and technology overviews

### Developer Experience

- Clear project structure with logical organization
- Consistent naming conventions
- Helpful utility scripts
- Extensive inline documentation

### Standards & Best Practices

- Git commit conventions for clear history
- Semantic versioning (SemVer)
- Code organization following DDD principles
- Separation of concerns across layers

---

## 📝 Important Notes

This project demonstrates modern software engineering practices and serves as a comprehensive learning platform for:

- **Microservices Architecture** - Service decomposition and orchestration
- **Domain-Driven Design** - Strategic and tactical DDD patterns
- **Cloud-Native Development** - Containerization and infrastructure as code
- **DevOps Automation** - CI/CD pipelines and deployment strategies
- **Full-Stack Development** - Modern .NET and React ecosystem
- **Identity & Security** - OAuth2, OIDC, and access management
- **Database Engineering** - Migration strategies and ACID compliance

### Project Status

This is a **demonstration and learning project** intended for:

- Educational purposes
- Reference implementation
- Exploring modern development practices

### Not Production Deployment

While this project follows production-ready patterns and practices, it is **not** configured for production deployment. It demonstrates concepts and patterns that could be used in production systems.

### Evolutionary Design

The project is designed to evolve over time, with planned additions including:

- Additional microservices
- Advanced search capabilities
- Message-driven architecture
- Cloud deployment configurations
- Comprehensive test coverage

---

## 🔜 Next Release

- Admin Portal application (React)
  - Manage Customers
  - Manage Product Catalog
- Portal API (.NET 10)
  - Product Management

---

## 👷 Contributors

**Douglas Minnaar**

- GitHub: [@drminnaar](https://github.com/drminnaar)
- Role: Sole and primary maintainer

---

## 🔗 Links & Resources

- **Repository**: [github.com/drminnaar/offgrid](https://github.com/drminnaar/offgrid)
- **Documentation**: [See docs/ directory](docs/)
- **Issues**: [GitHub Issues](https://github.com/drminnaar/offgrid/issues)

---

## 🎉 Thank You

Thank you for exploring Offgrid v1.0.0! This release represents the foundation of an e-commerce platform demonstration project.

For detailed information about any component, please refer to the respective README files and documentation linked throughout this release notes.

---
