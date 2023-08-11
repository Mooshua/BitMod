FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["standalone/BitMod.Launcher/BitMod.Launcher.csproj", "standalone/BitMod.Launcher/"]
RUN dotnet restore "standalone/BitMod.Launcher/BitMod.Launcher.csproj"
COPY . .
WORKDIR "/src/standalone/BitMod.Launcher"
RUN dotnet build "BitMod.Launcher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BitMod.Launcher.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BitMod.Launcher.dll"]
