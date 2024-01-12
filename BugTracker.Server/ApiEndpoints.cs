using BugTracker.Server.Areas.Authentication;
using BugTracker.Server.Areas.Projects;

public static class ApiEndpoints
{
	public static WebApplication MapApiEndpoints(this WebApplication app)
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