using AutoMapper;
using BugTracker.Server.Data;
using BugTracker.Server.Helpers;

namespace BugTracker.Server.Features.Projects.GetProjectByIdEndpoint;

public static class GetProjectByIdEndpoint
{
    public static RouteGroupBuilder MapGetProjectByIdEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapGet("/{Id}", GetProjectById)
            .WithName(nameof(GetProjectById))
            .WithOpenApi()
            .AddEndpointFilter<ValidationFilter<GetProjectByIdRequest>>();

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
