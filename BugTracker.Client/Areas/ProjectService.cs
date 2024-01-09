using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using BugTracker.Shared.Models;
using System.Text.Json;

namespace BugTracker.Client.Areas;

public class ProjectService
{
	private readonly HttpClient httpClient;
	private readonly NavigationManager navigationManager;

	public ProjectService(HttpClient httpClient, NavigationManager navigationManager)
	{
		this.httpClient = httpClient;
		this.navigationManager = navigationManager;
		this.httpClient.BaseAddress = new Uri(navigationManager.BaseUri);
	}

	public async Task<IEnumerable<GetProjectModel>> GetAllProjects()
	{
		var projects = httpClient.GetFromJsonAsync<IEnumerable<GetProjectModel>>("api/projects",
			new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			}).Result;
		
		return projects;
	}
}