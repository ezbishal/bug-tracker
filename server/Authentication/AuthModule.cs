using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.Contracts;
using server.Helpers;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace server.Authentication;

public class AuthModule : IModule
{
    public IEndpointRouteBuilder RegisterEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/user");

        group.MapPost("/register", async (RegisterUserModel registerUserModel, HttpContext context, UserManager<ApplicationUser> userManager) =>
        {
            var user = new ApplicationUser
            {
                FirstName = registerUserModel.FirstName,
                LastName = registerUserModel.LastName,
                Email = registerUserModel.Email
            };

            var result = await userManager.CreateAsync(user, registerUserModel.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, RolesEnum.Admin.ToString());
                return Results.Ok(user);
            }
            else
            {
                return Results.BadRequest(result.Errors);
            }
        })
        .WithOpenApi()
        .AddEndpointFilter<ValidationFilter<RegisterUserModel>>();

        group.MapPost("/token", async (
            [FromQuery(Name = "username")] string username,
            [FromQuery(Name = "password"), DataType(DataType.Password)] string password,
            UserManager<ApplicationUser> userManager,
            CancellationToken cancellationToken) =>
            {
                var user = await userManager.FindByNameAsync(username);
                if (user is not null && await userManager.CheckPasswordAsync(user, password))
                {
                    var token = GenerateJwtToken(user);
                    return Results.Ok(token);

                }

                return Results.Unauthorized();
            })
            .WithOpenApi();


        return app;
    }

    public static string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName)
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