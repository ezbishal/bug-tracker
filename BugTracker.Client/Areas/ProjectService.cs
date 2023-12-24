using System.Net.Http.Json;
using BugTracker.Shared.Models;

namespace BugTracker.Client.Areas;

public class ProjectService(HttpClient httpClient)
{
	private readonly HttpClient httpClient = httpClient;

	public async Task<IEnumerable<GetProjectModel>> GetAllProjects()
	{
		var projects = await httpClient.GetFromJsonAsync<IEnumerable<GetProjectModel>>("https://localhost:7109/api/projects");
		
		return projects;
	}
}