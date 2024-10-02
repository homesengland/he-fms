using System.Text.Json;
using System.Text.Json.Serialization;
using HE.FMS.Middleware.Common.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.Middleware.Common.Config;

public static class CommonModule
{
    public static JsonSerializerOptions CommonSerializerOptions => new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
    };

    public static IServiceCollection AddCommonModule(this IServiceCollection services)
    {
        services.AddSingleton(CommonSerializerOptions);
        services.AddSingleton<IStreamSerializer, StreamSerializer>();
        services.AddSingleton<IObjectSerializer, ObjectSerializer>();

        return services;
    }
}
