
namespace BugTracker.Client.Authentication;

public class AuthService(HttpClient httpClient)
{
	private readonly HttpClient httpClient = httpClient;
	
	public async Task Authenticate(string? usernmae, string? password)
    {
    	
    }
}