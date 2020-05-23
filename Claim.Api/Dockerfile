FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Claim.Api/Claim.Api.csproj", "Claim.Api/"]
RUN dotnet restore "Claim.Api/Claim.Api.csproj"
COPY . .
WORKDIR "/src/Claim.Api"
RUN dotnet build "Claim.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Claim.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Claim.Api.dll"]
