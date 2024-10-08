# FMS Integration Platform

## Developers' flow diagram

[LucidChart Here](https://lucid.app/lucidchart/521bf20d-9052-4a0c-9b8a-7c1e8931395a/edit?beaconFlowId=E265A244F18B3E1A&invitationId=inv_0b60e453-bca5-431b-9c2a-d1e89abaa78d&page=mXwzAnnOLpm1)

## Durable functions
Solution uses durable function for business process orchestration.

⚠️ Do not update `Microsoft.Azure.Functions.Worker.Sdk` package to version `1.16.0` or higher, it will break durable functions because of [this bug](https://github.com/microsoft/durabletask-dotnet/issues/247).

## Local Development
When running Azure Function in Rider it must be started with elevated permissions.

This project integrates with Azure resources using `DefaultAzureCredential`. To authenticate with Azure resources create Service Principal and set following environment variables:
 - AZURE_TENANT_ID
 - AZURE_CLIENT_ID
 - AZURE_CLIENT_SECRET
Alternative way is Azure CLI login. Following roles are required to be granted for Service Principal:
 - Key Vault
     - Key Vault Secrets Officer
 - Service Bus
     - Azure Service Bus Data Sender
     - Azure Service Bus Data Receiver

### Prerequisites
- **Azurite**
- **Service Bus** in Standard Tier
    - Create `poc` topic and `push-message-to-cosmosdb` subscription
- **Cosmos DB** insance or [emulator](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator).
    - Start docker container emulator with command ```docker run -p 8081:8081 -p 10250:10250 -p 10251:10251 -p 10252:10252 -p 10253:10253 -p 10254:10254 -p 10255:10255 --env AZURE_COSMOS_EMULATOR_ENABLE_DATA_PERSISTENCE=1 --env AZURE_COSMOS_EMULATOR_IP_ADDRESS_OVERRIDE=127.0.0.1 --name cosmos-emulator --pull missing -t -i mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator:latest```
    - Create `fms` database
    - Create `fms-container` container
    - Create `/partitionKey` partition

### Configuration
Configuration for local development is stored in `local.settings.json`. It should contain flat structure of keys, nested values should be split using `:`;
- `ServiceBus:Connection` connection details to Service Bus, use following configuration keys for local development:
    - `ServiceBus:Connection__fullyQualifiedNamespace`
    - `ServiceBus:Connection__tenantId`
    - `ServiceBus:Connection__clientId`
    - `ServiceBus:Connection__clientSecret`
- `ServiceBus:PushMessageToCosmosDb:Topic` PoC topic name which will forward message to Cosmos DB
- `ServiceBus:PushMessageToCosmosDb:Subscription` PoC subscription name which will forward message to Cosmos DB
- `CosmosDb:AccountEndpoint` provided on Azure environment, uses Managed Identity authorization
- `CosmosDb:ConnectionString` provide full connection string for local development using Cosmos DB emulator
- `CosmosDb:DatabaseId` id of the database `fms`
- `CosmosDb:ContainerId` if of the database container `fms-container`
- `Mambu:RotateApiKeyTimeTrigger` cron expression for rotating Mambu API key
- `Mambu:Api:BaseUrl` base url to Mambu API
- `Mambu:Api:RetryCount` number of retries for Mambu API calls
- `Mambu:Api:RetryDelayInMilliseconds` delay between retries for Mambu API calls
- `Mambu:ApiKey:KeyVaultValueName` secret name which stores ApiKey in Azure Key Vault
- `Mambu:ApiKey:ExpirationInSeconds` expiration in seconds of Mambu API key which is used for rotating
- `KeyVault:Url` url to Azure Key Vault
- `Grants:BranchId` id of Mambu branch (environment)
- `Grants:OpenGrantAccount:TopicName` service bus topic name for creating grant account
- `Grants:OpenGrantAccount:SubscriptionName` service bus subscription name for creating grant account

### Docker
To build docker image locally run the following command on the main repository folder:
`docker build -t middleware:latest -f src\HE.FMS.Middleware\Dockerfile .`
