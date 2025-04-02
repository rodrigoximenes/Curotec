# Curotec Backend - Setup Guide

This guide provides instructions for running the Curotec backend application both locally and via Docker.

## Prerequisites
- Docker and Docker Compose installed
- .NET SDK 8.0 or higher installed
- Entity Framework Core CLI installed

## Getting Started

1. Navigate to the project directory:
```bash
cd ./Curotec.backend
```

### 1. Starting Services
1. Ensure Docker is running.
2. Start containers using Docker Compose:
```bash
docker compose up
```

### 2. Running Database Migration
After the containers are running, navigate to the "src" directory to apply database migrations:
```bash
cd ./src

dotnet ef database update --project ./Curotec.Data/Curotec.Data.csproj --startup-project ./Curotec.WebAPI/Curotec.WebAPI.csproj --configuration Release
```

### 3. Running Locally
To run the application locally, follow these steps:
1. Ensure the SQL Server container is running (e.g., via Docker Compose).
2. Run the application using Visual Studio or the CLI:
   - Using Visual Studio: Open the solution and run the project.
   - Using CLI:
   ```bash
   dotnet run --project ./Curotec.WebAPI/Curotec.WebAPI.csproj
   ```
3. Access the application at: [https://localhost:7090/swagger/index.html](https://localhost:7090/swagger/index.html)

### 4. Running via Docker
Access the application via Docker at:
- [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

## Important Notes
- Ensure the database container is running before applying migrations.
- Both local and Dockerized setups connect to the same SQL Server database.
- Migrations cannot be run before starting the database container.
- Always run the migration from the "src" directory.
- Both environments are sharing the same SQL Server database container.

You're all set to explore the Curotec API! 🚀

