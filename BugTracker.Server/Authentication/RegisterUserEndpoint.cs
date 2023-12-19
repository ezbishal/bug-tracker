using BugTracker.Server.Helpers;
using BugTracker.Shared.UserModels;
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

    private static async Task<IResult> RegisterUser(RegisterUserModel request, HttpContext context, UserManager<ApplicationUser> userManager)
    {
        var user = new ApplicationUser { FirstName = request.FirstName, LastName = request.LastName, UserName = request.Username };
        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded) return Results.Ok(user);
        else return Results.BadRequest(result.Errors);
    }
}
