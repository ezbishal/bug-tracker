using BugTracker.Server.Features.Projects.GetProjectByIdEndpoint;

namespace BugTracker.Server.Features.Projects.GetAllProjectsEndpoint;

public class GetAllProjectsResponse
{
    public IEnumerable<GetProjectByIdResponse> Projects { get; set; }
}
