# Developer Store - Sales API

A robust sales management system built with .NET 8, following DDD (Domain-Driven Design) principles, Clean Architecture, and SOLID patterns.

## Architecture Overview

### Domain-Driven Design (DDD) Implementation
- **Domain Layer**: Contains the core business logic, entities, and value objects
- **Application Layer**: Handles use cases and orchestrates domain objects
- **Infrastructure Layer**: Implements persistence and external services
- **API Layer**: Provides the REST interface
- **CrossCutting Layer**: Contains shared components and utilities

### Design Patterns Used
- **Repository Pattern**: For data access abstraction
- **Unit of Work**: Ensures transaction consistency
- **Strategy Pattern**: For flexible business rule implementation
- **Factory Pattern**: For object creation
- **Observer Pattern**: For event handling (implemented via MediatR)
- **Specification Pattern**: For complex business rules

### Clean Architecture
- Clear separation of concerns
- Dependency inversion principle
- Independent of frameworks
- Testable architecture

## Getting Started

### Prerequisites
- .NET 8 SDK
- Docker Desktop
- Visual Studio 2022 or VS Code

### Installation Steps

1. Clone the repository
```bash
git clone https://github.com/carlossoliveiraa/DeveloperStore.git
cd DeveloperStore


2. Start PostgreSQL using Docker
```bash
docker-compose -f docker-compose.db.yml up -d
```

3. Run Database Migrations
```bash
# For Sales Context
Add-Migration InitialSales -Context SalesDbContext -OutputDir Data/Migrations/Sales
Update-Database -Context SalesDbContext

# For Identity Context
Add-Migration InitialIdentity -Context UserDbContext -OutputDir Data/Migrations/Identity
Update-Database -Context UserDbContext
```

4. Run the application
```bash
dotnet run --project DeveloperStore.Sales.API
```

## Testing the API

### Authentication
First, obtain a JWT token by authenticating:

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@admin.com",
  "password": "admin"
}
```

### Creating a Sale
Use the following model to create a sale:

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

The project maintains a high testing standard with over 70% unit test coverage. Tests are implemented using:
- xUnit for test framework
- Moq for mocking
- FluentAssertions for assertions


Key areas covered by tests:
- Domain entities and value objects
- Application services
- Command/Query handlers
- Business rules validation
- Integration tests for critical paths

## Recent Updates

### Event Handling Implementation
We've recently added an event-based messaging system using MediatR:
- `QueueMessageEvent`: Represents messages to be queued
- `QueueMessageEventHandler`: Handles message logging
- Asynchronous processing
- Structured logging integration

## Technical Specifications

- **Framework**: .NET 8
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Authentication**: JWT Bearer
- **Documentation**: Swagger/OpenAPI
- **Logging**: Structured logging with Serilog
- **Message Bus**: MediatR for in-memory events

## Contributing

Please read our contributing guidelines before submitting pull requests.

