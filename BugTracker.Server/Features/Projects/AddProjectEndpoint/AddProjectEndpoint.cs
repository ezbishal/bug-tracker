using AutoMapper;
using BugTracker.Server.Data;
using BugTracker.Server.Features.Projects.GetProjectByIdEndpoint;
using BugTracker.Server.Helpers;
using BugTracker.Server.Models;

namespace BugTracker.Server.Features.Projects.AddProjectEndpoint
{
	public static class AddProjectEndpoint
	{
		public static RouteGroupBuilder MapAddProjectEndpoint(this RouteGroupBuilder builder)
		{
			builder.MapPost("", AddProject)
				.WithName(nameof(AddProject))
				.WithOpenApi()
				.AddEndpointFilter<ValidationFilter<AddProjectRequest>>();

			return builder;
		}

		/// <summary>
		/// Create a new project
		/// </summary>
		public static async Task<IResult> AddProject(AddProjectRequest addProjectRequest,
			 ApplicationDbContext dbContext, IMapper mapper)
		{
			try
			{
				var project = mapper.Map<ProjectModel>(addProjectRequest);
				dbContext.Projects.Add(project);
				dbContext.SaveChanges();
				var projectToSend = mapper.Map<GetProjectByIdResponse>(project);
				
				return Results.CreatedAtRoute(
					routeName: "GetProjectById",
					routeValues: new { id = project.Id },
					value: projectToSend
				);
				
				
			}
			catch (Exception ex)
			{
				return Results.StatusCode(500);
			}
		}
	}
}
