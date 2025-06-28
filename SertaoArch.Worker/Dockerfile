# Use the official .NET 9 runtime as base image
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

# Use the .NET 9 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution file first (for better layer caching)
COPY ["SertaoArch.sln", "."]

# Copy all project files
COPY ["SertaoArch.Worker/SertaoArch.Worker.csproj", "SertaoArch.Worker/"]
COPY ["Infra/SertaoArch.DI/SertaoArch.DI.csproj", "Infra/SertaoArch.DI/"]
COPY ["Infra/SertaoArch.Mapping/SertaoArch.Mapping.csproj", "Infra/SertaoArch.Mapping/"]
COPY ["Infra/SertaoArch.Bootstrap/SertaoArch.Bootstrap.csproj", "Infra/SertaoArch.Bootstrap/"]
COPY ["Infra/SertaoArch.QueueServiceRMQ/SertaoArch.QueueServiceRMQ.csproj", "Infra/SertaoArch.QueueServiceRMQ/"]
COPY ["Infra/SertaoArch.Data/SertaoArch.Data.csproj", "Infra/SertaoArch.Data/"]
COPY ["Infra/SertaoArch.Repository/SertaoArch.Repository.csproj", "Infra/SertaoArch.Repository/"]
COPY ["Infra/SertaoArch.Common/SertaoArch.Common.csproj", "Infra/SertaoArch.Common/"]

# Restore dependencies
RUN dotnet restore "SertaoArch.Worker/SertaoArch.Worker.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR "/src/SertaoArch.Worker"
RUN dotnet build "SertaoArch.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SertaoArch.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: copy the published app to the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV DOTNET_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "SertaoArch.Worker.dll"]

