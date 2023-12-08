using BugTracker.Client.Pages;
using BugTracker.Server.Components;
using BugTrackerApi;
using BugTrackerApi.Authentication.GetAuthTokenEndpoint;
using BugTrackerApi.Authentication.RegisterUserEndpoint;
using BugTrackerApi.Features.Projects.AddProjectEndpoint;
using BugTrackerApi.Features.Projects.DeleteProjectEndpoint;
using BugTrackerApi.Features.Projects.GetAllProjectsEndpoint;
using BugTrackerApi.Features.Projects.GetProjectByIdEndpoint;
using BugTrackerApi.Features.Projects.UpdateProjectEndpoint;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

#region Blazor 

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddFluentUIComponents();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bug Tracker API");
    });
}

#region Blazor

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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



#endregion

app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

//Authentication
app.MapGroup("/user").WithTags("Authentication")
    .MapRegisterUserEndpoint()
    .MapGetTokenEndpoint();

// Projects
app.MapGroup("/projects").WithTags("Projects")
    .MapGetAllProjectsEndpoint()
    .MapGetProjectByIdEndpoint()
    .MapAddProjectEndpoint()
    .MapUpdateProjectEndpoint()
    .MapDeleteProjectEndpoint();

// Bugs


// Comments

app.Run();

