# Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS="http://+:80;https://+:443"
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["DeveloperStore.Sales.API/DeveloperStore.Sales.API.csproj", "DeveloperStore.Sales.API/"]
COPY ["DeveloperStore.Sales.Application/DeveloperStore.Sales.Application.csproj", "DeveloperStore.Sales.Application/"]
COPY ["DeveloperStore.Sales.Infrastructure/DeveloperStore.Sales.Infrastructure.csproj", "DeveloperStore.Sales.Infrastructure/"]
COPY ["DeveloperStore.Sales.Domain/DeveloperStore.Sales.Domain.csproj", "DeveloperStore.Sales.Domain/"]

RUN dotnet restore "DeveloperStore.Sales.API/DeveloperStore.Sales.API.csproj"

COPY . .

WORKDIR "/src/DeveloperStore.Sales.API"
RUN dotnet build "DeveloperStore.Sales.API.csproj" -c Debug -o /app/build --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish "DeveloperStore.Sales.API.csproj" -c Debug -o /app/publish /p:UseAppHost=false --no-restore

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "DeveloperStore.Sales.API.dll"]
