using AutoMapper;
using BugTrackerAPI.Data;

namespace BugTrackerAPI.Features.GetProjectById;

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
        var project = dbContext.Projects.ToList().FirstOrDefault(p => p.Id == Id);
        var projectDto = mapper.Map<GetProjectByIdResponse>(project);
        return Results.Ok(projectDto);
    }
}
