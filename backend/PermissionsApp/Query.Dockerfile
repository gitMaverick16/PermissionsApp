# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar la solución y todos los proyectos
COPY PermissionsApp.sln ./
COPY src/Query src/Query

# Restaurar paquetes
WORKDIR /app/src/Query/PermissionsApp.Query.Api
RUN dotnet restore

# Compilar y publicar
RUN dotnet publish -c Release -o /publish --no-restore

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /publish .

EXPOSE 8082
ENTRYPOINT ["dotnet", "PermissionsApp.Query.Api.dll"]
