FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["tests/BitMod.Benchmarks/BitMod.Benchmarks.csproj", "tests/BitMod.Benchmarks/"]
RUN dotnet restore "tests/BitMod.Benchmarks/BitMod.Benchmarks.csproj"
COPY . .
WORKDIR "/src/tests/BitMod.Benchmarks"
RUN dotnet build "BitMod.Benchmarks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BitMod.Benchmarks.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BitMod.Benchmarks.dll"]
