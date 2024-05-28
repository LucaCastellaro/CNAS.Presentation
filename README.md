# CNAS.Presentation

## Who
I am a software engineer and I like building custom solutions that fit my needs.

But as they are **not** professional solutions, i manually include my libraries over and over.

This is not only in conflict with the DRY principle, but also a waste of time.

## Why
To solve this *problem* i'm creating a set of libraries:
1. CNAS.Presentation
2. CNAS.Business
3. CNAS.Repository

## What
CNAS is an achronym for **C**lean .**N**ET **A**PI **S**ervice.

With the Presentation library you can create a clean presentation layer, using minimal APIs.

The idea is to create a nuget package for this library, to make it fast and easy to include in new projects.

The nuget package will be publicly deployed on the nuget store.

## How

### Install
1. Search for "CNAS.Presentation" in the nuget store
2. Install said nuget

### Usage
Define your endpoints - UserEndpoints.cs:
``` c#
using CNAS.Presentation.Abstractions;
using CNAS.Presentation.Extensions;

namespace My.Apis;

public interface IUserEndpoints : IEndpointDefinition
{
    Task<bool> Get(CancellationToken ct = default);
}

public sealed class UserEndpoints : IUserEndpoints
{
    public void MapEndpoints(WebApplication app)
    {
        var group = app.CreateGroup(nameof(UserEndpoints));

        group.MapGet("", Get).WithOpenApi();
    }

    public Task<bool> Get(CancellationToken ct = default)
    {
        return Task.FromResult(true);
    }
}
```

In your Program.cs:
``` c#
using CNAS.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Some code...

builder.Services.AddEndpointDefinitions(typeof(Program));

// Some more code...

var app = builder.Build();

// Even more code...

app.UseEndpointDefinitions();

app.Run();
```
You should now be able to call this api: `curl -X GET https://localhost:7095/api/user`.
The response should be: `true`.