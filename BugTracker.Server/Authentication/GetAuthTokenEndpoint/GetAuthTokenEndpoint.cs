using BugTracker.Server.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BugTracker.Server.Authentication.GetAuthTokenEndpoint;

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
        GetAuthTokenRequest request,
        UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
    {

        var user = await userManager.FindByNameAsync(request.Username);
        if (user is not null && await userManager.CheckPasswordAsync(user, request.Password))
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

