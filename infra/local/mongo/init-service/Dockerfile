# Use .NET 10 SDK to build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY ["MongoInit/MongoInit.csproj", "MongoInit/"]
RUN dotnet restore "MongoInit/MongoInit.csproj"

# Copy everything and publish
COPY . .
WORKDIR /src/MongoInit
RUN dotnet publish "MongoInit.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS runtime
WORKDIR /app

# Copy published app
COPY --from=build /app/publish .

ENV DOTNET_USE_POLLING_FILE_WATCHER=false

ENTRYPOINT ["dotnet", "MongoInit.dll"]
