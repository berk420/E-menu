# Base image for .NET 6.0 runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image for .NET 6.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-menu_backend.csproj", "E-menu_backend/"]
WORKDIR "/src/E-menu_backend"
RUN dotnet restore

# Kaynak kodunu kopyala
COPY . .

# Self-contained uygulama için RuntimeIdentifier ekle
RUN dotnet build -c Release -r linux-x64 --self-contained true -o /app/build

# Publish iþlemi
FROM build AS publish
RUN dotnet publish -c Release -r linux-x64 --self-contained true -o /app/publish

# Final imaj
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Gerekli baðýmlýlýklarý yükle
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

# Uygulamayý baþlat
ENTRYPOINT ["/app/E-menu_backend"]
