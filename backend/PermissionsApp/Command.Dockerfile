FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Command/PermissionsApp.Command.Api/PermissionsApp.Command.Api.csproj", "PermissionsApp.Command.Api/"]
COPY ["src/Command/PermissionsApp.Command.Application/PermissionsApp.Command.Application.csproj", "PermissionsApp.Command.Application/"]
COPY ["src/Command/PermissionsApp.Command.Contracts/PermissionsApp.Command.Contracts.csproj", "PermissionsApp.Command.Contracts/"]
COPY ["src/Command/PermissionsApp.Command.Domain/PermissionsApp.Command.Domain.csproj", "PermissionsApp.Command.Domain/"]
COPY ["src/Command/PermissionsApp.Command.Infrastructure/PermissionsApp.Command.Infrastructure.csproj", "PermissionsApp.Command.Infrastructure/"]
RUN dotnet restore "PermissionsApp.Command.Api/PermissionsApp.Command.Api.csproj"
COPY . ../
WORKDIR /src/Command/PermissionsApp.Command.Api
RUN dotnet build "PermissionsApp.Command.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
ENV ASPNETCORE_HTTP_PORTS=5112
EXPOSE 5112
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PermissionsApp.Command.Api.dll"]