using BugTracker.Server.Data;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Areas.Projects
{
    public static class CreateProjectEndpoint
    {
        public static RouteGroupBuilder MapCreateProjectEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapPost("", CreateProject)
                .WithName(nameof(CreateProject))
                .Accepts<ProjectModel>("application/json")
                .WithOpenApi();

            return builder;
        }

        /// <summary>
        /// Create a new project
        /// </summary>
        public static async Task<IResult> CreateProject(ProjectModel projectModel,
             ApplicationDbContext dbContext)
        {
            try
            {
                dbContext.Projects.Add(projectModel);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute(
                    routeName: "GetProjectById",
                    routeValues: new { id = projectModel.Id },
                    value: projectModel
                );


            }
            catch (Exception ex)
            {
                return Results.StatusCode(500);
            }
        }
    }
}
