using BugTracker.Client.Layout;
using BugTracker.Server;
using BugTracker.Server.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bug Tracker API");
	});

	app.UseWebAssemblyDebugging();
}
else
{
	app.UseHsts();
}

app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

await app.SeedRoles();

app.MapApiEndpoints();

app.Run();

