using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BugTracker.Server.Areas.Authentication;

public static class GetAuthTokenEndpoint
{
    public static RouteGroupBuilder MapGetTokenEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPost("/token", GetToken)
            .WithName(nameof(GetToken))
            .WithOpenApi();

        return builder;
    }

    /// <summary>
    /// Get authentication token
    /// </summary>
    private static async Task<IResult> GetToken(
        [FromQuery(Name = "username")] string username,
        [FromQuery(Name = "password"), DataType(DataType.Password)] string password,
        UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
    {

        var user = await userManager.FindByNameAsync(username);
        if (user is not null && await userManager.CheckPasswordAsync(user, password))
        {
            var token = GenerateJwtToken(user);
            return Results.Ok(token);

        }

        return Results.Unauthorized();
    }

    public static string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
        };

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

