FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shop.Customer.Service/Shop.Customer.Service.csproj", "Shop.Customer.Service/"]
RUN dotnet restore "Shop.Customer.Service/Shop.Customer.Service.csproj"
COPY . .
WORKDIR "/src/Shop.Customer.Service"
RUN dotnet build "Shop.Customer.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.Customer.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Customer.Service.dll"]
