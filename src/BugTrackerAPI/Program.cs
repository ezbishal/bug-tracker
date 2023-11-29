using BugTrackerApi;
using BugTrackerApi.Features.AddProjectEndpoint;
using BugTrackerApi.Features.DeleteProject;
using BugTrackerApi.Features.GetAllProjects;
using BugTrackerApi.Features.GetProjectById;
using BugTrackerApi.Features.Token;
using BugTrackerApi.Features.UpdateProject;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rhenus External API v1");
    });
}

app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

//Token
app.MapGroup("/api/token").WithTags("Token")
    .MapGetTokenEndpoint();

// Projects
app.MapGroup("/api/projects").WithTags("Projects")
    .MapGetAllProjectsEndpoint()
    .MapGetProjectByIdEndpoint()
    .MapAddProjectEndpoint()
    .MapUpdateProjectEndpoint()
    .MapDeleteProjectEndpoint();

// Bugs


// Comments

app.Run();

