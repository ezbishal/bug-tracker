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
            var projectsModels = dbContext.Projects.ToList();
            var getProjectModels = new List<GetProjectModel>();
            foreach (var project in projectsModels)
            {
                getProjectModels.Add(new GetProjectModel().Map(project));
            }

            return Results.Ok(getProjectModels);
        }
    }
}
