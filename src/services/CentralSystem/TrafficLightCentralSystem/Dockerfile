#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["services/CentralSystem/TrafficLightCentralSystem/TrafficLightCentralSystem.csproj", "services/CentralSystem/TrafficLightCentralSystem/"]
RUN dotnet restore "services/CentralSystem/TrafficLightCentralSystem/TrafficLightCentralSystem.csproj"
COPY . .
WORKDIR "/src/services/CentralSystem/TrafficLightCentralSystem"
RUN dotnet build "TrafficLightCentralSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrafficLightCentralSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrafficLightCentralSystem.dll"]
