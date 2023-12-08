using BugTrackerApi.ValidationFilter;
using Microsoft.AspNetCore.Identity;

namespace BugTrackerApi.Authentication.RegisterUserEndpoint;

public static class RegisterUserEndpoint
{
    public static RouteGroupBuilder MapRegisterUserEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPost("/register", RegisterUser)
            .WithName(nameof(RegisterUser))
            .WithOpenApi()
            .AddEndpointFilter<ValidationFilter<RegisterUserRequest>>();

        return builder;
    }

    private static async Task<IResult> RegisterUser(RegisterUserRequest request, HttpContext context, UserManager<ApplicationUser> userManager)
    {
        var user = new ApplicationUser { FirstName = request.FirstName, LastName = request.LastName, UserName = request.Username };
        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded) return Results.Ok(user);
        else return Results.BadRequest(result.Errors);
    }
}
