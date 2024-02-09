
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Копируем файлы csproj проекта API
COPY FilmoSearchPortal.API/*.csproj FilmoSearchPortal.API/
RUN dotnet restore FilmoSearchPortal.API/FilmoSearchPortal.API.csproj

# Копируем все файлы проекта API
COPY . .

# Переходим в каталог с проектом API и выполняем сборку
WORKDIR "/src/FilmoSearchPortal.API"  
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FilmoSearchPortal.API.dll"]


