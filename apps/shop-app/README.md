# Offgrid - Shop App

---

## 🛠️ Initial Installation

This is a [Next.js](https://nextjs.org) project bootstrapped with [`create-next-app`](https://nextjs.org/docs/app/api-reference/cli/create-next-app).

The initial project was created as follows:

```bash

npx create-next-app@latest .

Need to install the following packages:
create-next-app@16.1.6
Ok to proceed? (y) y

√ Would you like to use the recommended Next.js defaults? » No, customize settings
√ Would you like to use TypeScript? ... No / Yes
√ Which linter would you like to use? » ESLint
√ Would you like to use React Compiler? ... No / Yes
√ Would you like to use Tailwind CSS? ... No / Yes
√ Would you like your code inside a `src/` directory? ... No / Yes
√ Would you like to use App Router? (recommended) ... No / Yes
√ Would you like to customize the import alias (`@/*` by default)? ... No / Yes
Creating a new Next.js app in C:\Users\drmin\code\offgrid\shop\shop-app.

Using npm.

Initializing project with template: app-tw


Installing dependencies:
- next
- react
- react-dom

Installing devDependencies:
- @tailwindcss/postcss
- @types/node
- @types/react
- @types/react-dom
- babel-plugin-react-compiler
- eslint
- eslint-config-next
- tailwindcss
- typescript

```

---

## 📦 Packages

This section highlights important packages that have been used to build the app.

### HeroUI

HeroUI is a modern React UI component library built on top of Tailwind CSS and Framer Motion. It provides a collection of pre-built, accessible, and customizable UI components for building user interfaces.

Key Features:

- Component Library: Ready-to-use components like buttons, modals, dropdowns, cards, etc.
- Tailwind CSS Integration: Built with Tailwind CSS for styling and customization
- Framer Motion: Smooth animations and transitions
- Accessibility: Components are designed with accessibility in mind
- Dark Mode Support: Built-in theme switching capabilities
- TypeScript Support: Full TypeScript support for type safety

Use [Global Installation](https://www.heroui.com/docs/guide/installation#global-installation)

```bash
npm install @heroui/react framer-motion
```

### Next Themes

Next Themes is a lightweight abstraction library for managing themes in Next.js applications. It simplifies theme switching and persistence in your React app.

Key Features:

- Theme Switching: Easily switch between themes (e.g., light/dark mode)
- Persistence: Automatically saves the user's theme preference to localStorage
- System Preference Detection: Can detect and use the system's theme preference
- No Flash: Prevents theme flash on page load
- SSR Support: Works seamlessly with Next.js server-side rendering
- Multiple Themes: Support for multiple custom themes beyond just light/dark

For more details, visit the [Next Themes Documentation](https://github.com/pacocoursey/next-themes#readme)

```bash
npm install next-themes
```

### Lucide React

Lucide React is an icon library that provides a collection of clean, consistent, and customizable SVG icons for React applications.

Key Features:

- Icon Library: Over 1000+ open-source icons
- Consistent Design: All icons follow a unified design system
- Customizable: Easy to customize size, color, stroke width, and other properties
- Lightweight: Minimal performance impact, tree-shakeable
- TypeScript Support: Full TypeScript support included
- Easy Integration: Simple to use in React components

For more details, visit the [Lucide React Documentation](https://lucide.dev/)

```bash
npm install lucide-react
```

---

## 🚀 Getting Started

First, run the development server:

```bash

npm run dev

```

Open [http://localhost:3000](http://localhost:3000) with your browser to see the result.

---

## 🤖 Agents Guidance

- Local guide: [./agents.md](./agents.md)

---

## 🗝️ Keycloak Auth Setup

- Step 1 - Install packages
  
  ```bash
  npm install next-auth@beta keycloak-js
  ```

- Step 2 - Configure Authjs
  
  Follow the installation instructions [here](https://authjs.dev/getting-started/installation)

  Add `.env` file to the root of the app. Add the following contents to `.env`:

  ```yaml
  AUTH_KEYCLOAK_ID="shop-app"
  AUTH_KEYCLOAK_ISSUER="http://localhost:8080/realms/offgrid-public"
  AUTH_KEYCLOAK_SECRET=""
  AUTH_URL="http://localhost:3000"
  AUTH_SECRET=""
  ```

  <br />

  > [!NOTE]
  > &nbsp;  
  > See [.env.example](./.env.example) file for an example of the properties required for `.env`
  >

  <br />

  > [!IMPORTANT]
  > Fromt the root of the app (folder containing package.json), run the following command to obtain the `AUTH_SECRET`:  
  >  
  > ```bash
  > npx auth secret
  > ```

  <br />

- Step 3 - See Typescript Module Augmentation
  
  Review [Module Augmentation](https://authjs.dev/getting-started/typescript#module-augmentation)

  See [auth.ts](./auth.ts).

- Step 4 - Configure Keycloak provider
  
  See [Authjs Keycloak Provider](https://authjs.dev/getting-started/providers/keycloak)

- Step 5 - Add Session provider
  
  See [./components/providers/app-provider.tsx](./components/providers/app-provider.tsx).

  ```tsx
  <SessionProvider>{children}</SessionProvider>
  ```

- Step 6 - Create a client in keycloak
  
  A keycloak realm and client has already been setup as part of keycloak infrastructure setup. See [./infra/local/keycloak/realms/offgrid-public-realm.json](../../../infra/local/keycloak/realms/offgrid-public-realm.json).

### Route protection

The app uses proxy-based route protection for private pages.

- Protected routes: `/profile`, `/cart`, `/checkout`
- Public routes: `/`, `/products`, and product search/filter URLs

Unauthenticated requests to protected routes are redirected to Auth.js sign-in and then returned to the original URL via `callbackUrl`.
  

---

## 🐳 Docker Setup

### App Settings

When the app is packaged into a Docker container, it will use an environment file called `.env.docker`. This file needs to be created and can be based on the [`.env.docker.example`](./.env.docker.example). The environment file is specified as part of the [`compose.yaml`](./compose.yaml) file as follows:

```yaml
env_file:
  - .env.docker
```

This will ensure that the requried environment settings are used when initialising Docker container. Note that Docker Compose service names are used instead of localhost. Inside a container, localhost refers to the container itself, not your host machine or other containers. Each service runs on its own network namespace, so containers must reach each other via the Compose network DNS name (the service name), not localhost.

> [!IMPORTANT]
> Add a `keycloak` entry to your hosts file so the browser can resolve `http://keycloak:8080` during login/redirect callbacks when running locally. This maps the hostname to your local machine.
> - Windows: Edit `C:\Windows\System32\drivers\etc\hosts` as Administrator and add:
> `127.0.0.1 keycloak`
> - Linux: Edit `/etc/hosts` as root and add:
> `127.0.0.1 keycloak`

### Manage Shop Containers

The `shop-app` defines its own [`Dockerfile`](./Dockerfile) and [`compose.yaml`](./compose.yaml) file.

The containers are managed using `docker compose` as follows:

```bash
# location: ./apps/shop/infra

# start containers
docker compose up --build

# stop containers
docker compose down --volumes --remove-orphans

# recreate shop-api container
docker compose up --build -d --force-recreate shop-api

# recreate shop-app container
docker compose up --build -d --force-recreate shop-app

---

## 🎓 Learn More

To learn more about Next.js, take a look at the following resources:

- [Next.js Documentation](https://nextjs.org/docs) - learn about Next.js features and API.
- [Learn Next.js](https://nextjs.org/learn) - an interactive Next.js tutorial.

You can check out [the Next.js GitHub repository](https://github.com/vercel/next.js) - your feedback and contributions are welcome!

---
