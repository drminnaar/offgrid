# Offgrid Portal API - Version 1

---

## 🗺️ Overview

As a staff member, you will be able to:

- open the web application with a default landing page
- sign-in

As a Customer Team member, you will be able to:

- open the web application with a default landing page
- sign-in, and sign-out
- manage customer details
  - view list of customers
  - view customer details
  - suspend customer
  - reinstate customer

> [!IMPORTANT]
> &nbsp;  
> Staff user accounts are managed through the Keycloak Admin Portal.  
> &nbsp;  

<br />

![Usecase Diagram](usecase.png)

---

## 📐 High Level Design

![HLSD](hlsd.png)

---

## 🛒 Portal Website

The scope for this version is as follows:

- Create initial React application and establish baseline (minimal required setup)
- Introduce User Interface (UI) libraries to provide styling
- Introduce routing libraries to enable navigation
- Introduce state management
- Create navigation bar
- Introduce auth capability like sign-in, and sign-out
- Integrate with Portal API to manage customers
- Create Dockerfile to define Portal web application image

---

## { ... } Portal API

The Portal API follows a [modular monolith](https://grok.com/share/c2hhcmQtMw_f1c77275-98eb-4a84-8056-1909c5036e4c) architectural style.

The scope for this version is as follows:

- Create initial .NET 10 API and establish baseline (minimal requried setup)
- Once a baseline is established, introduce the following capabilities to the API
  - custom logging setup
  - error handling
  - validation
- Create customers endpoint to allow Portal application to manage customer details
  - list customers (with pagination and filters)
  - get customer detail by customer id
  - suspend customer
  - reinstate customer
- Introduce auth capability to ensure that relevant endpoint requests are authorised
- Create Dockerfile to define Portal API image

---

## 📬 Customer Outbox Processor

The outbox processor reliably publishes customer domain events to the message bus (RabbitMQ) using the outbox pattern. It runs as a background worker that polls the outbox table, converts pending messages into CloudEvents, publishes them to RabbitMQ, and updates outbox state for retries or permanent failure.

- Create .NET 10 Background service to process customer domain events from the customer outbox table
- Convert domain events into integration events ([CNCF CloudEvents](https://www.cncf.io/projects/cloudevents/))
- Publish integration events to RabbitMQ
- Implement basic retry and failure policies
- Create Dockerfile to define outbox image

---

## ✉️ Customer Event Processor

The event processor consumes customer CloudEvents from RabbitMQ and routes them to handlers. It runs as a set of hosted background services, one per event type, and uses queue-based consumers to process events reliably.

- Create .NET 10 background service to host background workers
- Create background worker for each integration event type
- Create Dockerfile to define processor image

---

## 🏗️ Infrastructure

The primary areas of focus in this version are as follows:

- Define initial Docker compose file to manage API and Web Application services
- Update Keycloak infrastructure with new realm for the Portal admin app
  - Define clients
  - Define roles
  - Define groups
  - Define sample users
- Define RabbitMQ Docker compose service

---
