# Developer Store Sales API

## Initial Setup

1. Clone the repository
```bash
git clone [repository-url]
cd DeveloperStore
```

2. Install EF Core tools
```powershell
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL
```

3. Configure PostgreSQL
```bash
# Using Docker
docker run --name developer-store-db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=SalesDb -p 5432:5432 -d postgres:15
```

4. Run Migrations
```powershell
# In Infrastructure layer
Add-Migration InitialSales -Context SalesDbContext -OutputDir Data/Migrations/Sales
Add-Migration InitialIdentity -Context UserDbContext -OutputDir Data/Migrations/Identity
```

5. Start the application
```powershell
dotnet run --project DeveloperStore.Sales.API
```

## Authentication

1. Login to get JWT token:
```bash
POST http://localhost:8080/api/v1/auth/signin
Content-Type: application/json

{
  "email": "admin@admin.com",
  "password": "admin"
}
```

2. Use the returned token in Authorization header:
```
Authorization: [token]
```

## Main Endpoints

### Create Sale
```bash
POST /api/v1/sales/create
Content-Type: application/json
Authorization: [token]

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

### Other Endpoints
- `GET /api/v1/sales/{id}` - Get sale by ID
- `GET /api/v1/sales?page=1&pageSize=10` - List paginated sales
- `PATCH /api/v1/sales/{id}/cancel` - Cancel sale

## Technologies

- .NET 8
- PostgreSQL
- Entity Framework Core
- JWT Authentication
- Docker

## Project Structure

The solution is organized using Clean Architecture principles with the following layers:

- **DeveloperStore.Sales.API**: Presentation layer (API endpoints)
- **DeveloperStore.Sales.Application**: Application layer (use cases and business logic)
- **DeveloperStore.Sales.Domain**: Domain layer (entities and business rules)
- **DeveloperStore.Sales.Infrastructure**: Infrastructure layer (data access, external services)
- **DeveloperStore.Sales.CrossCutting**: Cross-cutting concerns (logging, validation)
- **DeveloperStore.Sales.Tests**: Unit and integration tests

## Prerequisites

- [Docker](https://www.docker.com/get-docker)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PowerShell](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell) (for Windows users)

## Getting Started

### 1. Clone the Repository

```bash
git clone [repository-url]
cd DeveloperStore
```

### 2. Start the Development Environment

The project uses Docker Compose to set up the development environment. You can start all services using the provided PowerShell script:

```powershell
.\start-dev.ps1
```

Or manually using Docker Compose:

```bash
docker-compose up -d
```

This will start:
- PostgreSQL database on port 5432
- API service on ports 8080 (HTTP) and 8081 (HTTPS)

### 3. Database Configuration

The PostgreSQL database is automatically configured with the following settings:
- Host: localhost
- Port: 5432
- Database: SalesDb
- Username: postgres
- Password: postgres

### 4. API Access

Once the services are running, you can access the API at:
- HTTP: http://localhost:8080
- HTTPS: https://localhost:8081

### 5. Development Workflow

The project is set up for development with hot-reload enabled. Changes to the code will automatically trigger a rebuild of the application.

## Environment Variables

The following environment variables are configured in the docker-compose.yml:

- `ASPNETCORE_ENVIRONMENT`: Development
- `ASPNETCORE_URLS`: "http://+:80;https://+:443"
- `ConnectionStrings__SalesConnection`: PostgreSQL connection string
- `ASPNETCORE_Kestrel__Certificates__Default__Path`: Path to SSL certificate
- `ASPNETCORE_Kestrel__Certificates__Default__Password`: SSL certificate password

## Stopping the Services

To stop all services:

```bash
docker-compose down
```

## Additional Information

- The project uses Clean Architecture principles for better separation of concerns and maintainability
- PostgreSQL is used as the primary database
- Docker is used for containerization and development environment setup
- The API is configured with both HTTP and HTTPS endpoints
- Development environment includes hot-reload for faster development cycles

## Troubleshooting

If you encounter any issues:

1. Ensure all prerequisites are installed
2. Check if Docker is running
3. Verify that ports 5432, 8080, and 8081 are not in use by other applications
4. Check Docker logs for any errors:
   ```bash
   docker-compose logs
   ```
 