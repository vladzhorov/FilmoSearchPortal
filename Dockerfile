#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FilmoSearchPortal.API/FilmoSearchPortal.API.csproj", "FilmoSearchPortal.API/"]
COPY ["FilmoSearchPortal.BLL/FilmoSearchPortal.BLL.csproj", "FilmoSearchPortal.BLL/"]
COPY ["FilmoSearchPortal.DAL/FilmoSearchPortal.DAL.csproj", "FilmoSearchPortal.DAL/"]
RUN dotnet restore "./FilmoSearchPortal.API/FilmoSearchPortal.API.csproj" --disable-parallel
RUN dotnet publish "./FilmoSearchPortal.API/FilmoSearchPortal.API.csproj" -c release -o /app --no-restore


#Server Stage

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80

ENTRYPOINT ["dotnet", "FilmoSearchPortal.API.dll"]

