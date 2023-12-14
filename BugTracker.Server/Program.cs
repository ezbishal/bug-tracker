using BugTracker.Client.Components;
using BugTracker.Client.Pages;
using BugTracker.Server;

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
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapEndpoints();

app.Run();

