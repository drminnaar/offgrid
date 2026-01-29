# Offgrid Shop API - Version 1

---

## 🗺️ Overview

As a prospective customer (not yet signed up), you will be able to:

- open the web application with a default landing page
- sign-up

As a customer (sign-up completed), you will be able to:

- open the web application with a default landing page
- sign-in, and sign-out
- view customer details for current account

When a prospective customer signs up, an account is created within Identity Provider (Keycloak), and a profile is created via a backend API in the database. The profile in the database is kept in sync with details from Identity Provider.

![Usecase Diagram](usecase.png)

---

## 📐 High Level Design

![HLSD](hlsd.png)

---

## 🛒 Shop Website

The primary areas of focus in this version are as follows:

- Create initial Next.js application and establish baseline (minimal required setup)
- Introduce User Interface (UI) libraries to provide styling
- Create navigation bar
- Introduce auth capability like sign-up, sign-in, and sign-out
- Integrate with Shop API to create/update customer details
- Create Dockerfile to define Shop web application image

---

## { ... } Shop API

The Shop API follows a [modular monolith](https://grok.com/share/c2hhcmQtMw_f1c77275-98eb-4a84-8056-1909c5036e4c) architectural style.

The primary areas of focus in this version are as follows:

- Create initial .NET 10 API and establish baseline (minimal requried setup)
- Once a baseline is established, introduce the following capabilities to the API
  - custom logging setup
  - error handling
  - validation
- Create customers endpoint to allow Shop application to create/update customer details
- Introduce auth capability to ensure that relevant endpoint requests are authorised
- Create Dockerfile to define Shop API image

---

## 🏗️ Infrastructure

The primary areas of focus in this version are as follows:

- Define initial Docker compose file to manage required infrastructure services
- Define initial Docker compose file to manage API and Web Application services
- Define Keycloak service
- Define Postgresql service
- Define Redgate Flyway service
- Define Redgate Flyway migrations

---
