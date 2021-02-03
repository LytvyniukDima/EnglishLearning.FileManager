FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
ARG NUGET_PASS

COPY . /app
WORKDIR /app/src/EnglishLearning.FileManager.Host
RUN dotnet nuget update source github -u LytvyniukDima -p $NUGET_PASS --store-password-in-clear-text
RUN dotnet publish -c Release -o /app/output

WORKDIR /app/src/EnglishLearning.FileManager.SqlMigrations
RUN dotnet publish -c Release -o /app/migrations

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS runtime
COPY --from=build /app/output /app/host
COPY --from=build /app/migrations /app/migrations

ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_URLS="http://*:8900"

ENTRYPOINT ["sh", "-c"]
CMD ["dotnet /app/host/EnglishLearning.FileManager.Host.dll"]
