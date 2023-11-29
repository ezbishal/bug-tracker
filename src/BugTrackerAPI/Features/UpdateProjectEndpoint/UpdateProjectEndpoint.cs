using AutoMapper;
using BugTrackerAPI.Data;
using BugTrackerAPI.Features.GetProjectById;
using BugTrackerAPI.Models.Projects;

namespace BugTrackerAPI.Features.UpdateProject;

public static class UpdateProjectEndpoint
{
    public static RouteGroupBuilder MapUpdateProjectEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPut("/{Id}", UpdateProject)
            .WithName(nameof(UpdateProject))
            .WithOpenApi();

        return builder;
    }

    /// <summary>
    /// Update project
    /// </summary>
    public static async Task<IResult> UpdateProject(UpdateProjectRequest updateProjectDto,
    ApplicationDbContext dbContext, IMapper mapper)
    {
        try
        {
            var project = mapper.Map<ProjectModel>(updateProjectDto);
            var projectToUpdate = dbContext.Projects.FirstOrDefault(p => p.Id == project.Id);
            projectToUpdate.Name = project.Name;
            dbContext.SaveChanges();

            var projectToSend = mapper.Map<GetProjectByIdResponse>(project);
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
