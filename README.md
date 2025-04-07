# Curotec - Full Stack Setup Guide (Frontend + Backend + DB via Docker)

This guide provides instructions for running the complete Curotec application—frontend, backend, and database—using Docker Compose, along with an option to run only the backend separately.

---

## ✅ Prerequisites

- Docker and Docker Compose installed  
- .NET SDK 8.0 or higher installed  
- Entity Framework Core CLI installed (for running migrations)

---

## 🚀 Running Everything (Frontend + Backend + Database)

1. Make sure Docker is running.
2. From the **root project directory** (where the full `docker-compose.yml` is located), run:

   ```bash
   docker compose up --build
   ```

3. Access the application:

   - Frontend: [http://localhost:4200](http://localhost:4200)  
   - Backend (Swagger): [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

4. After all containers are up, run the database migration:

   ```bash
   cd ./Curotec.backend/src

   dotnet ef database update --project ./Curotec.Data/Curotec.Data.csproj --startup-project ./Curotec.WebAPI/Curotec.WebAPI.csproj --configuration Release
   ```

---

## 🛠️ Running Only the Backend (with Database)

To run only the backend and the SQL Server container:

1. Navigate to the backend directory:

   ```bash
   cd ./Curotec.backend
   ```

2. Start services using Docker Compose:

   ```bash
   docker compose up --build
   ```

3. Access the API:

   - [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

4. Apply the database migration as before:

   ```bash
   cd ./src

   dotnet ef database update --project ./Curotec.Data/Curotec.Data.csproj --startup-project ./Curotec.WebAPI/Curotec.WebAPI.csproj --configuration Release
   ```

---

## 💡 Notes

- The database must be running before applying migrations.
- Always run migrations from the `Curotec.backend/src` directory.
- The frontend is served by NGINX and available at port `4200`.
- Both frontend and backend communicate via `localhost` during development.

---

You're now ready to run and explore the full Curotec stack with Docker! 🐳✨
