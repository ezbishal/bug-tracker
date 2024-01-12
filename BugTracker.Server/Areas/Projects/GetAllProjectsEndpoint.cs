using AutoBogus;
using BugTracker.Server.Data;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Areas.Projects
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
            ApplicationDbContext dbContext)
        {
            // var projectsModels = dbContext.Projects.ToList();
            // var getProjectsModels = new List<ProjectModel>();
            // foreach (var project in projectsModels)
            // {
            //     getProjectsModels.Add(mapper.Map<ProjectModel>(project));
            // }
            var getProjectsModel = AutoFaker.Generate<ProjectModel>(5);

            return Results.Ok(getProjectsModel);
        }
    }
}
