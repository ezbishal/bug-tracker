using BugTrackerApi.Features.GetProjectById;

namespace BugTrackerApi.Features.GetAllProjects;

public class GetAllProjectsResponse
{
    public IEnumerable<GetProjectByIdResponse> Projects { get; set; }
}
