using Microsoft.AspNetCore.Builder;

namespace CNAS.Presentation.Abstractions;

/// <summary>
/// Make your Endpoint classes extend this.
/// </summary>
public interface IEndpointDefinition
{
    /// <summary>
    /// Implement this method to map your Endpoints.
    /// </summary>
    /// <param name="app">The web application.</param>
    void MapEndpoints(WebApplication app);
}