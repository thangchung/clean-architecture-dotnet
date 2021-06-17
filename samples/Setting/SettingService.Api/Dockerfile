FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["samples/Setting/SettingService.Api/SettingService.Api.csproj", "SettingService.Api/"]
RUN dotnet restore "samples/Setting/SettingService.Api/SettingService.Api.csproj"
COPY . .
WORKDIR "/src/SettingService.Api"
RUN dotnet build "SettingService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SettingService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SettingService.Api.dll"]
