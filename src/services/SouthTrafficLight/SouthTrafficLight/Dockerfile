#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["services/SouthTrafficLight/SouthTrafficLight/SouthTrafficLight.csproj", "services/SouthTrafficLight/SouthTrafficLight/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "services/SouthTrafficLight/SouthTrafficLight/SouthTrafficLight.csproj"
COPY . .
WORKDIR "/src/services/SouthTrafficLight/SouthTrafficLight"
RUN dotnet build "SouthTrafficLight.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SouthTrafficLight.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SouthTrafficLight.dll"]