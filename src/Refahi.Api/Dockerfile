
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY src/Refahi.Api/Refahi.Api.csproj ./src/Refahi.Api/
RUN dotnet restore ./src/Refahi.Api/Refahi.Api.csproj

COPY . .
WORKDIR /app/src/Refahi.Api
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000

ENTRYPOINT ["dotnet", "Refahi.Api.dll"]
