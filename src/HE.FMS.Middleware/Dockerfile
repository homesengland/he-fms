FROM mcr.microsoft.com/dotnet/sdk:8.0 AS installer-env

#WORKDIR ./src

COPY ["HE.FMS.Middleware", "src/HE.FMS.Middleware/"]
COPY ["HE.FMS.Middleware.BusinessLogic", "src/HE.FMS.Middleware.BusinessLogic/"]
COPY ["HE.FMS.Middleware.Common", "src/HE.FMS.Middleware.Common/"]
COPY ["HE.FMS.Middleware.Contract", "src/HE.FMS.Middleware.Contract/"]
COPY ["HE.FMS.Middleware.Providers", "src/HE.FMS.Middleware.Providers/"]

RUN dotnet restore "src/HE.FMS.Middleware/HE.FMS.Middleware.csproj"

COPY . /src/dotnet-function-app
WORKDIR /src/dotnet-function-app

RUN mkdir -p /home/site/wwwroot

RUN cd ..
RUN ls -la


RUN dotnet publish src/HE.FMS.Middleware/HE.FMS.Middleware.csproj -c Release -o /home/site/wwwroot

RUN groupadd -g 999 appuser && \
    useradd -r -u 999 -g appuser appuser
USER appuser

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

EXPOSE 8080
EXPOSE 4443
ENV ASPNETCORE_URLS=http://*:8080

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]
