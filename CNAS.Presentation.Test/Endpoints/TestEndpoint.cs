using CNAS.Presentation.Abstractions;
using CNAS.Presentation.Extensions;
using Microsoft.AspNetCore.Builder;

namespace CNAS.Presentation.Test.Endpoints;

public interface ITestEndpoint : IEndpointDefinition
{
    Task<bool> Get(CancellationToken ct = default);
}

public sealed class TestEndpoint : ITestEndpoint
{
    public void MapEndpoints(WebApplication app)
    {
        var group = app.CreateGroup(nameof(TestEndpoint));

        group.MapGet("", Get);
    }

    public Task<bool> Get(CancellationToken ct = default)
    {
        return Task.FromResult(true);
    }
}