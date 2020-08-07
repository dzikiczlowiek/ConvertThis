FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ConvertThis.WebApi/ConvertThis.WebApi.csproj", "ConvertThis.WebApi/"]
RUN dotnet restore "ConvertThis.WebApi/ConvertThis.WebApi.csproj"
COPY . .
WORKDIR "/src/ConvertThis.WebApi"
RUN dotnet build "ConvertThis.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConvertThis.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConvertThis.WebApi.dll"]
