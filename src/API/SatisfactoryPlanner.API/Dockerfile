#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/SatisfactoryPlanner.API/SatisfactoryPlanner.API.csproj", "API/SatisfactoryPlanner.API/"]
COPY ["BuildingBlocks/Application/SatisfactoryPlanner.BuildingBlocks.Application.csproj", "BuildingBlocks/Application/"]
COPY ["BuildingBlocks/Domain/SatisfactoryPlanner.BuildingBlocks.Domain.csproj", "BuildingBlocks/Domain/"]
COPY ["Modules/Production/Application/SatisfactoryPlanner.Modules.Production.Application.csproj", "Modules/Production/Application/"]
COPY ["Modules/Production/Domain/SatisfactoryPlanner.Modules.Production.Domain.csproj", "Modules/Production/Domain/"]
COPY ["Modules/Production/Infrastructure/SatisfactoryPlanner.Modules.Production.Infrastructure.csproj", "Modules/Production/Infrastructure/"]
COPY ["BuildingBlocks/Infrastructure/SatisfactoryPlanner.BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/Infrastructure/"]
COPY ["Modules/Worlds/Application/SatisfactoryPlanner.Modules.Worlds.Application.csproj", "Modules/Worlds/Application/"]
COPY ["Modules/UserAccess/IntegrationEvents/SatisfactoryPlanner.Modules.UserAccess.IntegrationEvents.csproj", "Modules/UserAccess/IntegrationEvents/"]
COPY ["Modules/Worlds/Domain/SatisfactoryPlanner.Modules.Worlds.Domain.csproj", "Modules/Worlds/Domain/"]
COPY ["Modules/Worlds/IntegrationEvents/SatisfactoryPlanner.Modules.Worlds.IntegrationEvents.csproj", "Modules/Worlds/IntegrationEvents/"]
COPY ["Modules/Worlds/Infrastructure/SatisfactoryPlanner.Modules.Worlds.Infrastructure.csproj", "Modules/Worlds/Infrastructure/"]
COPY ["BuildingBlocks/SatisfactoryPlanner.BuildingBlocks.EventBus/SatisfactoryPlanner.BuildingBlocks.EventBus.csproj", "BuildingBlocks/SatisfactoryPlanner.BuildingBlocks.EventBus/"]
COPY ["Modules/Resources/Application/SatisfactoryPlanner.Modules.Resources.Application.csproj", "Modules/Resources/Application/"]
COPY ["Modules/Resources/Domain/SatisfactoryPlanner.Modules.Resources.Domain.csproj", "Modules/Resources/Domain/"]
COPY ["Modules/Resources/Infrastructure/SatisfactoryPlanner.Modules.Resources.Infrastructure.csproj", "Modules/Resources/Infrastructure/"]
COPY ["Modules/UserAccess/Application/SatisfactoryPlanner.Modules.UserAccess.Application.csproj", "Modules/UserAccess/Application/"]
COPY ["Modules/UserAccess/Domain/SatisfactoryPlanner.Modules.UserAccess.Domain.csproj", "Modules/UserAccess/Domain/"]
COPY ["Modules/UserAccess/Infrastructure/SatisfactoryPlanner.Modules.UserAccess.Infrastructure.csproj", "Modules/UserAccess/Infrastructure/"]
RUN dotnet restore "./API/SatisfactoryPlanner.API/./SatisfactoryPlanner.API.csproj"
COPY . .
WORKDIR "/src/API/SatisfactoryPlanner.API"
RUN dotnet build "./SatisfactoryPlanner.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SatisfactoryPlanner.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SatisfactoryPlanner.API.dll"]