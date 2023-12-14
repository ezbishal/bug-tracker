using Microsoft.FluentUI.AspNetCore.Components;

namespace BugTracker.Client;

public static class Registry
{
    public static IServiceCollection ConfigureSharedServices(this IServiceCollection services)
    {
        services.AddFluentUIComponents();

        return services;
    }
}
