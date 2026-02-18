---
title: V2.0.0
author: Douglas Minnaar
tags: ["v2.0.0"]
date: 2026-02-18 (YYYY-MM-DD)
---

# Offgrid v2.0.0 Release Notes

## 🚀 Highlights

- **Portal App**: Customer detail page, customer list with filters, pagination, and improved navigation.
- **Portal API**: Full customer management endpoints (list, detail, suspend, reinstate), outbox pattern, event processor, and robust validation.
- **Infrastructure**: Outbox/event processor, RabbitMQ integration, improved Flyway migrations, and enhanced local dev scripts.
- **Shared Libraries**: New DDD/domain abstractions, enum utilities, pagination, and messaging contracts.
- **Docs & Onboarding**: Comprehensive agent guidance, onboarding, and design documentation.

---

## ✨ Features

### Portal App

- Customer detail page and related UI components
- Customer list page: table, filters, pagination, drawer nav, API integration
- Keycloak authentication, Redux Toolkit state, theme toggle
- Routing, login, and not found page

### Portal API

- Paginated "Get All Customers" endpoint
- "Get Customer By Id" endpoint with validation
- Filter customers by status
- Customer suspend/reinstate endpoints and domain events
- Customer change tracking with Unit of Work
- Outbox pattern for reliable event publishing
- EventProcessor service with Spectre.Console visualization

### Infrastructure

- RabbitMQ service and integration
- Flyway migrations for customer, outbox, and change tracking tables
- Dev helper scripts for infra, psql, rabbitmqadmin

### Shared Libraries

- Enum extensions, pagination utilities, DDD/domain abstractions
- Messaging contracts, CNCF cloud event factory

### Docs & Onboarding

- Agent guidance, onboarding, and reorganized documentation
- Portal and shop design docs

---

## 🛠️ Improvements & Refactoring

- Refactored project into a simpler monorepo layout
- Improved JSON serialization, error handling, and validation
- Added concurrency handling for customer updates
- Enhanced scripts and tasks for local development

---

## 🐞 Bug Fixes & Chores

- Fixed formatting, naming, and improved code organization
- Updated scripts to match new directory structure
- Fixed broken documentation links

---

## 📚 Documentation

- See [README.md](README.md) for project overview and getting started.
- See [docs/onboarding.md](docs/onboarding.md) for onboarding steps.
- See [docs/portal/design/version-1/README.md](docs/portal/design/version-1/README.md) for portal API and event processor design.
- See [services/portal/README.md](services/portal/README.md) for portal API overview
- See [apps/portal-app/README.md](services/portal/README.md) for portal application overview

---

## 🎉 Thank You

Thank you for exploring Offgrid v2.0.0! This release focused on implementing the initial backoffice system called Portal. 

For detailed information about any component, please refer to the respective README files and documentation linked throughout this release notes.

---
