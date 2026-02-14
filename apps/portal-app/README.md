# Offgrid Portal App

---

## ⚛ React + TypeScript + Vite

This project template is generated using [Vite](https://vite.dev/). See [Scaffolding Your First Vite Project](https://vite.dev/guide/#scaffolding-your-first-vite-project).

```bash

npm create vite@latest . -- --template react-ts

```

This template provides a minimal setup to get React working in Vite with HMR and some ESLint rules.

Currently, two official plugins are available:

- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react) uses [Babel](https://babeljs.io/) (or [oxc](https://oxc.rs) when used in [rolldown-vite](https://vite.dev/guide/rolldown)) for Fast Refresh
- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react-swc) uses [SWC](https://swc.rs/) for Fast Refresh

### React Compiler

The React Compiler is not enabled on this template because of its impact on dev & build performances. To add it, see [this documentation](https://react.dev/learn/react-compiler/installation).

### Expanding the ESLint configuration

If you are developing a production application, we recommend updating the configuration to enable type-aware lint rules:

```js
export default defineConfig([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Other configs...

      // Remove tseslint.configs.recommended and replace with this
      tseslint.configs.recommendedTypeChecked,
      // Alternatively, use this for stricter rules
      tseslint.configs.strictTypeChecked,
      // Optionally, add this for stylistic rules
      tseslint.configs.stylisticTypeChecked,

      // Other configs...
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // other options...
    },
  },
])
```

You can also install [eslint-plugin-react-x](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-x) and [eslint-plugin-react-dom](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-dom) for React-specific lint rules:

```js
// eslint.config.js
import reactX from 'eslint-plugin-react-x'
import reactDom from 'eslint-plugin-react-dom'

export default defineConfig([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Other configs...
      // Enable lint rules for React
      reactX.configs['recommended-typescript'],
      // Enable lint rules for React DOM
      reactDom.configs.recommended,
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // other options...
    },
  },
])
```

---

## 🚀 Getting Started

### Start App

```bash

npm run dev

```

Access app at [http://localhost:4000](http://localhost:4000)

---

## 🤖 Agents Guidance

- Local guide: [./agents.md](./agents.md)

---

## 📦 Packages

### Roboto Fonts

```bash

npm install @fontsource/roboto

```

See [Typesource - Roboto Install](https://fontsource.org/fonts/roboto/install).

### Material MUI (and Icons)

See:

- [Material UI - Getting Started](https://mui.com/material-ui/getting-started/)
- [Material UI - Install](https://mui.com/material-ui/getting-started/installation/)
- [Material UI - Install Icons](https://mui.com/material-ui/material-icons/)
- [Material UI - Install Datagrid](https://mui.com/x/react-data-grid/quickstart/)

<br />

```bash
# install mui framework
npm install @mui/material @emotion/react @emotion/styled

# install mui icons
npm install @mui/icons-material

# install mui data grid
npm install @mui/x-data-grid

```

### Lucide React

Lucide is built with ES Modules, so it's completely tree-shakable. Each icon can be imported as a React component, which renders an inline SVG element.

See [Lucide React](https://lucide.dev/guide/packages/lucide-react).

```bash

npm install lucide-react

```

### React Router

React Router is the standard, widely-used library for handling navigation and routing in React applications. It enables the creation of single-page applications (SPAs) with dynamic views and bookmarkable URLs, allowing navigation between different components without requiring a full page reload.

See [React Router](https://reactrouter.com/home) for more details.

This project installs React Router using _Data Mode_. See the [official installation docs](https://reactrouter.com/start/data/installation).

```bash

npm install react-router

```

### Keycloak

Keycloak JS is the official client-side JavaScript library (adapter) that enables web applications to use Keycloak for all aspects of identity and access management (IAM). It handles the complexities of authentication protocols like OpenID Connect and OAuth 2.0 under the hood, simplifying the integration of security features into your application.

See the following links for more information:

- [Official Keycloak Documentation](https://www.keycloak.org/securing-apps/javascript-adapter)
- [GitHub Repo](https://github.com/keycloak/keycloak-js)
- [NPM Package](https://www.npmjs.com/package/keycloak-js)

```bash

npm install keycloak-js

```

### React Redux & Redux Toolkit

React Redux is the official UI binding library that connects the standalone Redux state management library to React applications. It provides a predictable way to manage a complex application's global state in a single, centralized location called the store.

See the following links for more information:

- [React Redux Getting Started](https://react-redux.js.org/introduction/getting-started)
- [React Redux NPM Package](https://www.npmjs.com/package/react-redux)

```bash

npm install react-redux

```

Redux Toolkit (RTK) is the official, recommended, and opinionated toolset for efficient Redux development. It streamlines state management by reducing boilerplate code, simplifying store setup, and enabling "mutative" immutable updates. Key features include configureStore, createSlice, and built-in support for middleware like Redux Thunk.

See the following links for more information:

- [Redux Getting Started](https://redux.js.org/introduction/getting-started)
- [Redux NPM Package](https://www.npmjs.com/package/@reduxjs/toolkit)

```bash

npm install @reduxjs/toolkit

```

---
