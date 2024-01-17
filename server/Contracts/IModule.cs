namespace server.Contracts;

public interface IModule
{
    public IEndpointRouteBuilder RegisterEndpoints(IEndpointRouteBuilder app);

}
