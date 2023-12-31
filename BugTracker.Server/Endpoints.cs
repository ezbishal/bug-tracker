using BugTracker.Server.Authentication;
using BugTracker.Server.Features.Projects;

public static class Endpoints
{
	public static WebApplication MapEndpoints(this WebApplication app)
	{

		app.MapGroup("/api/user").WithTags("Authentication")
			.MapRegisterUserEndpoint()
			.MapGetTokenEndpoint();

		app.MapGroup("/api/projects").WithTags("Projects")
			.MapGetAllProjectsEndpoint()
			.MapGetProjectByIdEndpoint()
			.MapCreateProjectEndpoint()
			.MapUpdateProjectEndpoint()
			.MapDeleteProjectEndpoint();

		return app;
	}
}