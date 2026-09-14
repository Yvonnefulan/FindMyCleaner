FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY FindMyCleaner/FindMyCleanerAPI.csproj FindMyCleaner/
RUN dotnet restore FindMyCleaner/FindMyCleanerAPI.csproj
COPY FindMyCleaner/ FindMyCleaner/
RUN dotnet publish FindMyCleaner/FindMyCleanerAPI.csproj -c Release -o /app/publish --no-restore /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 10000
USER $APP_UID
CMD ["/bin/sh", "-c", "exec dotnet FindMyCleanerAPI.dll --urls http://0.0.0.0:${PORT:-10000}"]
