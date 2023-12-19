using AutoMapper;
using BugTracker.Server.Data;
using BugTracker.Server.Helpers;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Features.Projects
{
    public static class CreateProjectEndpoint
    {
        public static RouteGroupBuilder MapCreateProjectEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapPost("", CreateProject)
                .WithName(nameof(CreateProject))
                .WithOpenApi()
                .AddEndpointFilter<ValidationFilter<ProjectModel>>();

            return builder;
        }

        /// <summary>
        /// Create a new project
        /// </summary>
        public static async Task<IResult> CreateProject(ProjectModel createProjectModel,
             ApplicationDbContext dbContext, IMapper mapper)
        {
            try
            {
                var projectModel = mapper.Map<ProjectModel>(createProjectModel);
                dbContext.Projects.Add(projectModel);
                dbContext.SaveChanges();
                var getProjectModel = new GetProjectModel().Map(projectModel);

                return Results.CreatedAtRoute(
                    routeName: "GetProjectById",
                    routeValues: new { id = projectModel.Id },
                    value: getProjectModel
                );


            }
            catch (Exception ex)
            {
                return Results.StatusCode(500);
            }
        }
    }
}
