FROM mcr.microsoft.com/dotnet/sdk:8.0.401 AS build-env

COPY ["HE.FMS.Middleware.Claims/","app/HE.FMS.Middleware.Claims/"]
COPY ["HE.FMS.Middleware.Shared/","app/HE.FMS.Middleware.Shared/"]
COPY ["HE.FMS.Middleware.BusinessLogic/","app/HE.FMS.Middleware.BusinessLogic/"]
COPY ["HE.FMS.Middleware.Common/","app/HE.FMS.Middleware.Common/"]
COPY ["HE.FMS.Middleware.Contract/","app/HE.FMS.Middleware.Contract/"]
COPY ["HE.FMS.Middleware.Providers/","app/HE.FMS.Middleware.Providers/"]
COPY ["nuget.config", "/app"]
COPY ["Directory.Build.props", "/app"]
COPY [".editorconfig", "/app"]

RUN dotnet restore "app/HE.FMS.Middleware.Claims/HE.FMS.Middleware.Claims.csproj"

RUN mkdir -p /home/site/wwwroot
RUN dotnet publish app/HE.FMS.Middleware.Claims/HE.FMS.Middleware.Claims.csproj -c Release -o /home/site/wwwroot

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


# FROM mcr.microsoft.com/dotnet/sdk:8.0.401 AS build-env

# COPY ["HE.FMS.Middleware.Claims/HE.FMS.Middleware.Claims.csproj","app/HE.FMS.Middleware.Claims/"]
# COPY ["HE.FMS.Middleware.Shared/HE.FMS.Middleware.Shared.csproj","app/HE.FMS.Middleware.Shared/"]
# COPY ["HE.FMS.Middleware.BusinessLogic/HE.FMS.Middleware.BusinessLogic.csproj","app/HE.FMS.Middleware.BusinessLogic/"]
# COPY ["HE.FMS.Middleware.Common/HE.FMS.Middleware.Common.csproj","app/HE.FMS.Middleware.Common/"]
# COPY ["HE.FMS.Middleware.Contract/HE.FMS.Middleware.Contract.csproj","app/HE.FMS.Middleware.Contract/"]
# COPY ["HE.FMS.Middleware.Providers/HE.FMS.Middleware.Providers.csproj","app/HE.FMS.Middleware.Providers/"]
# COPY ["nuget.config", "/app"]
# COPY ["Directory.Build.props", "/app"]
# COPY [".editorconfig", "/app"]

# RUN dotnet restore "app/HE.FMS.Middleware.Claims/HE.FMS.Middleware.Claims.csproj"

# #COPY . /src/dotnet-funct-app
# #RUN cd /src/dotnet-function-app && \
# RUN mkdir -p /home/site/wwwroot
# RUN dotnet publish app/HE.FMS.Middleware.Claims/HE.FMS.Middleware.Claims.csproj -c Release -o /home/site/wwwroot

# RUN groupadd -g 999 appuser && \
#  useradd -r -u 999 -g appuser appuser
# USER appuser

# FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0-appservice
# ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
#     AzureFunctionsJobHost__Logging__Console__IsEnabled=true

# EXPOSE 8080
# EXPOSE 4443
# ENV ASPNETCORE_URLS=http://*:8080

# COPY --from=build-env ["/home/site/wwwroot", "/home/site/wwwroot"]
