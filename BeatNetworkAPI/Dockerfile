FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BeatNetworkAPI/BeatNetworkAPI.csproj", "BeatNetworkAPI/"]
RUN dotnet restore "BeatNetworkAPI/BeatNetworkAPI.csproj"
COPY . .
WORKDIR "/src/BeatNetworkAPI"
RUN dotnet build "BeatNetworkAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeatNetworkAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeatNetworkAPI.dll"]
