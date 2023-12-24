using Microsoft.AspNetCore.Identity;

namespace BugTracker.Server.Helpers;

public static class WebApplicationExtension
{
	public static async Task<WebApplication> SeedRoles(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

		if(!await roleManager.RoleExistsAsync("Admin")) await roleManager.CreateAsync(new IdentityRole("Admin"));
		if(!await roleManager.RoleExistsAsync("ProjectManager")) await roleManager.CreateAsync(new IdentityRole("ProjectManager"));
		if(!await roleManager.RoleExistsAsync("TeamLead")) await roleManager.CreateAsync(new IdentityRole("TeamLead"));
		if(!await roleManager.RoleExistsAsync("TeamMember")) await roleManager.CreateAsync(new IdentityRole("TeamMember"));
		if(!await roleManager.RoleExistsAsync("Guest")) await roleManager.CreateAsync(new IdentityRole("Guest"));
		
		return app;
	}
	
}