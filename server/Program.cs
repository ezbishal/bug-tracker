using Microsoft.AspNetCore.Http.HttpResults;
using server;
using server.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();
var app = builder.Build();
app.UseCors();

app.UseStaticFiles();

app.UseAntiforgery();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

await app.SeedRoles();

app.Run();

