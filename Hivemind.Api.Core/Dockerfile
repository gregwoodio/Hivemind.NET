FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY . ./Hivemind.Api.Core
WORKDIR /src/Hivemind.Api.Core
RUN dotnet restore ./Hivemind.Api.Core/Hivemind.Api.Core.csproj
RUN dotnet build ./Hivemind.Api.Core/Hivemind.Api.Core.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app ./Hivemind.Api.Core/Hivemind.Api.Core.csproj

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Hivemind.Api.Core.dll"]
EXPOSE 80
