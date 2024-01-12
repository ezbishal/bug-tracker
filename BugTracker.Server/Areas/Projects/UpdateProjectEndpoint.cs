using BugTracker.Server.Data;
using BugTracker.Server.Helpers;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Areas.Projects;

public static class UpdateProjectEndpoint
{
    public static RouteGroupBuilder MapUpdateProjectEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPut("/{Id}", UpdateProject)
            .WithName(nameof(UpdateProject))
            .WithOpenApi()
            .AddEndpointFilter<ValidationFilter<ProjectModel>>();

        return builder;
    }

    /// <summary>
    /// Update project
    /// </summary>
    public static async Task<IResult> UpdateProject(ProjectModel projectModel,
    ApplicationDbContext dbContext)
    {
        try
        {
            var projectModelToUpdate = dbContext.Projects.FirstOrDefault(p => p.Id == projectModel.Id);

            projectModelToUpdate.Name = projectModel.Name;

            dbContext.SaveChanges();

            return Results.CreatedAtRoute(
                routeName: "GetProjectById",
                routeValues: new { projectModelToUpdate.Id },
                value: projectModel
            );
        }
        catch (Exception ex)
        {
            return Results.StatusCode(500);
        }
    }
}
