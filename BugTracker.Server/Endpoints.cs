using BugTracker.Server.Authentication.GetAuthTokenEndpoint;
using BugTracker.Server.Authentication.RegisterUserEndpoint;
using BugTracker.Server.Features.Projects.AddProjectEndpoint;
using BugTracker.Server.Features.Projects.DeleteProjectEndpoint;
using BugTracker.Server.Features.Projects.GetAllProjectsEndpoint;
using BugTracker.Server.Features.Projects.GetProjectByIdEndpoint;
using BugTracker.Server.Features.Projects.UpdateProjectEndpoint;

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
			.MapAddProjectEndpoint()
			.MapUpdateProjectEndpoint()
			.MapDeleteProjectEndpoint();

		return app;
	}
}