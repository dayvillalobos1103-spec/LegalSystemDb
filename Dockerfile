FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiamos todo
COPY . .

# 2. Restauramos (esto ya sabemos que funciona)
RUN dotnet restore "LegalSystem.Solution.sln"

# 3. Publicamos usando la ruta exacta que vimos en tus carpetas
RUN dotnet publish "LegalSystem.API/LegalSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- RUNTIME ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Librerías para PostgreSQL
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE 8080

ENTRYPOINT ["dotnet", "LegalSystem.API.dll"]