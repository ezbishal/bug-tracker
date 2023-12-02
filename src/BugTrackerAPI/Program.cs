using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BugTrackerApi;
using BugTrackerApi.Authentication.GetAuthTokenEndpoint;
using BugTrackerApi.Authentication.RegisterUserEndpoint;
using BugTrackerApi.Features.Projects.AddProjectEndpoint;
using BugTrackerApi.Features.Projects.DeleteProjectEndpoint;
using BugTrackerApi.Features.Projects.GetAllProjectsEndpoint;
using BugTrackerApi.Features.Projects.GetProjectByIdEndpoint;
using BugTrackerApi.Features.Projects.UpdateProjectEndpoint;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var config = builder.Configuration;

string? kvUrl = config["KeyVaultConfig:KvUrl"];
string? tenantId = config["KeyVaultConfig:TenantId"];
string? clientId = config["KeyVaultConfig:ClientId"];
string? clientSecret = config["KeyVaultConfig:ClientSecretId"];

var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

var client = new SecretClient(new Uri(kvUrl), credential);
config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

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

