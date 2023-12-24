using AutoMapper;
using BugTracker.Server.Data;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Features.Projects;

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
        ApplicationDbContext dbContext, IMapper mapper)
    {
        var projectModel = dbContext.Projects.ToList().FirstOrDefault(p => p.Id == Id);
        var getProjectModel = mapper.Map<GetProjectModel>(projectModel);
        return Results.Ok(getProjectModel);
    }
}
