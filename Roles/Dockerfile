FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Roles/Roles.csproj", "Roles2/"]
RUN dotnet restore "Roles/Roles.csproj"
COPY . .
WORKDIR "/src/Roles2"
RUN dotnet build "Roles.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Roles.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Roles.dll"]
