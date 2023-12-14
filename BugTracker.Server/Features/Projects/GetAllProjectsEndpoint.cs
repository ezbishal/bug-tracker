using AutoMapper;
using BugTracker.Server.Data;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Features.Projects
{
    public static class GetAllProjectsEndpoint
    {
        public static RouteGroupBuilder MapGetAllProjectsEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapGet("", GetAllProjects)
                .WithName(nameof(GetAllProjects))
                .WithOpenApi();

            return builder;
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        public static async Task<IResult> GetAllProjects(
            ApplicationDbContext dbContext, IMapper mapper)
        {
            var projects = dbContext.Projects.ToList();
            var projectDto = mapper.Map<List<GetProjectModel>>(projects);
            return Results.Ok(projectDto);
        }
    }
}
