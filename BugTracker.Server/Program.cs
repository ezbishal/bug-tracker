using BugTracker.Server;
using BugTracker.Server.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();
var app = builder.Build();

app.UseStaticFiles();

app.UseAntiforgery();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

await app.SeedRoles();

app.MapGet("/", () =>
{
    return new RazorComponentResult<_Host>();
});

app.MapApiEndpoints();

app.Run();

