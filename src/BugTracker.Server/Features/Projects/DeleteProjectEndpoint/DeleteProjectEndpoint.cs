using BugTrackerApi.Data;
using BugTrackerApi.ValidationFilter;

namespace BugTrackerApi.Features.Projects.DeleteProjectEndpoint
{
    public static class DeleteProjectEndpoint
    {
        public static RouteGroupBuilder MapDeleteProjectEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapDelete("/{Id}", DeleteProject)
                .WithName(nameof(DeleteProject))
                .WithOpenApi()
                .AddEndpointFilter<ValidationFilter<DeleteProjectRequest>>();

            return builder;
        }

        /// <summary>
        /// Delete project
        /// </summary>
        public static async Task<IResult> DeleteProject(ApplicationDbContext dbContext, int Id)
        {
            dbContext.Remove(Id);
            dbContext.SaveChanges();
            return Results.Ok();
        }
    }
}
