using Microsoft.AspNetCore.Identity;

namespace BugTracker.Server.Areas.Authentication;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

}
