#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Product/ProductService.Application/ProductService.Application.csproj", "src/Product/ProductService.Application/"]
COPY ["src/_BuildingBlocks/N8T.Infrastructure.EfCore/N8T.Infrastructure.EfCore.csproj", "src/_BuildingBlocks/N8T.Infrastructure.EfCore/"]
COPY ["src/_BuildingBlocks/N8T.Infrastructure/N8T.Infrastructure.csproj", "src/_BuildingBlocks/N8T.Infrastructure/"]
COPY ["src/_BuildingBlocks/N8T.Core/N8T.Core.csproj", "src/_BuildingBlocks/N8T.Core/"]
COPY ["src/_DataContracts/CoolStore.IntegrationEvents/CoolStore.IntegrationEvents.csproj", "src/_DataContracts/CoolStore.IntegrationEvents/"]
COPY ["src/Product/ProductService.Core/ProductService.Core.csproj", "src/Product/ProductService.Core/"]
COPY ["src/_DataContracts/CoolStore.AppContracts/CoolStore.AppContracts.csproj", "src/_DataContracts/CoolStore.AppContracts/"]
COPY ["src/Customer/CustomerService.Core/CustomerService.Core.csproj", "src/Customer/CustomerService.Core/"]
COPY ["src/Setting/SettingService.Core/SettingService.Core.csproj", "src/Setting/SettingService.Core/"]
COPY ["src/_BuildingBlocks/N8T.Infrastructure.OTel/N8T.Infrastructure.OTel.csproj", "src/_BuildingBlocks/N8T.Infrastructure.OTel/"]
COPY ["src/Product/ProductService.Infrastructure/ProductService.Infrastructure.csproj", "src/Product/ProductService.Infrastructure/"]
RUN dotnet restore "src/Product/ProductService.Application/ProductService.Application.csproj"
COPY . .
WORKDIR "/src/src/Product/ProductService.Application"
RUN dotnet build "ProductService.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductService.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.Application.dll"]
