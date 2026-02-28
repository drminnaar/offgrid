---
title: V2.1.0
author: Douglas Minnaar
tags: ["v2.1.0"]
date: 2026-02-28 (YYYY-MM-DD)
---

# Offgrid v2.1.0 Release Notes

The focus of this release is to introduce product management.

## 🚀 Highlights

- **Portal App**: Product management pages, product details decomposition, improved filtering, and type consistency.
- **Portal API**: New endpoints for products and product variants.
- **Infrastructure**: Added MongoDB service, add MongoDb initialization, and build tasks for mongosh.
- **Shared Libraries**: MongoDB utilities, datetime conversion extensions, and enhanced query/filtering support.
- **Docs & Onboarding**: Updated onboarding docs for MongoDB and decision registry.

---

## ✨ Features

### Portal App

- Product management pages with advanced filtering.
- Decomposed product details into reusable subcomponents.
- Improved type consistency by replacing interfaces with types.

### Portal API

- Endpoint to retrieve product by product id.
- Endpoint to get product variants.
- Initial 'products' module.

### Infrastructure

- Added MongoDB service to local stack.
- Build task for running mongosh.
- Improved dictionary key casing in MongoDB seed data.

### Shared Libraries

- MongoDB utilities for .NET.
- Extension to convert datetime to Unix time (seconds).
- QueryOptions abstraction for filtering.
- Factory method for empty PagedList.

### Docs & Onboarding

- Updated onboarding documentation for MongoDB.
- Added decision registry documentation and summary.

---

## 🛠️ Improvements & Refactoring

- Decomposed product details into subcomponents for maintainability.
- Replaced interfaces with types for consistency in portal app.
- Refactored IMongoQuery to QueryOptions in shared libraries.
- Introduced protected IEventHandler property to RabbitMqConsumerClientBase.

---

## 🐞 Bug Fixes & Chores

- Fixed dictionary key casing in MongoDB seed data.
- Fixed v2.0.0 release notes typo.

---

## 📚 Documentation

- See [README.md](README.md) for project overview and getting started.
- See [docs/onboarding.md](docs/onboarding.md) for onboarding steps.
- See [docs/portal/design](docs/portal/design) for versioned designs:
  - See [docs/portal/design/version-1/README.md](/docs/portal/design/version-1/README.md)
  - See [docs/portal/design/version-2/README.md](/docs/portal/design/version-2/README.md)
  - See [docs/portal/design/version-2_1/README.md](/docs/portal/design/version-2_1/README.md)
- See [services/portal/README.md](services/portal/README.md) for portal API overview.
- See [apps/portal-app/README.md](apps/portal-app/README.md) for portal application overview.

---

## 🎉 Thank You

Thank you for exploring Offgrid v2.1.0! This release focused on expanding product management capabilities and improving infrastructure and developer experience.

For detailed information about any component, please refer to the respective README files and documentation linked throughout this release notes.

---
