FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos todo el contenido de una vez
COPY . .

# Restauramos usando la solución
RUN dotnet restore "LegalSystem.Solution.sln"

# Publicamos usando un comodín para que encuentre la API esté donde esté
RUN dotnet publish "**/LegalSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE 8080

ENTRYPOINT ["dotnet", "LegalSystem.API.dll"]