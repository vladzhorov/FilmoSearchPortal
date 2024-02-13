
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src


COPY FilmoSearchPortal.API/*.csproj FilmoSearchPortal.API/
RUN dotnet restore FilmoSearchPortal.API/FilmoSearchPortal.API.csproj


COPY . .


WORKDIR "/src/FilmoSearchPortal.API"  
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FilmoSearchPortal.API.dll"]


