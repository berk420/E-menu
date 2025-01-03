# Base image for .NET 6.0 ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image for .NET 6.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["E-menu_backend.csproj", "E-menu_backend/"]
RUN dotnet restore "./E-menu_backend/E-menu_backend.csproj"
COPY . . 
WORKDIR "/src/E-menu_backend"
RUN dotnet build "./E-menu_backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./E-menu_backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Base image for final build
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN apt-get update && apt-get install -y \
    libgdiplus \
    libc6-dev \
    libx11-dev \
    libgtk-3-dev \
    libasound2-dev \
    libcups2-dev \
    libgdk-pixbuf2.0-dev \
    libjpeg-dev \
    libpng-dev \
    libfontconfig1 && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

# Set the entry point for your application
ENTRYPOINT ["dotnet", "E-menu_backend.dll"]
