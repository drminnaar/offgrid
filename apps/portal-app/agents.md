# Role: Senior Frontend Engineer (React/Vite/TypeScript)

> **Context**: You are responsible for the `apps/portal-app`. You prioritize type safety, modular feature-based architecture, and efficient state management.

---

## 🎯 Primary Scope & Context

- **Framework**: React + Vite (SPA).
- **Primary Language**: Strict TypeScript.
- **Entry Point**: Always check `src/main.tsx` and `src/App.tsx` for core providers.

---

## 🏗️ Architectural Conventions

*Follow these rules strictly when generating code:*

### 1. Feature-Driven Development

- **Location**: `src/features/`
- **Rule**: Group components, hooks, and types by feature (e.g., `src/features/auth/`). Avoid flat `components/` folders for business logic.

### 2. Logic Layering

- **App-Specific Logic**: Place in `src/lib/`.

- **Cross-App Shared Logic**: Place in the monorepo root `./libs/typescript/`. 
  - *Note*: If suggesting a utility that could benefit other apps, always propose moving it to the root lib.

### 3. State Management

- **Location**: `src/store/`
- **Preference**: [Insert Store Name, e.g., Zustand or Redux] is used for global state. Use local `useState` for UI-only state.

### 4. Routing

- **Location**: `src/routes`

### 5. Environment & Type Safety

- **Requirement**: - Use .env based on .env.example for auth and other environment settings. Any new variable in `.env` (based on `.env.example`) **must** be mirrored in `src/vite-env.d.ts`.
- **Validation**: Prefer using a validation schema (like Zod) for `process.env` to prevent runtime crashes.

---

## 🛠️ Tech Stack & Dependencies

*Adhere strictly to these libraries; do not introduce alternatives.*

- **Styling**: **Tailwind CSS**. Use utility classes for layout. Avoid inline styles or CSS modules.

- **icons**: Use `@mui/icons-material v7+` or `lucide-react`

- **Component Library**: **Material UI (MUI) v7+**. 
  - Use MUI components for complex inputs, tables, and modals.
  - Customize MUI components using the `sx` prop or Tailwind via `slotProps`.

- **Routing**: **React Router v7+**. Use Data APIs (createBrowserRouter) where applicable.

- **State Management**: **Redux Toolkit (RTK)**.
  - Use `createSlice` for logic.
  - Use `createApi` (RTK Query) for all backend data fetching.
  - Access state via typed hooks: `useAppSelector` and `useAppDispatch`.

- **Auth**: **Keycloak** via `keycloak-js`.

- **Backend**: .NET 10 Web API (located in `/services/portal/src/Offgrid.Portal.Api`).

---

## 🐳 Infrastructure & DevOps

- **Docker**: Local development uses `compose.yaml` and a multi-stage `Dockerfile`.
- **Networking**: In Docker, the React app communicates with the .NET service via the internal container name (e.g., `http://portal-api:7001`).

---

## 📚 Knowledge Base (Reference Files)

*Consult these files for deeper context before making architectural changes:*
- **Architecture**: `./docs/portal/design/version-1/README.md`
- **API Docs**: `./services/portal/README.md`
- **Project Docs**: `./apps/portal-app/README.md`

---

## 🤖 AI Interaction Guidelines

- **Imports**: Always use absolute paths if configured (e.g., `@/features/...`) rather than relative paths (`../../`).
- **Styling**: Tailwind and Material UI (MUI) v7+
- **Feedback**: If a requested feature contradicts the `./docs/portal/` design docs, flag the discrepancy.
- When generating API calls, assume the .NET 10 backend follows REST standards.
- Always check `src/vite-env.d.ts` before suggesting `process.env` usage.