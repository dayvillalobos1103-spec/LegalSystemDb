FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiamos ABSOLUTAMENTE TODO para no fallar con las rutas
COPY . .

# 2. Restauramos usando el archivo de solución (que está en la raíz)
RUN dotnet restore "LegalSystem.Solution.sln"

# 3. Publicamos usando un comodín (**) para que encuentre el proyecto
# Esto busca el archivo .csproj sin importar en qué carpeta esté
RUN dotnet publish "**/LegalSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- RUNTIME ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Librerías para PostgreSQL
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

# Copiamos lo que se publicó
COPY --from=build /app/publish .

# Configuración de Render
ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE 8080

# El nombre del archivo de salida
ENTRYPOINT ["dotnet", "LegalSystem.API.dll"]