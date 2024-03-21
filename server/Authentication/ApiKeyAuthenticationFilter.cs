
namespace Server.Authentication;

public class ApiKeyAuthenticationFilter : IEndpointFilter
{
    private const string ApiKeyHeaderName = "X-Api-Key";
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        string? apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];

        if (IsApiKeyValid(apiKey))
            return await next(context);

        return Results.Unauthorized();
    }

    private static bool IsApiKeyValid(string? apiKey) => !string.IsNullOrWhiteSpace(apiKey);
}
