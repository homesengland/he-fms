namespace HE.FMS.Middleware.Providers.CosmosDb;

public interface ICosmosDbClient
{
    Task UpsertItem<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : ICosmosDbItem;
}
