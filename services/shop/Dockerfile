# Stage 1: Base image for building
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution file
COPY Offgrid.slnx .

# Copy project files
COPY src/ShopApi/ShopApi.csproj src/ShopApi/
COPY src/Customers/Customers.csproj src/Customers/

# Restore dependencies
RUN dotnet restore

# Copy all source files
COPY . .

# Build the solution
WORKDIR /src/src/ShopApi
RUN dotnet build ShopApi.csproj -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish ShopApi.csproj -c Release -o /app/publish --no-restore /p:UseAppHost=false

# Stage 3: Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Install system dependencies
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

# Copy published application
COPY --from=publish /app/publish .

# Listen on port 7000 inside the container
ENV ASPNETCORE_URLS=http://+:7000

# Expose port
EXPOSE 7000

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:7000 || exit 1

# Entry point
ENTRYPOINT ["dotnet", "ShopApi.dll"]
