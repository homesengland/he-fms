FROM mcr.microsoft.com/dotnet/sdk:8.0.401 AS build-env

COPY ["HE.FMS.Middleware", "/app/HE.FMS.Middleware"]
COPY ["HE.FMS.Middleware.BusinessLogic", "/app/HE.FMS.Middleware.BusinessLogic"]
COPY ["HE.FMS.Middleware.Common", "/app/HE.FMS.Middleware.Common"]
COPY ["HE.FMS.Middleware.Contract", "/app/HE.FMS.Middleware.Contract"]
COPY ["HE.FMS.Middleware.Providers", "/app/HE.FMS.Middleware.Providers"]
COPY ["nuget.config", "/app"]
COPY ["Directory.Build.props", "/app"]
COPY [".editorconfig", "/app"]

COPY ["src/HE.FMS.Middleware/HE.FMS.Middleware.csproj","src/HE.FMS.Middleware/"]
COPY ["src/HE.FMS.Middleware.Shared/HE.FMS.Middleware.Shared.csproj","src/HE.FMS.Middleware.Shared/"]
COPY ["src/HE.FMS.Middleware.BusinessLogic/HE.FMS.Middleware.BusinessLogic.csproj","src/HE.FMS.Middleware.BusinessLogic/"]
COPY ["src/HE.FMS.Middleware.Common/HE.FMS.Middleware.Common.csproj","src/HE.FMS.Middleware.Common/"]
COPY ["src/HE.FMS.Middleware.Contract/HE.FMS.Middleware.Contract.csproj","src/HE.FMS.Middleware.Contract/"]
COPY ["src/HE.FMS.Middleware.Providers/HE.FMS.Middleware.Providers.csproj","src/HE.FMS.Middleware.Providers/"]

RUN dotnet restore "/app/HE.FMS.Middleware/HE.FMS.Middleware.csproj" --configfile "/app/nuget.config"

RUN mkdir -p /home/site/wwwroot
#RUN dotnet build app/HE.FMS.Middleware/HE.FMS.Middleware.csproj -c Release --configfile "app/nuget.config"
RUN dotnet publish app/HE.FMS.Middleware/HE.FMS.Middleware.csproj -c Release -o home/site/wwwroot

RUN groupadd -g 999 appuser && \
    useradd -r -u 999 -g appuser appuser
USER appuser

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0-appservice
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

EXPOSE 8080
EXPOSE 4443
ENV ASPNETCORE_URLS=http://*:8080

COPY --from=build-env ["/home/site/wwwroot", "/home/site/wwwroot"]
