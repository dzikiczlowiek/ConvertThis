# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["source/WebApi", "WebApi/"]
RUN dotnet restore "WebApi/ConvertThis.WebApi/ConvertThis.WebApi.csproj"
COPY . .

# publish
FROM build AS publish
WORKDIR /src/WebApi/ConvertThis.WebApi
RUN dotnet publish "ConvertThis.WebApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Colors.API.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ConvertThis.WebApi.dll