FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80

WORKDIR /src
COPY *.csproj KimeAit.Api/
RUN dotnet restore KimeAit.Api/*.csproj

WORKDIR /src/KimeAit.Api
COPY . .
RUN dotnet publish /src/KimeAit.Api/*.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "KimeAit.Api.dll"]