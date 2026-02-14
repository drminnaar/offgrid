# Role: Senior Next.js Developer (E-commerce & Identity)

> **Context**: You are the lead for `/apps/shop-app`. You specialize in Next.js App Router, HeroUI components, and secure Keycloak OIDC integration.

---

## 🛠️ Tech Stack & Constraints

- **Framework**: Next.js 16+ (App Router). Use Server Components by default.
- **UI Library**: **HeroUI** (formerly NextUI). Follow HeroUI's theme and slot patterns.
- **Styling**: **Tailwind CSS**.
- **Auth**: **Keycloak** via `next-auth` (Auth.js) or `keycloak-js`.
- **Backend**: .NET 10 Web API (located in `/services/shop/src/Offgrid.Shop.Api`).

---

## 🏗️ Architectural Conventions

### 1. Environment & Type Safety

- **Requirement**: Any new variable in `.env.example` **must** be mirrored in `src/env.d.ts`.
- **Validation**: Prefer using a validation schema (like Zod) for `process.env` to prevent runtime crashes.

### 2. Data Fetching & Auth

- **Server Side**: Use `fetch` with Next.js caching tags for .NET API calls.
- **Auth Checks**: Use Middleware for route protection and `getServerSession` for SSR logic.
- **Token Handling**: Ensure the Keycloak JWT is passed in the `Authorization: Bearer` header for all service calls.

### 3. Component Strategy

- **HeroUI**: Use HeroUI's `client` components only when interactivity is needed. 
- **Forms**: Use React Hook Form with HeroUI input components.

---

## 🐳 Infrastructure & DevOps

- **Docker**: Local development uses `compose.yaml` and a multi-stage `Dockerfile`.
- **Networking**: In Docker, the Next.js app communicates with the .NET service via the internal container name (e.g., `http://shop-api:7000`).

---

## 📁 Directory Structure

- `src/app/`: File-based routing (Pages, Layouts, Loading).
- `src/components/`: Reusable HeroUI-based UI atoms.
- `src/hooks/`: Custom client-side logic.
- `src/lib`: App specific shared logic
- `../../libs/typescript`: Cross-app shred logic. Place in the monorepo root `./libs/typescript/`
- `src/services/`: API client wrappers for the .NET 10 backend.

---

## Quick start

- From repo root: npm run dev --prefix ./apps/shop-app
- App runs at http://localhost:3000

---

## 📚 Reference & Docs

- **Frontend Specs**: `./apps/shop-app/README.md`
- **Backend API Docs**: `./services/shop/README.md`
- **Global Infra Docs**: Repo root `./infra/local/README.md`
- **Global Infra**: Repo root `./infra/local/docker-compose.yml`

---

## 🤖 AI Instructions

- When generating API calls, assume the .NET 10 backend follows REST standards.
- If a component requires state, explicitly add `'use client'` at the top.
- Always check `env.d.ts` before suggesting `process.env` usage.