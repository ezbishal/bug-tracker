using AutoMapper;
using BugTrackerApi.Data;
using BugTrackerApi.Features.Projects.GetProjectByIdEndpoint;
using BugTrackerApi.ValidationFilter;

namespace BugTrackerApi.Features.Projects.GetAllProjectsEndpoint
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
