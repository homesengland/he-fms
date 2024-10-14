# FMS Integration Platform
This repository contains source code of Azure Functions that are part of FMS Integration Platform.\
The platform is responsible for integration with external systems and processing data.\
It is built using Azure Functions and following Azure resources: Service Bus, Cosmos DB, File Share.

## Developers' flow diagram

[LucidChart Here](https://lucid.app/lucidchart/521bf20d-9052-4a0c-9b8a-7c1e8931395a/edit?beaconFlowId=E265A244F18B3E1A&invitationId=inv_0b60e453-bca5-431b-9c2a-d1e89abaa78d&page=mXwzAnnOLpm1)

## Local Development

This project integrates with Azure resources using `DefaultAzureCredential`. To authenticate with Azure resources create Service Principal and set following environment variables:
- AZURE_TENANT_ID
- AZURE_CLIENT_ID
- AZURE_CLIENT_SECRET
Alternative way is Azure CLI login. Following roles are required to be granted for Service Principal:
- Service Bus
    - Azure Service Bus Data Sender
    - Azure Service Bus Data Receiver

> When running Azure Function in Rider it must be started with elevated permissions.

### Prerequisites
- **Azurite**
- **Service Bus** in Standard Tier
    - Create topics and subscriptions
        - `claims-topic` and `claims-subscription`
        - `reclaims-topic` and `reclaims-subscription`
- **Cosmos DB** insance or [emulator](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator).
    - Start docker container emulator with command \
    ```docker run -p 8081:8081 -p 10250:10250 -p 10251:10251 -p 10252:10252 -p 10253:10253 -p 10254:10254 -p 10255:10255 --env AZURE_COSMOS_EMULATOR_ENABLE_DATA_PERSISTENCE=1 --env AZURE_COSMOS_EMULATOR_IP_ADDRESS_OVERRIDE=127.0.0.1 --name cosmos-emulator --pull missing -t -i mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator:latest```
    - Create `fms` database
    - Create containers (each container should have configured partition key to `/partitionKey`)
        - container for configuration: `config`
        - container for trace logs: `trace`
        - container for efin's data: `efin`

### Configuration
Configuration for local development is stored in `local.settings.json`. It should contain flat structure of keys, nested values should be split using `:`;

#### Service Bus configuration
- `ServiceBus:Connection` connection details to Service Bus, use following configuration keys for local development:
    - `ServiceBus:Connection__fullyQualifiedNamespace`
    - `ServiceBus:Connection__tenantId`
    - `ServiceBus:Connection__clientId`
    - `ServiceBus:Connection__clientSecret`
- `Claims:Create:TopicName` topic name for creating claims
- `Claims:Create:SubscriptionName` subscription name for creating claims
- `Reclaims:Create:TopicName` topic name for creating reclaims
- `Reclaims:Create:SubscriptionName` subscription name for creating reclaims

#### Cosmos DB configuration
- `CosmosDb:AccountEndpoint` provided on Azure environment, uses Managed Identity authorization
- `CosmosDb:ConnectionString` provides full connection string (cab be used for local development)
- `CosmosDb:DatabaseId` id of the database
- `EfinConfigDb:ContainerId` id of the container for configuration
- `EfinDb:ContainerId` id of the container for efin's data
- `TraceDb:ContainerId` id of the container for trace logs

#### File Share configuration
- `IntegrationStorage:ConnectionString` connection string to Azure Storage Account
- `IntegrationStorage:ShareName` name of the file share

#### Other configuration
- `Claims:Create:CronExpression` cron expression for creating claims
- `Reclaims:Create:CronExpression` cron expression for creating reclaims
- `AllowedEnvironments` list of allowed environments for the middleware

### Docker
Integration platform is hosted on Function Apps in Azure and is containerized using Docker.\
To build docker image locally run the following commands:\
`docker build -t middleware:latest -f HE.FMS.Middleware.Claims\Dockerfile .`\
`docker build -t middleware:latest -f HE.FMS.Middleware.Reclaims\Dockerfile .`\
