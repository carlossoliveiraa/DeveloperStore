# Developer Store - Sales API

A robust sales management system built with .NET 8, following Domain-Driven Design (DDD), Clean Architecture, and SOLID principles.

Repository Front-End : https://github.com/carlossoliveiraa/DeveloperStore-Front

---

## Architecture Overview

### Domain-Driven Design (DDD)
- **Domain Layer**: Core business logic, entities, and value objects
- **Application Layer**: Use case orchestration and business rules
- **Infrastructure Layer**: Database access and external services
- **API Layer**: RESTful interface
- **CrossCutting Layer**: Shared components and utilities

### Design Patterns Implemented
- **Repository Pattern**: Data access abstraction
- **Unit of Work**: Transactional consistency
- **Strategy Pattern**: Flexible business rule logic
- **Factory Pattern**: Object creation strategy
- **Observer Pattern**: Event handling via MediatR
- **Specification Pattern**: Encapsulated complex rules

### Clean Architecture
- Clear separation of concerns
- Framework-independent core
- Inversion of control
- Testable and maintainable

---

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Visual Studio 2022 or VS Code

---

### ⚠️ Important: PostgreSQL must be running

Before running the API, ensure the PostgreSQL container is running.
If not, the application will fail to start due to a missing database connection.

⚠️ Important: Make sure you are inside the root folder of the project before executing the command below.
Otherwise, Docker won't be able to find the correct docker-compose.db.yml file and the command will not work.

Start the database container with:
docker-compose -f docker-compose.db.yml up -d

or run it manually if the database does not yet exist

docker run --name sales_postgres \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=SalesDb \
  -p 5432:5432 \
  -d postgres:16

### Installation

1. Clone the repository:
git clone https://github.com/carlossoliveiraa/DeveloperStore.git
cd DeveloperStore

3. Run database migrations:
# Sales context
Add-Migration InitialSales -Context SalesDbContext -OutputDir Data/Migrations/Sales
Update-Database -Context SalesDbContext

# Identity context
Add-Migration InitialIdentity -Context UserDbContext -OutputDir Data/Migrations/Identity
Update-Database -Context UserDbContext
3. Run the API:
dotnet run --project DeveloperStore.Sales.API

## API Usage

### 🔐 Authentication

Request a JWT token:
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@admin.com",
  "password": "admin"
}
```

### 🛒 Creating a Sale

POST /api/sales
Authorization: Bearer {your-jwt-token}
Content-Type: application/json

### 🛒 Creating a Sale

```http
POST /api/sales
Authorization: Bearer {your-jwt-token}
Content-Type: application/json

{
  "saleNumber": "S-20250403-001",
  "saleDate": "2025-04-03T19:30:00Z",
  "customerId": "c1a1a8a0-4c44-4f3e-9e61-d2a62d1bcd11",
  "customerName": "Carlos Oliveira",
  "branchId": "a7e1a5c3-6f4f-4412-8b99-71bb30d54c99",
  "branchName": "Filial São Paulo",
  "items": [
    {
      "productId": "f3e3c8f1-5f01-4d4f-9f4d-12bb5566b1aa",
      "productName": "Monitor 27\" 4K",
      "quantity": 5,
      "unitPrice": 1200.00
    },
    {
      "productId": "da8b1e9e-2334-4dc9-8b6a-5c27e8f5e33d",
      "productName": "Cabo HDMI 2m",
      "quantity": 2,
      "unitPrice": 50.00
    }
  ]
}
```

## Testing Coverage

The project maintains over **70% unit test coverage**, using:

- **xUnit** for test framework
- **Moq** for mocking
- **FluentAssertions** for fluent assertions

### Covered Areas:
- Domain entities and value objects
- Application services and orchestrators
- Command and query handlers
- Business rule validations
- Critical path integration tests

## Recent Updates

### 📣 Event-Based Architecture

Event-driven messaging system integrated with MediatR:
- `QueueMessageEvent`: Represents outbound messages
- `QueueMessageEventHandler`: Logs message details
- Asynchronous processing with structured logging

---

## Technical Specifications

| Feature            | Technology               |
|--------------------|---------------------------|
| Framework          | .NET 8                    |
| Database           | PostgreSQL                |
| ORM                | Entity Framework Core     |
| Authentication     | JWT Bearer                |
| API Documentation  | Swagger / OpenAPI         |
| Logging            | Serilog (structured)      |
| Messaging          | MediatR (in-memory bus)   |

---

## Contributing

Pull requests are welcome. Please review our contributing guidelines before submitting.
