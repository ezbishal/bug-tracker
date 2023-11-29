using BugTrackerApi.Features.AddProjectEndpoint;
using BugTrackerApi.Features.DeleteProject;
using BugTrackerApi.Features.GetAllProjects;
using BugTrackerApi.Features.GetProjectById;
using BugTrackerApi.Features.Token;
using BugTrackerApi.Features.UpdateProject;
using BugTrackerAPI.Data;
using BugTrackerAPI.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rhenus External API", Version = "v1" });

    // Define the OAuth2.0 or Bearer token scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
    .AddJwtBearer("JwtBearer", JwtBearerOptions =>
    {
        JwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKeyIsSecretSoDoNotTellAnyoneAboutIt")),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)
        };

    });
builder.Services.AddAuthorization();
builder.Services.AddCors();

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAutoMapper(typeof(MapperProfiles));

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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

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

app.UseCors();
app.Run();

