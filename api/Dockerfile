FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY ["src/OmoqoTest.Api/OmoqoTest.Api.csproj", "OmoqoTest.Api/"]
COPY ["src/OmoqoTest.Application/OmoqoTest.Application.csproj", "OmoqoTest.Application/"]
COPY ["src/OmoqoTest.Contracts/OmoqoTest.Contracts.csproj", "OmoqoTest.Contracts/"]
COPY ["src/OmoqoTest.Domain/OmoqoTest.Domain.csproj", "OmoqoTest.Domain/"]
COPY ["src/OmoqoTest.Infrastructure/OmoqoTest.Infrastructure.csproj", "OmoqoTest.Api/"]
COPY ["tests/OmoqoTest.UnitTests/OmoqoTest.UnitTests.csproj", "OmoqoTest.UnitTests/"]
RUN dotnet restore "OmoqoTest.Api/OmoqoTest.Api.csproj"

COPY . .
WORKDIR "/tests/OmoqoTest.UnitTests"
RUN dotnet test --logger:trx

WORKDIR "/src/OmoqoTest.Api"
RUN dotnet build "OmoqoTest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OmoqoTest.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OmoqoTest.Api.dll"]