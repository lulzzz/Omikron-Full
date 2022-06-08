FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Vault/Omikron.VaultService/Omikron.VaultService.csproj", "Services/Vault/Omikron.VaultService/"]
RUN dotnet restore "Services/Vault/Omikron.VaultService/Omikron.VaultService.csproj"
COPY . .
WORKDIR "/src/Services/Vault/Omikron.VaultService"
RUN dotnet build "Omikron.VaultService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Omikron.VaultService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
RUN chown -R 1001:1001 /app
ENTRYPOINT ["dotnet", "Omikron.VaultService.dll"]