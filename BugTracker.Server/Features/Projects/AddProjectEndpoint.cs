using AutoMapper;
using BugTracker.Server.Data;
using BugTracker.Server.Helpers;
using BugTracker.Server.Models;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Features.Projects
{
    public static class AddProjectEndpoint
    {
        public static RouteGroupBuilder MapAddProjectEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapPost("", AddProject)
                .WithName(nameof(AddProject))
                .WithOpenApi()
                .AddEndpointFilter<ValidationFilter<AddProjectModel>>();

            return builder;
        }

        /// <summary>
        /// Create a new project
        /// </summary>
        public static async Task<IResult> AddProject(AddProjectModel addProjectRequest,
             ApplicationDbContext dbContext, IMapper mapper)
        {
            try
            {
                var project = mapper.Map<ProjectModel>(addProjectRequest);
                dbContext.Projects.Add(project);
                dbContext.SaveChanges();
                var projectToSend = mapper.Map<GetProjectModel>(project);

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
