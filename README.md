This project is a .NET Core API for a sales system, using Clean Architecture principles and PostgreSQL as the database.

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
 