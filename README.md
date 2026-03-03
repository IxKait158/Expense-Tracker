# Expense Tracker API

A robust, full-stack personal finance management application designed to help users track their daily expenses, manage custom categories, and view financial statistics. 

This project was built to demonstrate backend best practices, including **Clean Architecture**, the **CQRS pattern**, and secure API design using **.NET**.

## Tech Stack

**Backend:**
* **Framework:** C# / .NET 8 (ASP.NET Core Web API)
* **Architecture:** Clean Architecture & CQRS Pattern
* **Mediator:** MediatR (for decoupling request handling)
* **ORM:** Entity Framework Core
* **Database:** MS SQL Server
* **Security:** ASP.NET Core Identity, JWT Bearer Authentication

**Frontend:**
* HTML5 & CSS3
* Vanilla JavaScript (ES Modules, Fetch API)

## Architecture Overview

The backend is structured using **Clean Architecture** principles to ensure separation of concerns, testability, and maintainability:

1. **Domain:** Contains enterprise-wide logic and core entities (`User`, `Category`, `Transaction`, `BaseEntity`).
2. **Application:** Contains business logic, DTOs, interfaces, and CQRS handlers (Commands/Queries) using MediatR.
3. **Infrastructure:** Implements data access (EF Core `DbContext`), database configurations, and external services.
4. **API (Presentation):** ASP.NET Core Controllers, middleware (Global Exception Handling), and dependency injection setups.