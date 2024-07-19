# FMS Integration Platform

## Local Development
When running Azure Function in Rider it must be started with elevated permissions.

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
- `CosmosDb:PartitionKey` partition key `/partitionKey`