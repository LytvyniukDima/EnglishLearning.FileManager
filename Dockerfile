FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG NUGET_PASS

COPY . /app
WORKDIR /app/src/EnglishLearning.FileManager.Host
RUN dotnet nuget update source github -u LytvyniukDima -p $NUGET_PASS --store-password-in-clear-text
RUN dotnet publish -c Release -o /app/output

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
COPY --from=build /app/output /app/host
WORKDIR /app/host

ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_URLS="http://*:8900"

ENTRYPOINT ["dotnet", "EnglishLearning.FileManager.Host.dll"]
