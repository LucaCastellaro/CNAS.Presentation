using CNAS.Presentation.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CNAS.Presentation.Extensions;

/// <summary>
/// Extension methods to use Endpoints in your program.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Register the Endpoints definitions.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <returns>The <typeparamref name="WebApplication" /> so that other methods can be chained.</returns>
    public static WebApplication UseEndpointDefinitions(this WebApplication app)
    {
        var endpointsDefinitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

        foreach (var definition in endpointsDefinitions)
        {
            definition.MapEndpoints(app);
        }

        return app;
    }
}