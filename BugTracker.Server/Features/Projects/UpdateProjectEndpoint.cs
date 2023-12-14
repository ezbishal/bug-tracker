using AutoMapper;
using BugTracker.Server.Data;
using BugTracker.Server.Helpers;
using BugTracker.Server.Models;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Features.Projects;

public static class UpdateProjectEndpoint
{
    public static RouteGroupBuilder MapUpdateProjectEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPut("/{Id}", UpdateProject)
            .WithName(nameof(UpdateProject))
            .WithOpenApi()
            .AddEndpointFilter<ValidationFilter<UpdateProjectModel>>();

        return builder;
    }

    /// <summary>
    /// Update project
    /// </summary>
    public static async Task<IResult> UpdateProject(UpdateProjectModel updateProjectDto,
    ApplicationDbContext dbContext, IMapper mapper)
    {
        try
        {
            var project = mapper.Map<ProjectModel>(updateProjectDto);
            var projectToUpdate = dbContext.Projects.FirstOrDefault(p => p.Id == project.Id);
            projectToUpdate.Name = project.Name;
            dbContext.SaveChanges();

            var projectToSend = mapper.Map<GetProjectModel>(project);
            return Results.CreatedAtRoute(
                routeName: "GetProjectById",
                routeValues: new { project.Id },
                value: projectToSend
            );
        }
        catch (Exception ex)
        {
            return Results.StatusCode(500);
        }
    }
}
