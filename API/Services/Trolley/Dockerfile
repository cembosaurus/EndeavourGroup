FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["API/Services/Trolley/Trolley.csproj", "API/Services/Trolley/"]
COPY ["API/Business/Business.csproj", "API/Business/"]
RUN dotnet restore "API/Services/Trolley/Trolley.csproj"
COPY . .
RUN dotnet build "API/Services/Trolley/Trolley.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API/Services/Trolley/Trolley.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trolley.dll", "--environment=Production"]