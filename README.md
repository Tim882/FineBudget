# FineBudget

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![React](https://img.shields.io/badge/React-18-61DAFB?logo=react)
![TypeScript](https://img.shields.io/badge/TypeScript-5.5-3178C6?logo=typescript)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-4169E1?logo=postgresql)
![Docker](https://img.shields.io/badge/Docker-✓-2496ED?logo=docker)
![License](https://img.shields.io/badge/license-MIT-green)

**Personal & Family Budget Manager** — full-stack application for tracking income, expenses, and planning your monthly budget. Built with Clean Architecture, CQRS, and modern frontend stack.

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Screenshots](#screenshots)
- [Key Design Decisions](#key-design-decisions)
- [Roadmap](#roadmap)
- [License](#license)

![Dashboard](docs/dashboard.png)

## Features

- 🔐 **JWT Authentication** — access & refresh tokens, secure password hashing
- 💰 **Transaction Management** — CRUD for income and expenses with categories
- 📊 **Analytics Dashboard** — pie charts by category, income/expense dynamics over 6 months
- 🏷️ **Custom Categories** — create, edit, delete with emoji icons
- 🔍 **Monthly Filtering** — browse transactions by any month/year
- 📱 **Responsive Design** — works on desktop and mobile
- 🐳 **Docker** — PostgreSQL, Seq, Jaeger in Docker Compose
- 📝 **Structured Logging** — Serilog + Seq with full-text search
- 🔎 **Distributed Tracing** — OpenTelemetry + Jaeger
- ✅ **Health Checks** — database connectivity monitoring

## Tech Stack

### Backend
| Technology | Purpose |
|---|---|
| .NET 8 | Runtime |
| ASP.NET Core Minimal API | Web framework |
| Entity Framework Core | ORM |
| PostgreSQL 16 | Database |
| MediatR + CQRS | Application architecture |
| FluentValidation | Request validation |
| Ardalis.Specification | Query specifications |
| Serilog + Seq | Structured logging |
| OpenTelemetry + Jaeger | Distributed tracing |
| JWT (Bearer) | Authentication |
| BCrypt.Net | Password hashing |
| Docker | Containerization |

### Frontend
| Technology | Purpose |
|---|---|
| React 18 | UI library |
| TypeScript | Type safety |
| Vite | Build tool |
| MUI 6 | Component library |
| TanStack Query | Server state |
| Zustand | Client state |
| React Router | Routing |
| Recharts | Charts |
| Axios | HTTP client |

## Architecture
┌─────────────────────────────────────────────────────────────┐
│                    React SPA (Vite + TypeScript)             │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐ ┌───────────────┐  │
│  │   MUI 6  │ │  Recharts│ │  Zustand │ │ TanStack Query│  │
│  └──────────┘ └──────────┘ └──────────┘ └───────────────┘  │
└────────────────────────┬────────────────────────────────────┘
                         │ HTTPS + JWT Bearer
┌────────────────────────▼────────────────────────────────────┐
│              ASP.NET Core 8 Minimal API                      │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐ ┌───────────────┐  │
│  │  MediatR │ │FluentVal │ │ OpenTele │ │   Serilog     │  │
│  │   CQRS   │ │ idation  │ │  metry   │ │    → Seq      │  │
│  └──────────┘ └──────────┘ └──────────┘ └───────────────┘  │
└────────────────────────┬────────────────────────────────────┘
                         │ EF Core
┌────────────────────────▼────────────────────────────────────┐
│                      PostgreSQL 16                           │
└─────────────────────────────────────────────────────────────┘

Observability: Seq (logs) · Jaeger (traces)

### Solution Structure

FineBudget/
├── server/
│   ├── FineBudget.sln
│   └── src/
│       ├── FineBudget.Domain/          # Entities & Enums
│       │   ├── Entities/
│       │   │   ├── Transaction.cs
│       │   │   ├── Category.cs
│       │   │   ├── User.cs
│       │   │   └── RefreshToken.cs
│       │   └── Enums/
│       │       └── TransactionType.cs
│       │
│       ├── FineBudget.Application/     # CQRS, Specifications, Interfaces
│       │   ├── Categories/
│       │   │   ├── Commands/           # Create, Update, Delete
│       │   │   └── Queries/            # GetCategories, GetCategoryById
│       │   ├── Transactions/
│       │   │   ├── Commands/           # Create, Update, Delete
│       │   │   └── Queries/            # GetByMonth, GetById
│       │   ├── Statistics/
│       │   │   └── Queries/            # GetByCategory
│       │   ├── Auth/                   # Register, Login, Refresh, Logout
│       │   ├── Specifications/         # Ardalis.Specification
│       │   ├── Common/
│       │   │   ├── Behaviors/          # ValidationBehavior
│       │   │   └── Interfaces/         # IAppDbContext, ICurrentUserService
│       │   └── DependencyInjection.cs
│       │
│       ├── FineBudget.Infrastructure/  # EF Core, Configurations
│       │   ├── Persistence/
│       │   │   ├── AppDbContext.cs
│       │   │   └── Configurations/
│       │   │       ├── TransactionConfiguration.cs
│       │   │       ├── CategoryConfiguration.cs
│       │   │       ├── UserConfiguration.cs
│       │   │       └── RefreshTokenConfiguration.cs
│       │   └── DependencyInjection.cs
│       │
│       └── FineBudget.Api/             # Minimal API Endpoints
│           ├── Services/
│           │   ├── JwtService.cs
│           │   └── CurrentUserService.cs
│           ├── Program.cs
│           └── appsettings.json
│
├── client/
│   └── src/
│       ├── api/
│       │   ├── axios.ts                # Axios instance + interceptors
│       │   ├── auth.ts                 # Auth API functions
│       │   ├── categories.ts           # Categories API functions
│       │   ├── transactions.ts         # Transactions API functions
│       │   └── statistics.ts           # Statistics API functions
│       ├── components/
│       │   └── layout/
│       │       ├── AppLayout.tsx        # Sidebar + Header
│       │       └── ProtectedRoute.tsx   # Auth guard
│       ├── pages/
│       │   ├── LoginPage.tsx
│       │   ├── RegisterPage.tsx
│       │   ├── DashboardPage.tsx        # Charts + summary cards
│       │   ├── TransactionsPage.tsx     # CRUD + monthly filter
│       │   └── CategoriesPage.tsx       # CRUD with dialog
│       ├── store/
│       │   └── authStore.ts             # Zustand auth state
│       ├── theme.ts                     # MUI dark theme
│       ├── App.tsx                      # Routes
│       └── main.tsx                     # Entry point
│
├── docker-compose.yml                   # PostgreSQL + Seq + Jaeger
└── docs/
    ├── login.png
    ├── dashboard.png
    ├── transactions.png
    └── categories.png

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20+](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Entity Framework Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### 1. Clone and Start Infrastructure

Clone the repository and start Docker containers:

    git clone https://github.com/Tim882/FineBudget.git
    cd FineBudget
    docker-compose up -d

This starts PostgreSQL, Seq, and Jaeger.

### 2. Backend

    cd server
    dotnet restore
    dotnet ef database update --project src/FineBudget.Infrastructure --startup-project src/FineBudget.Api
    dotnet run --project src/FineBudget.Api

API starts at **https://localhost:7121** and **http://localhost:5064**.

### 3. Frontend

    cd client
    npm install
    npm run dev

Frontend starts at **http://localhost:5173** with proxy to backend API.

### 4. Verify

| Service | URL |
|---------|-----|
| App | http://localhost:5173 |
| Swagger UI | https://localhost:7121/swagger |
| Health Check | https://localhost:7121/health |
| Seq (logs) | http://localhost:8081 |
| Jaeger (traces) | http://localhost:16686 |

### Quick Test

1. Open http://localhost:5173 and register a new account
2. Create a few categories (e.g. 🛒 Groceries, 💰 Salary)
3. Add some transactions for the current month
4. Check the dashboard — charts will appear automatically

## API Endpoints

### Auth (public)

| Method | Path | Description |
|--------|------|-------------|
| POST | /api/auth/register | Register new user |
| POST | /api/auth/login | Login, get access and refresh tokens |
| POST | /api/auth/refresh | Refresh expired access token |
| POST | /api/auth/logout | Revoke refresh token |

### Categories (protected)

| Method | Path | Description |
|--------|------|-------------|
| GET | /api/categories | List all categories for current user |
| GET | /api/categories/{id} | Get category by ID |
| POST | /api/categories | Create new category |
| PUT | /api/categories/{id} | Update existing category |
| DELETE | /api/categories/{id} | Delete category |

### Transactions (protected)

| Method | Path | Description |
|--------|------|-------------|
| GET | /api/transactions?year=&month= | List transactions by month |
| GET | /api/transactions/{id} | Get transaction by ID |
| POST | /api/transactions | Create new transaction |
| PUT | /api/transactions/{id} | Update existing transaction |
| DELETE | /api/transactions/{id} | Delete transaction |

### Statistics (protected)

| Method | Path | Description |
|--------|------|-------------|
| GET | /api/statistics/by-category?year=&month= | Expenses grouped by category |

## Screenshots

| | |
|---|---|
| ![Login](docs/login.png) | ![Dashboard](docs/dashboard.png) |
| *Login page* | *Dashboard with charts* |
| ![Transactions](docs/transactions.png) | ![Categories](docs/categories.png) |
| *Transaction list with monthly filter* | *Category management* |

> Add screenshots to `docs/` folder. Use Cmd+Shift+4 on Mac to capture.

## Key Design Decisions

- **Clean Architecture** — strict separation of Domain, Application, Infrastructure, and API layers. Domain has zero dependencies
- **CQRS with MediatR** — every operation is a Command or Query with its own Handler. Thin API endpoints, testable logic
- **Specification Pattern** — reusable query logic via Ardalis.Specification. No LINQ duplication across handlers
- **FluentValidation** — declarative request validation with automatic pipeline behaviors via MediatR
- **Minimal API** — modern .NET endpoint routing without controllers. Less boilerplate, better performance
- **JWT + Refresh Tokens** — refresh tokens stored in DB, revocable. Frontend auto-refreshes on 401 via Axios interceptor
- **Multi-User Ready** — all data scoped to authenticated user via ICurrentUserService. Ready for family sharing feature
- **Structured Logging** — Serilog writes structured logs to console and Seq. Full-text search, filtering, and correlation
- **Distributed Tracing** — OpenTelemetry traces every HTTP request and DB query. Exported to Jaeger for analysis
- **Health Checks** — database connectivity monitoring at `/health` endpoint with Entity Framework Core check
- **BCrypt Password Hashing** — industry-standard adaptive hashing. No plaintext passwords ever stored
- **Dark Theme by Default** — MUI 6 dark theme with glassmorphism effects, gradient accents, and smooth transitions

## Roadmap

- [x] JWT Authentication with refresh tokens
- [x] Full CRUD for Transactions and Categories
- [x] Dashboard with PieChart and BarChart
- [x] Monthly filtering for transactions
- [x] Responsive glassmorphism UI with MUI 6
- [x] Structured logging (Serilog + Seq)
- [x] Distributed tracing (OpenTelemetry + Jaeger)
- [x] Docker Compose for all infrastructure
- [ ] Budget planning with cashflow forecast
- [ ] CSV/OFX bank statement import via RabbitMQ + MassTransit
- [ ] Family sharing (multiple users per household with roles)
- [ ] Recurring transactions (monthly bills, salary)
- [ ] Azure deployment (App Service, Azure SQL, Key Vault, Service Bus)
- [ ] Unit and integration tests (xUnit, Moq, Testcontainers)

## License

MIT © [Tim Sharafutdinov](https://github.com/Tim882)