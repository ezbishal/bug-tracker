using BugTracker.Client.Areas;
using BugTracker.Client.Authentication;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BugTracker.Client;

public static class Registry
{
	public static IServiceCollection ConfigureSharedServices(this IServiceCollection services)
	{
		services.AddFluentUIComponents();
		services.AddHttpClient<ProjectService>();
		services.AddHttpClient<AuthService>();
		return services;
	}
}
