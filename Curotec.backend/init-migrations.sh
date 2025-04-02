#!/bin/bash

# Espera o SQL Server estar disponível
echo "Aguardando SQL Server ficar disponível..."
until nc -z -v -w30 sqlserver-container 1433
do
  echo "Aguardando a conexão com o SQL Server..."
  sleep 1
done

# Aplica as migrações ao iniciar o contêiner
echo "Aplicando as migrações..."
dotnet ef database update --project /src/Curotec.Data/Curotec.Data.csproj --startup-project /src/Curotec.WebAPI/Curotec.WebAPI.csproj --configuration Release

echo "Migrações aplicadas com sucesso."
