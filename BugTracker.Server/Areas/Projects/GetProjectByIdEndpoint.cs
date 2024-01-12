using BugTracker.Server.Data;

namespace BugTracker.Server.Areas.Projects;

public static class GetProjectByIdEndpoint
{
    public static RouteGroupBuilder MapGetProjectByIdEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapGet("/{Id}", GetProjectById)
            .WithName(nameof(GetProjectById))
            .WithOpenApi();

        return builder;
    }

    /// <summary>
    /// Get specific project
    /// </summary>
    public static async Task<IResult> GetProjectById(int Id,
        ApplicationDbContext dbContext)
    {
        var projectModel = dbContext.Projects.ToList().FirstOrDefault(p => p.Id == Id);
        return Results.Ok(projectModel);
    }
}
