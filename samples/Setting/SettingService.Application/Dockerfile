#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["samples/Setting/SettingService.Application/SettingService.Application.csproj", "samples/Setting/SettingService.Application/"]
COPY ["src/N8T.Infrastructure/N8T.Infrastructure.csproj", "src/N8T.Infrastructure/"]
COPY ["src/N8T.Core/N8T.Core.csproj", "src/N8T.Core/"]
COPY ["samples/DataContracts/CoolStore.AppContracts/CoolStore.AppContracts.csproj", "samples/DataContracts/CoolStore.AppContracts/"]
COPY ["samples/Customer/CustomerService.Core/CustomerService.Core.csproj", "samples/Customer/CustomerService.Core/"]
COPY ["samples/DataContracts/CoolStore.IntegrationEvents/CoolStore.IntegrationEvents.csproj", "samples/DataContracts/CoolStore.IntegrationEvents/"]
COPY ["src/N8T.Infrastructure.OTel/N8T.Infrastructure.OTel.csproj", "src/N8T.Infrastructure.OTel/"]
COPY ["samples/Product/ProductService.Core/ProductService.Core.csproj", "samples/Product/ProductService.Core/"]
COPY ["samples/Setting/SettingService.Core/SettingService.Core.csproj", "samples/Setting/SettingService.Core/"]
COPY ["src/N8T.Infrastructure.EfCore/N8T.Infrastructure.EfCore.csproj", "src/N8T.Infrastructure.EfCore/"]
COPY ["samples/Setting/SettingService.Infrastructure/SettingService.Infrastructure.csproj", "samples/Setting/SettingService.Infrastructure/"]
RUN dotnet restore "samples/Setting/SettingService.Application/SettingService.Application.csproj"
COPY . .
WORKDIR "/src/samples/Setting/SettingService.Application"
RUN dotnet build "SettingService.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SettingService.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SettingService.Application.dll"]
