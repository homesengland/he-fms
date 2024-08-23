FROM mcr.microsoft.com/dotnet/sdk:8.0 AS installer-env

RUN groupadd -g 999 appuser && \
    useradd -r -u 999 -g appuser appuser
USER appuser

COPY ["HE.FMS.Middleware/HE.FMS.Middleware.csproj", "src/HE.FMS.Middleware/"]
COPY ["HE.FMS.Middleware.BusinessLogic/HE.FMS.Middleware.BusinessLogic.csproj", "src/HE.FMS.Middleware.BusinessLogic/"]
COPY ["HE.FMS.Middleware.Common/HE.FMS.Middleware.Common.csproj", "src/HE.FMS.Middleware.Common/"]
COPY ["HE.FMS.Middleware.Contract/HE.FMS.Middleware.Contract.csproj", "src/HE.FMS.Middleware.Contract/"]
COPY ["HE.FMS.Middleware.Providers/HE.FMS.Middleware.Providers.csproj", "src/HE.FMS.Middleware.Providers/"]

RUN dotnet restore "HE.FMS.Middleware/HE.FMS.Middleware.csproj"

COPY . /src/dotnet-function-app
WORKDIR /src/dotnet-function-app

RUN mkdir -p /home/site/wwwroot && \
    dotnet publish src/HE.FMS.Middleware/HE.FMS.Middleware.csproj -c Release -o /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

EXPOSE 8080
EXPOSE 4443
ENV ASPNETCORE_URLS=http://*:8080

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]
