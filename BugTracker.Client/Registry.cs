using BugTracker.Client.Areas;
using BugTracker.Client.Authentication;

namespace BugTracker.Client;

public static class Registry
{
	public static IServiceCollection ConfigureSharedServices(this IServiceCollection services)
	{
		services.AddHttpClient<ProjectService>();
		services.AddHttpClient<AuthService>();

		return services;
	}
}
