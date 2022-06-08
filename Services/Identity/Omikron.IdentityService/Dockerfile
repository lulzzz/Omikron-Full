FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Identity/Omikron.IdentityService/Omikron.IdentityService.csproj", "Services/Identity/Omikron.IdentityService/"]
RUN dotnet restore "Services/Identity/Omikron.IdentityService/Omikron.IdentityService.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Omikron.IdentityService"
RUN dotnet build "Omikron.IdentityService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Omikron.IdentityService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
RUN chown -R 1001:1001 /app
ENTRYPOINT ["dotnet", "Omikron.IdentityService.dll"]