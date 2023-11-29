using BugTrackerAPI.Features.GetProjectById;

namespace BugTrackerAPI.Features.GetAllProjects;

public class GetAllProjectsResponse
{
    public IEnumerable<GetProjectByIdResponse> Projects { get; set; }
}
