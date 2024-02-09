##Build Stage
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY ["FilmoSearchPortal.API/FilmoSearchPortal.API.csproj", "FilmoSearchPortal.API/"]
#COPY ["FilmoSearchPortal.BLL/FilmoSearchPortal.BLL.csproj", "FilmoSearchPortal.BLL/"]
#COPY ["FilmoSearchPortal.DAL/FilmoSearchPortal.DAL.csproj", "FilmoSearchPortal.DAL/"]
#RUN dotnet restore "./FilmoSearchPortal.API/FilmoSearchPortal.API.csproj" --disable-parallel
#RUN dotnet publish "./FilmoSearchPortal.API/FilmoSearchPortal.API.csproj" -c release -o /app --no-restore
#
#
##Server Stage
#
#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#COPY --from=build /app ./
#
#EXPOSE 80
#
#ENTRYPOINT ["dotnet", "FilmoSearchPortal.API.dll"]

#FROM microsoft/aspnetcore:7.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM microsoft/aspnetcore-build:7.0 AS build
#WORKDIR /src
#
#COPY FilmoSearchPortal.API/FilmoSearchPortal.API.csproj FilmoSearchPortal.API/
#RUN dotnet restore FilmoSearchPortal.API/FilmoSearchPortal.API.csproj
#COPY . .
#WORKDIR /src/FilmoSearchPortal.API
#RUN dotnet buildFilmoSearchPortal.API.csproj -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish FilmoSearchPortal.API.csproj -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "FilmoSearchPortal.API.dll"]

# Use an official ASP.NET Core runtime as a base image
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


