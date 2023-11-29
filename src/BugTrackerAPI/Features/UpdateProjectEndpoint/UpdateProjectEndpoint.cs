using AutoMapper;
using BugTrackerApi.Models.Projects;
using BugTrackerAPI.Data;

namespace BugTrackerApi.Features.UpdateProject;

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
    public static async Task<IResult> UpdateProject(UpdateProjectDto updateProjectDto,
    ApplicationDbContext dbContext, IMapper mapper)
    {
        try
        {
            var project = mapper.Map<ProjectModel>(updateProjectDto);
            var projectToUpdate = dbContext.Projects.FirstOrDefault(p => p.Id == project.Id);
            projectToUpdate.Name = project.Name;
            dbContext.SaveChanges();

            var projectToSend = mapper.Map<GetProjectDto>(project);
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
