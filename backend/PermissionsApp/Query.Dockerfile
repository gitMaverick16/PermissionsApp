FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Query/PermissionsApp.Query.Api/PermissionsApp.Query.Api.csproj", "PermissionsApp.Query.Api/"]
COPY ["src/Query/PermissionsApp.Query.Application/PermissionsApp.Query.Application.csproj", "PermissionsApp.Query.Application/"]
COPY ["src/Query/PermissionsApp.Query.Contracts/PermissionsApp.Query.Contracts.csproj", "PermissionsApp.Query.Contracts/"]
COPY ["src/Query/PermissionsApp.Query.Domain/PermissionsApp.Query.Domain.csproj", "PermissionsApp.Query.Domain/"]
COPY ["src/Query/PermissionsApp.Query.Infrastructure/PermissionsApp.Query.Infrastructure.csproj", "PermissionsApp.Query.Infrastructure/"]
RUN dotnet restore "PermissionsApp.Query.Api/PermissionsApp.Query.Api.csproj"
COPY . ../
WORKDIR /src/Query/PermissionsApp.Query.Api
RUN dotnet build "PermissionsApp.Query.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
ENV ASPNETCORE_HTTP_PORTS=5043
EXPOSE 5043
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PermissionsApp.Query.Api.dll"]