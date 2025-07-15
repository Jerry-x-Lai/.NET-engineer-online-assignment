FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CryptoInfoApi.csproj", "."]
RUN dotnet restore "./CryptoInfoApi.csproj"
COPY . .
RUN dotnet publish "CryptoInfoApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CryptoInfoApi.dll"]
