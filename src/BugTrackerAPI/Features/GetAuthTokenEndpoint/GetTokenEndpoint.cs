using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BugTrackerApi.Features.Token;

public static class GetTokenEndpoint
{
    public static RouteGroupBuilder MapGetTokenEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPost("", GetToken)
            .WithName(nameof(GetToken))
            .WithOpenApi();

        return builder;
    }

    /// <summary>
    /// Get authentication token
    /// </summary>
    private static Task<IResult> GetToken(
        [FromHeader(Name = "username")] string? username,
        [FromHeader(Name = "password")] string? password,
        CancellationToken cancellationToken)
    {

        var user = GetUser(username, password);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
            new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(5)).ToUnixTimeSeconds().ToString()),
        };

        var token = new JwtSecurityToken(
            new JwtHeader(
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKeyIsSecretSoDoNotTellAnyoneAboutIt")),
                SecurityAlgorithms.HmacSha256)),
            new JwtPayload(claims));

        var output = new JwtSecurityTokenHandler().WriteToken(token);

        return Task.FromResult(Results.Ok(output));
    }

    public static IdentityUser? GetUser(string? username, string? password)
    {
        return new IdentityUser();
    }

}

