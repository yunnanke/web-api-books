# THIS IS MY DOCKERFILE — VERIFIED

# Stage 1: Build with .NET 9 SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src
COPY ["API_Books.api.csproj", "."]
RUN dotnet restore

COPY . .
RUN dotnet build "API_Books.api.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "API_Books.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime (ASP.NET Core 9)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_Books.api.dll"]