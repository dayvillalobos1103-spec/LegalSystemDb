# 1. ETAPA DE CONSTRUCCIÓN (BUILD)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos la solución
COPY ["LegalSystem.Solution.sln", "./"]

# Copiamos los proyectos con tus nombres REALES (con el .API)
COPY ["LegalSystem.API/LegalSystem.API.csproj", "LegalSystem.API/"]
COPY ["LegalSystem.Application/LegalSystem.Application.csproj", "LegalSystem.Application/"]
COPY ["LegalSystem.Domain/LegalSystem.Domain.csproj", "LegalSystem.Domain/"]
COPY ["LegalSystem.Infraestructura/LegalSystem.Infraestructura.csproj", "LegalSystem.Infraestructura/"]

# Restaurar
RUN dotnet restore "LegalSystem.Solution.sln"

# Copiar todo y publicar
COPY . .
WORKDIR "/src/LegalSystem.API"
RUN dotnet publish "LegalSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE 8080

# El nombre del archivo de salida
ENTRYPOINT ["dotnet", "LegalSystem.API.dll"]