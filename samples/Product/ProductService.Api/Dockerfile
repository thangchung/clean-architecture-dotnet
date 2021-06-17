FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["samples/Product/ProductService.Api/ProductService.Api.csproj", "ProductService.Api/"]
RUN dotnet restore "samples/Product/ProductService.Api/ProductService.Api.csproj"
COPY . .
WORKDIR "/src/ProductService.Api"
RUN dotnet build "ProductService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.Api.dll"]
