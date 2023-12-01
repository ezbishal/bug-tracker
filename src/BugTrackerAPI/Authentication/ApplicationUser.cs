using Microsoft.AspNetCore.Identity;

namespace BugTrackerApi.Authentication;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
