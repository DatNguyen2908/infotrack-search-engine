#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Infotrack.Sales.SearchEngine.WebApi/Infotrack.Sales.SearchEngine.WebApi.csproj", "Infotrack.Sales.SearchEngine.WebApi/"]
COPY ["Infotrack.Sales.SearchEngine.Application/Infotrack.Sales.SearchEngine.Application.csproj", "Infotrack.Sales.SearchEngine.Application/"]
COPY ["Infotrack.Sales.SearchEngine.Domain/Infotrack.Sales.SearchEngine.Domain.csproj", "Infotrack.Sales.SearchEngine.Domain/"]
COPY ["Infotrack.Sales.SearchEngine.Contracts/Infotrack.Sales.SearchEngine.Contracts.csproj", "Infotrack.Sales.SearchEngine.Contracts/"]
COPY ["Infotrack.Sales.SearchEngine.EFCore/Infotrack.Sales.SearchEngine.EFCore.csproj", "Infotrack.Sales.SearchEngine.EFCore/"]
COPY ["Infotrack.Sales.SearchEngine.Infrastructure/Infotrack.Sales.SearchEngine.Infrastructure.csproj", "Infotrack.Sales.SearchEngine.Infrastructure/"]
COPY ["Infotrack.Sales.SearchEngine.Constants/Infotrack.Sales.SearchEngine.Constants.csproj", "Infotrack.Sales.SearchEngine.Constants/"]
RUN dotnet restore "./Infotrack.Sales.SearchEngine.WebApi/./Infotrack.Sales.SearchEngine.WebApi.csproj"
COPY . .
WORKDIR "/src/Infotrack.Sales.SearchEngine.WebApi"
RUN dotnet build "./Infotrack.Sales.SearchEngine.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Infotrack.Sales.SearchEngine.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Infotrack.Sales.SearchEngine.WebApi.dll"]