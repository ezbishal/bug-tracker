using Azure.Identity;
using BugTracker.Server.Authentication;
using BugTracker.Server.Data;
using BugTracker.Server.Exceptions;
using BugTracker.Server.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BugTracker.Server;

public static class Registry
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.ConfigureSwagger();
        builder.ConfigureKeyVault();
        builder.ConfigureIdentity();
        builder.ConfigureAuthentication();
        builder.Services.ConfigureBlazor();

        var value = builder.Configuration["BugTrackerDBConnectionString"];
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration["BugTrackerDBConnectionString"]);
        });

        builder.Services.AddCors();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAuthorization();

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

        builder.Services.AddCors();

        builder.Services.AddAutoMapper(typeof(MapperProfiles));

        return builder;
    }

    public static WebApplicationBuilder ConfigureKeyVault(this WebApplicationBuilder builder)
    {
        var secrets = builder.Configuration.GetSection("KeyVaultConfig").Get<SecretsSettings>();

        if (secrets is not null)
        {
            builder.Configuration.AddAzureKeyVault(secrets.KeyVaultEndpoint, new DefaultAzureCredential());
        }

        return builder;
    }

    public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bug Tracker API", Version = "v1" });

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

        return builder;
    }

    public static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKeyIsSecretSoDoNotTellAnyoneAboutIt")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

            });

        return builder;
    }

    public static WebApplicationBuilder ConfigureIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return builder;
    }
}
