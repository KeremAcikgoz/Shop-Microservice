FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shop.Order/Shop.Order.csproj", "Shop.Order/"]
RUN dotnet restore "Shop.Order/Shop.Order.csproj"
COPY . .
WORKDIR "/src/Shop.Order"
RUN dotnet build "Shop.Order.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.Order.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Order.dll"]
