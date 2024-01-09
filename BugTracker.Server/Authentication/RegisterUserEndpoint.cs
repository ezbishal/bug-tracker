using BugTracker.Server.Helpers;
using Microsoft.AspNetCore.Identity;

namespace BugTracker.Server.Authentication;

public static class RegisterUserEndpoint
{
	public static RouteGroupBuilder MapRegisterUserEndpoint(this RouteGroupBuilder builder)
	{
		builder.MapPost("/register", RegisterUser)
			.WithName(nameof(RegisterUser))
			.WithOpenApi()
			.AddEndpointFilter<ValidationFilter<RegisterUserModel>>();

		return builder;
	}

	private static async Task<IResult> RegisterUser(RegisterUserModel registerUserModel, HttpContext context, UserManager<ApplicationUser> userManager)
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
	}
	
}
