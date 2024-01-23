using Microsoft.AspNetCore.Identity;

namespace Server.Helpers;

public static class WebApplicationExtension
{
    public static async Task<WebApplication> SeedRoles(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync(RolesEnum.Admin.ToString())) await roleManager.CreateAsync(new IdentityRole(RolesEnum.Admin.ToString()));
        if (!await roleManager.RoleExistsAsync(RolesEnum.ProjectManager.ToString())) await roleManager.CreateAsync(new IdentityRole(RolesEnum.ProjectManager.ToString()));
        if (!await roleManager.RoleExistsAsync(RolesEnum.TeamLead.ToString())) await roleManager.CreateAsync(new IdentityRole(RolesEnum.TeamLead.ToString()));
        if (!await roleManager.RoleExistsAsync(RolesEnum.TeamMember.ToString())) await roleManager.CreateAsync(new IdentityRole(RolesEnum.TeamMember.ToString()));
        if (!await roleManager.RoleExistsAsync(RolesEnum.Guest.ToString())) await roleManager.CreateAsync(new IdentityRole(RolesEnum.Guest.ToString()));

        return app;
    }

}