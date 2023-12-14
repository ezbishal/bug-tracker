using Microsoft.FluentUI.AspNetCore.Components;

namespace BugTracker.Client;

public static class Registry
{
    public static IServiceCollection ConfigureBlazor(this IServiceCollection services)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        services.AddFluentUIComponents();

        return services;
    }
}
