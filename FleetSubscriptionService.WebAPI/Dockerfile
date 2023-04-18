FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FleetSubscriptionService.WebAPI/FleetSubscriptionService.WebAPI.csproj", "FleetSubscriptionService.WebAPI/"]
RUN dotnet restore "FleetSubscriptionService.WebAPI/FleetSubscriptionService.WebAPI.csproj"
COPY . .
WORKDIR "/src/FleetSubscriptionService.WebAPI"
RUN dotnet build "FleetSubscriptionService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetSubscriptionService.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetSubscriptionService.WebAPI.dll"]
