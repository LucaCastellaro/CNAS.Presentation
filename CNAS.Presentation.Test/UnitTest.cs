using CNAS.Presentation.Abstractions;
using CNAS.Presentation.Extensions;
using CNAS.Presentation.Test.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CNAS.Presentation.Test;

public sealed class UnitTest
{
    [Fact]
    public async Task AddAndUseEndpoints()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddEndpointDefinitions(typeof(UnitTest));

        var app = builder.Build();

        app.UseEndpointDefinitions();

        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();
        Assert.Single(definitions);

        var testEndpoint = definitions.First();
        Assert.IsType<TestEndpoint>(testEndpoint);
        Assert.IsAssignableFrom<ITestEndpoint>(testEndpoint);
        Assert.IsAssignableFrom<IEndpointDefinition>(testEndpoint);

        var result = await (testEndpoint as ITestEndpoint)!.Get();
        Assert.True(result);
    }
}