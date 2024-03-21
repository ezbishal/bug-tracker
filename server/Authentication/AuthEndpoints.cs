using System.Transactions;
using Dumpify;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Authentication;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {

        RouteGroupBuilder group = app.MapGroup("/api/users").WithTags("Authentication");

        group.MapGet("", GetAllUsers)
            .WithName(nameof(GetAllUsers))
            .WithOpenApi()
            .RequireAuthorization(builder => builder.RequireRole(RolesEnum.Admin.ToString()));


        group.MapPost("/register", RegisterUser)
            .WithName(nameof(RegisterUser))
            .WithOpenApi();

        group.MapGet("/token", GetApiKey)
            .WithName(nameof(GetApiKey))
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
        if (userToUpdate is null) return Results.NotFound();
        userToUpdate.FirstName = user.FirstName;
        userToUpdate.LastName = user.LastName;
        IdentityResult? result = await userManager.UpdateAsync(userToUpdate);
        if (result.Succeeded)
        {
            return Results.Ok("User successfully updated.");
        }
        else
        {
            return Results.Problem("Unable to update specified user.");
        }
    }

    private static async Task<IResult> DeleteUser([FromRoute] string email, HttpContext context, UserManager<ApplicationUser> userManager)
    {
        email.Dump();
        ApplicationUser userToDelete = await userManager.FindByEmailAsync(email);
        userToDelete.Dump();
        if (userToDelete is null) return Results.NotFound();
        IdentityResult? result = await userManager.DeleteAsync(userToDelete);
        if (result.Succeeded)
        {
            return Results.Ok("User successfully deleted.");
        }
        else
        {
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
    public static async Task<IResult> GetApiKey(
        [FromQuery(Name = "email")] string email,
        [FromQuery(Name = "password")] string password,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken
        )
    {
        var user = await userManager.FindByNameAsync(email);
        if (user is not null && await userManager.CheckPasswordAsync(user, password))
        {
            ApiKeyModel? ApiKey = await dbContext.ApiKeys.FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
            if (ApiKey is null)
                return Results.Unauthorized();

            if (ApiKey.ExpiryDate > DateTime.Now)
                return Results.Ok(ApiKey.Key);

            if (ApiKey.ExpiryDate < DateTime.Now)
            {
                try
                {
                    dbContext.ApiKeys.Remove(ApiKey);
                    await dbContext.SaveChangesAsync(cancellationToken);

                    var newApiKey = new ApiKeyModel
                    {
                        UserId = user.Id,
                        Key = Guid.NewGuid().ToString(),
                        CreatedDate = DateTime.Now,
                        ExpiryDate = DateTime.Now.AddMonths(1)
                    };
                    dbContext.ApiKeys.Add(newApiKey);
                    await dbContext.SaveChangesAsync(cancellationToken);

                    return Results.Ok(newApiKey.Key);
                }
                catch
                {
                    return Results.Problem("Unable to generate a new API key due to internal issue. Please contact support.");
                }
            }


        }

        return Results.Unauthorized();
    }
}
