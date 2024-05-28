using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CNAS.Presentation.Extensions;

/// <summary>
/// Extension methods to create an Endpoints group.
/// </summary>
public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// Creates the Endpoints group.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="className">Name of the class - must have the "Endpoints" suffix (eg. UserEndpoints).</param>
    /// <param name="suffixes">The suffixes.</param>
    /// <returns>The <typeparamref name="RouteGroupBuilder" /> to which add Endpoints.</returns>
    public static RouteGroupBuilder CreateGroup(this IEndpointRouteBuilder builder,
        string className,
        params string[]? suffixes)
    {
        className = className
            .Replace("Endpoints", string.Empty)
            .ToLower();

        var prefix = $"api/{className}";

        if (suffixes is not null)
        {
            suffixes = suffixes.Where(xx => !string.IsNullOrWhiteSpace(xx)).ToArray();
            foreach (var item in suffixes)
            {
                var suffix = item.ToLower();
                className = $"{className} {suffix}";
                var parts = suffix.Split(" ").AsSpan();
                foreach (var part in parts)
                {
                    prefix += $"/{part}";
                }
            }
        }

        return builder.MapGroup(prefix)
            .WithTags(className);
    }
}