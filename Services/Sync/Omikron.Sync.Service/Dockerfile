FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:8081
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Sync/Omikron.Sync.Service/Omikron.Sync.Service.csproj", "Services/Sync/Omikron.Sync.Service/"]
RUN dotnet restore "Services/Sync/Omikron.Sync.Service/Omikron.Sync.Service.csproj"
COPY . .
WORKDIR "/src/Services/Sync/Omikron.Sync.Service"
RUN dotnet build "Omikron.Sync.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Omikron.Sync.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
RUN chown -R 1001:1001 /app
ENTRYPOINT ["dotnet", "Omikron.Sync.Service.dll"]