using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using Dumpify;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Server.Authentication;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app) {

        RouteGroupBuilder group = app.MapGroup("/api/users").WithTags("Authentication");

        group.MapGet("", GetAllUsers)
            .WithName(nameof(GetAllUsers))
            .WithOpenApi()
            .RequireAuthorization(builder => builder.RequireRole(RolesEnum.Admin.ToString()));
        

        group.MapPost("/register", RegisterUser)
            .WithName(nameof(RegisterUser))
            .WithOpenApi();

        group.MapGet("/token", GetToken)
            .WithName(nameof(GetToken))
            .WithOpenApi();
        
        group.MapPut("/{email}", UpdateUser)
            .WithName(nameof(UpdateUser))
            .WithOpenApi()
            .RequireAuthorization(builder => builder.RequireRole(RolesEnum.Admin.ToString()));

        group.MapDelete("/{email}", DeleteUser)
            .WithName(nameof(DeleteUser))
            .WithOpenApi()
            .RequireAuthorization(builder => builder.RequireRole(RolesEnum.Admin.ToString()));

        return app;
    }

    private static async Task<IResult> UpdateUser([FromRoute] string email, RegisterUserModel user, UserManager<ApplicationUser> userManager, HttpContext context)
    {
        ApplicationUser userToUpdate = await userManager.FindByEmailAsync(email);
        if(userToUpdate is null) return Results.NotFound();
        userToUpdate.FirstName = user.FirstName;
        userToUpdate.LastName = user.LastName;
        IdentityResult? result = await userManager.UpdateAsync(userToUpdate);
        if(result.Succeeded)
        {
            return Results.Ok("User successfully updated.");
        }
        else{
            return Results.Problem("Unable to update specified user.");
        }
    }

    private static async Task<IResult> DeleteUser([FromRoute] string email, HttpContext context, UserManager<ApplicationUser> userManager)
    {
        email.Dump();
        ApplicationUser userToDelete = await userManager.FindByEmailAsync(email);
        userToDelete.Dump();
        if(userToDelete is null) return Results.NotFound();
        IdentityResult? result = await userManager.DeleteAsync(userToDelete);  
        if(result.Succeeded)
        {
            return Results.Ok("User successfully deleted.");
        }
        else{
            return Results.Problem("Unable to delete specified user.");
        }
    }

    public static async Task<IResult> RegisterUser(RegisterUserModel registerUserModel,
        HttpContext context,
        UserManager<ApplicationUser> userManager)
    {
        var user = new ApplicationUser
        {
            FirstName = registerUserModel.FirstName,
            LastName = registerUserModel.LastName,
            UserName = registerUserModel.Email,
            Email = registerUserModel.Email
        };

        IdentityResult? result = await userManager.CreateAsync(user, registerUserModel.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, RolesEnum.Admin.ToString());
            // return Results.Ok(user);
            return Results.Ok("User created");
        }
        else
        {
            return Results.BadRequest(result.Errors);
        }
    }

    public static async Task<IResult> GetAllUsers(UserManager<ApplicationUser> userManager, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<ApplicationUser> users = await userManager.Users.ToListAsync(cancellationToken);
            if (users?.Any() ?? false) return Results.Ok(users);
            else return Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.StatusCode(500);
        }

    }
    public static async Task<IResult> GetToken([FromQuery(Name = "email")] string email, [FromQuery(Name = "password")] string password, UserManager<ApplicationUser> userManager, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(email);
        var roles = await userManager.GetRolesAsync(user);
        if (user is not null && await userManager.CheckPasswordAsync(user, password))
        {
            var token = GenerateJwtToken(user, roles);
            return Results.Ok(token);
        }

        return Results.Unauthorized();
    }
    public static string GenerateJwtToken(ApplicationUser user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Name, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };
        foreach(var role in roles)
        {
            claims.Add(new(ClaimTypes.Role, role));
        }

       var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKeyIsSecretSoDoNotTellAnyoneAboutIt"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "MyIssuer",
            audience: "MyAudience",
            claims: claims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}