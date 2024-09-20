FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProjectAuthorization/ProjectAuthorization.csproj", "ProjectAuthorization/"]
COPY ["Authorization.Infrastructure.Api/Authorization.Infrastructure.Api.csproj", "Authorization.Infrastructure.Api/"]
COPY ["Authorization.Application/Authorization.Application.csproj", "Authorization.Application/"]
COPY ["Authorization.Infrastructure.Database/Authorization.Infrastructure.Database.csproj", "Authorization.Infrastructure.Database/"]
COPY ["Authorization.YooKassa/Authorization.YooKassa.csproj", "Authorization.YooKassa/"]
RUN dotnet restore "ProjectAuthorization/ProjectAuthorization.csproj"
COPY . .
WORKDIR "/src/ProjectAuthorization"
RUN dotnet build "ProjectAuthorization.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectAuthorization.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY ["ProjectAuthorization/distrib/base.apk", "distrib/"]
COPY ["ProjectAuthorization/distrib/base.exe", "distrib/"]

ENTRYPOINT ["dotnet", "ProjectAuthorization.dll"]
