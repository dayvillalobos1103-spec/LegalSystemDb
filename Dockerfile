FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiamos todo
COPY . .

# 2. Restauramos
RUN dotnet restore "LegalSystem.Solution.sln"

# 3. PUBLICAMOS: ¡Usando la ruta exacta que vimos en tu log de éxito!
RUN dotnet publish "LegalSystem/LegalSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- RUNTIME ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE 8080

ENTRYPOINT ["dotnet", "LegalSystem.API.dll"]