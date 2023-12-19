using AutoMapper;
using BugTracker.Server.Data;
using BugTracker.Server.Helpers;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Features.Projects;

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
    public static async Task<IResult> UpdateProject(ProjectModel updateProjectModel,
    ApplicationDbContext dbContext, IMapper mapper)
    {
        try
        {
            var projectModel = mapper.Map<ProjectModel>(updateProjectModel);
            var projectModelToUpdate = dbContext.Projects.FirstOrDefault(p => p.Id == projectModel.Id);

            projectModelToUpdate.Name = projectModel.Name;

            dbContext.SaveChanges();

            var getProjectModel = new GetProjectModel().Map(projectModelToUpdate);

            return Results.CreatedAtRoute(
                routeName: "GetProjectById",
                routeValues: new { projectModelToUpdate.Id },
                value: getProjectModel
            );
        }
        catch (Exception ex)
        {
            return Results.StatusCode(500);
        }
    }
}
