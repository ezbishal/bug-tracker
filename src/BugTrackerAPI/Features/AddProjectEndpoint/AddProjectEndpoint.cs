using AutoMapper;
using BugTrackerApi.Data;
using BugTrackerApi.Features.AddProject;
using BugTrackerApi.Features.GetProjectById;
using BugTrackerApi.Models.Projects;

namespace BugTrackerApi.Features.AddProjectEndpoint
{
    public static class AddProjectEndpoint
    {
        public static RouteGroupBuilder MapAddProjectEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapPost("", AddProject)
                .WithName(nameof(AddProject))
                .WithOpenApi();

            return builder;
        }

        /// <summary>
        /// Create new project
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
