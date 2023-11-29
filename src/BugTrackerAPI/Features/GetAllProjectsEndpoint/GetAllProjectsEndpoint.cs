using AutoMapper;
using BugTrackerAPI.Data;
using BugTrackerAPI.Features.GetProjectById;
using BugTrackerAPI.ValidationFilter;

namespace BugTrackerAPI.Features.GetAllProjects
{
    public static class GetAllProjectsEndpoint
    {
        public static RouteGroupBuilder MapGetAllProjectsEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapGet("", GetAllProjects)
                .WithName(nameof(GetAllProjects))
                .WithOpenApi()
                .AddEndpointFilter<ValidationFilter<GetAllProjectsRequest>>(); ;

            return builder;
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        public static async Task<IResult> GetAllProjects(
            ApplicationDbContext dbContext, IMapper mapper)
        {
            var projects = dbContext.Projects.ToList();
            var projectDto = mapper.Map<List<GetProjectByIdResponse>>(projects);
            return Results.Ok(projectDto);
        }
    }
}
