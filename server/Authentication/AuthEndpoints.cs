using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Authentication;

public static class AuthEndpoints
{
	public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
	{
		RouteGroupBuilder group = app.MapGroup("/api/user").WithTags("Authentication");

		group.MapGet("", GetAllUsers)
			.WithName(nameof(GetAllUsers))
			.WithOpenApi();

		group.MapPost("/register", RegisterUser)
			.WithName(nameof(RegisterUser))
			.WithOpenApi();

		group.MapPost("/token", GetToken)
			.WithName(nameof(GetToken))
			.WithOpenApi();


		return app;
	}

	public static async Task<IResult> RegisterUser(RegisterUserModel registerUserModel,
		HttpContext context,
		UserManager<ApplicationUser> userManager)
	{
		var user = new ApplicationUser
		{
			FirstName = registerUserModel.FirstName,
			LastName = registerUserModel.LastName,
			UserName = registerUserModel.Username
		};
		
		IdentityResult? result = await userManager.CreateAsync(user, registerUserModel.Password);

		if (result.Succeeded)
		{
			await userManager.AddToRoleAsync(user, RolesEnum.Admin.ToString());
			return Results.Ok(user);
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
	public static async Task<IResult> GetToken([FromQuery(Name = "username")] string username,
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