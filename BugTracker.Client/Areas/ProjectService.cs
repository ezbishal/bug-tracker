using BugTracker.Shared.Models;

namespace BugTracker.Client.Areas;

public class ProjectService(HttpClient httpClient)
{
    private readonly HttpClient httpClient = httpClient;

    public IEnumerable<ProjectModel> GetAllProjects()
    {
        return [];
    }
}