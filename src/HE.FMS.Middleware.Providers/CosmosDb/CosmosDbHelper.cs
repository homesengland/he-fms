using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Exceptions.Internal;
using Microsoft.Extensions.Configuration;

namespace HE.FMS.Middleware.Providers.CosmosDb;
public class CosmosDbHelper
{
    private const string PartitonKeySetting = "";

    private readonly IConfiguration _configuration;

    public CosmosDbHelper(IConfiguration configuration)
    {
        _configuration = configuration;

        if (string.IsNullOrWhiteSpace(_configuration[PartitonKeySetting]))
        {
            throw new MissingConfigurationException(nameof(PartitonKeySetting));
        }
    }

    public CosmosDbItem CreateCosmosDbItem(object value, string idempotencyKey)
    {
        return new CosmosDbItem()
        {
            Id = Guid.NewGuid().ToString(),
            PartitionKey = _configuration[PartitonKeySetting] ?? string.Empty,
            IdempotencyKey = idempotencyKey,
            CreationTime = DateTime.Now,
            Value = value,
        };
    }
}
