# Stage 1: Application Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY src/Curotec.WebAPI/Curotec.WebAPI.csproj src/Curotec.WebAPI/
COPY src/Curotec.Data/Curotec.Data.csproj src/Curotec.Data/

RUN dotnet restore src/Curotec.WebAPI/Curotec.WebAPI.csproj
RUN dotnet restore src/Curotec.Data/Curotec.Data.csproj

COPY . .

RUN dotnet build src/Curotec.WebAPI/Curotec.WebAPI.csproj -c Release -o /app/build

# Stage 2: Application Publishing
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

COPY --from=build /app/build .

# Expor todas as portas
EXPOSE 80
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "Curotec.WebAPI.dll"]

# Stage 3: Final Configuration
FROM base AS final

WORKDIR /app

COPY --from=build /app/build .

EXPOSE 80
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:80
ENV DB_SERVER=sqlserver-container

ENTRYPOINT ["dotnet", "Curotec.WebAPI.dll"]
