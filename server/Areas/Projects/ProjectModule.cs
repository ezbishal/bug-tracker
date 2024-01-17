using server.Contracts;

namespace server.Areas.Projects;

public class ProjectModule : IModule
{
    public IEndpointRouteBuilder RegisterEndpoints(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/projects");

        group.MapGet("", () => { return "Hi there"; })
                .WithOpenApi();

        return app;
    }
}