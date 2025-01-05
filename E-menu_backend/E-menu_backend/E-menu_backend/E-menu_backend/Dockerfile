# Base image for .NET 6.0 runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image for .NET 6.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-menu_backend.csproj", "E-menu_backend/"]
RUN dotnet restore "./E-menu_backend/E-menu_backend.csproj"
COPY . . 
WORKDIR "/src/E-menu_backend"
RUN dotnet build "./E-menu_backend.csproj" -c Release  -r linux-x64 --self-contained true -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "./E-menu_backend.csproj" -c Release -r linux-x64 --self-contained true -o /app/publish


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

# Set the entry point for your application (for self-contained app)

ENTRYPOINT ["/app/E-menu_backend"]



