# Estágio 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Defina o diretório de trabalho no contêiner
WORKDIR /src

# Copie os arquivos .csproj para os diretórios de destino
COPY Curotec.WebAPI/Curotec.WebAPI.csproj Curotec.WebAPI/
COPY Curotec.Data/Curotec.Data.csproj Curotec.Data/

# Restaure as dependências
RUN dotnet restore Curotec.WebAPI/Curotec.WebAPI.csproj
RUN dotnet restore Curotec.Data/Curotec.Data.csproj

# Copie o restante dos arquivos do projeto
COPY . .

# Construa o projeto
RUN dotnet build Curotec.WebAPI/Curotec.WebAPI.csproj -c Release -o /app/build

# Estágio 2: Publicação da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# Defina o diretório de trabalho para a aplicação
WORKDIR /app

# Copie os arquivos compilados do estágio de build
COPY --from=build /app/build .

# Exponha a porta onde a aplicação irá rodar
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "Curotec.WebAPI.dll"]

# Estágio 3: Configuração final
FROM base AS final

# Defina o diretório de trabalho no contêiner final
WORKDIR /app

# Copie o arquivo de publicação
COPY --from=build /app/build .

# Exponha a porta
EXPOSE 80

# Comando de execução final
ENTRYPOINT ["dotnet", "Curotec.WebAPI.dll"]
