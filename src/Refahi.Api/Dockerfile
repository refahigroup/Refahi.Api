FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /api

COPY . .

WORKDIR /app/src/Refahi.Api
RUN dotnet restore

RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /api
COPY --from=build /api/publish .
EXPOSE 5000

ENTRYPOINT ["dotnet", "Refahi.Api.dll"]
