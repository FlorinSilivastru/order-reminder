![image](https://github.com/user-attachments/assets/e8d45ec7-2f57-4574-8463-82e601fc1162)


# 🛠️ Microservices Architecture Showcase

This repository contains microservices built to demonstrate advanced skills in building modern, production-ready .NET systems using clean architecture, message-based communication, and service observability.

---

## 📦 Overview

This solution simulates an **order tracking and product notification system**:

- 🧾 When an **order is created**, the system evaluates product quantity, usage history, and triggers **reorder notifications** if needed.
- 🔐 Includes a **User Service** for authentication (via cookie-based auth) and token issuance (via bearer tokens).
- ⚙️ All services follow **clean architecture** and are designed for scale and maintainability.

---

## 🧱 Architecture Highlights

Each microservice follows a strict layered structure:

- `Domain` — business logic and core models
- `Application` — CQRS handlers, interfaces, and use cases
- `Infrastructure` — data access, messaging, external integrations
- `Test` — this will contain the unit test cases
- `API` — thin HTTP layer with controllers or minimal API endpoints

✅ Implemented Patterns:
- 🧰 [MediatR](local package) for in-process messaging
- ✅ [FluentValidation](https://docs.fluentvalidation.net) for request validation
- ⚠️ Custom **Exception Handling Middleware** with logging
- 🔍 Correlation ID middleware to enable end-to-end traceability
- 🧾 API versioning to support future compatibility
- 🕵️ **Audit Middleware** to trace sensitive changes
- 🧩 **Correlation Middleware** for distributed traceability
- 📬 [MassTransit](https://masstransit.io/) + RabbitMQ for asynchronous service communication
- 📏 Code Metrics and Static Analysis using StyleCop Analyzers
---

## 🛠️ Tech Stack

| Area                 | Tech                     |
|----------------------|--------------------------|
| Language             | C# (.NET 9)              |
| Web Framework        | ASP.NET Core Minimal API |
| Messaging            | RabbitMQ + MassTransit   |
| Storage              | MongoDB (now), SQL (planned) |
| Gateway              | YARP Reverse Proxy       |
| Auth                 | Identity Provider (cookies + bearer) |
| Validation           | FluentValidation         |
| In-process Messaging | MediatR                  |
| API Docs             | Swagger / OpenAPI        |
| Hosting              | Docker Compose (MongoDB, RabbitMQ for now) |
| Code Quality         | StyleCop, SonarAnalyzer.CSharp, IDisposableAnalyzers, Code Metrics |

---

## 🚀 Features Implemented

- ✅ Clean architecture in services
- 🔗 Async communication via RabbitMQ & MassTransit
- 🔐 Secure gateway with mixed authentication modes
- ♻️ Health check endpoints for each service
- 🔄 Middleware pipeline (logging, correlation, auditing)
- 🧪 Swagger integrated for API testing
- 🧰 Readiness for testability and extensibility
- 📏 Static code analysis via StyleCop, SonarAnalyzer, IDisposableAnalyzers, and built-in Code Metrics
---

## 🛠️ Applying EF Core Migrations in Production
⚠️ Avoid using Database.Migrate() at application startup in production environments.
Doing so can lead to concurrency issues, race conditions, and inconsistent schema updates, especially in multi-instance or cloud deployments.
## ✅ Recommended Migration Strategies: 
1. Generate SQL Scripts for Controlled Deployment
Use EF Core to generate an idempotent SQL script, which can be reviewed and applied manually or through your CI/CD pipeline.
`Script-Migration -Idempotent -Output "Migrations\Prod.sql"`
2. Apply Migrations from a Deployment Environment
