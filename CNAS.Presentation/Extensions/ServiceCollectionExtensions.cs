using CNAS.Presentation.Abstractions;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;

namespace CNAS.Presentation.Extensions;

/// <summary>
/// Extension methods to add Endpoints in your program.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>Add the Endpoint classes that extend IEndpointDefinition to your program.</summary>
    /// <param name="services">The services.</param>
    /// <param name="scanMarkers">The scan markers.</param>
    /// <returns>The <typeparamref name="IServiceCollection" /> so that other methods can be chained.</returns>
    public static IServiceCollection AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
    {
        services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.PropertyNameCaseInsensitive = true;
        });

        var endpointDefinitions = new List<IEndpointDefinition>();

        foreach (var marker in scanMarkers)
        {
            endpointDefinitions.AddRange(
                marker.Assembly.ExportedTypes
                    .Where(xx => typeof(IEndpointDefinition).IsAssignableFrom(xx)
                                 && !xx.IsAbstract
                                 && !xx.IsInterface)
                    .Select(Activator.CreateInstance)
                    .Cast<IEndpointDefinition>()
                );
        }

        services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);

        return services;
    }
}