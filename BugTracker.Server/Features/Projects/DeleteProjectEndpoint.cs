using BugTracker.Server.Data;

namespace BugTracker.Server.Features.Projects
{
    public static class DeleteProjectEndpoint
    {
        public static RouteGroupBuilder MapDeleteProjectEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapDelete("/{Id}", DeleteProject)
                .WithName(nameof(DeleteProject))
                .WithOpenApi();

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
