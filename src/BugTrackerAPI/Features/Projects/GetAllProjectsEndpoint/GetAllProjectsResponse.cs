using BugTrackerApi.Features.Projects.GetProjectByIdEndpoint;

namespace BugTrackerApi.Features.Projects.GetAllProjectsEndpoint;

public class GetAllProjectsResponse
{
    public IEnumerable<GetProjectByIdResponse> Projects { get; set; }
}
