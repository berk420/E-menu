# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Stage 1: Build stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Restore dependencies
COPY E-menu_backend.csproj ./
RUN dotnet restore "./E-menu_backend.csproj"
# Copy source code
COPY . .
WORKDIR "/src"
# Build the project
RUN dotnet build "./E-menu_backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 2: Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./E-menu_backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 3: Run stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-menu_backend.dll"]
