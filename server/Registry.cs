using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Authentication;
using Server.Data;
using Server.Exceptions;

namespace Server;

public static class Registry
{
	public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
	{
		builder.ConfigureIdentity();

		builder.Services.AddAntiforgery();

		builder.Services.AddProblemDetails();

		builder.Services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlite("Data Source=app.db");

		});

		builder.Services.AddCors(options =>
		{
			options.AddPolicy("AllowOrigins",
				builder =>
				{
					builder.WithOrigins("http://localhost:3000")
						   .AllowAnyHeader()
						   .AllowAnyMethod();
				});
		});

		builder.Services.AddEndpointsApiExplorer();

		builder.Services.AddAuthorization();

		builder.Services.AddExceptionHandler<CustomExceptionHandler>();

		builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

		builder.Services.AddCors();

		return builder;
	}

	public static WebApplicationBuilder ConfigureIdentity(this WebApplicationBuilder builder)
	{
		builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
		{
			options.Password.RequireUppercase = false;
		})
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

		return builder;
	}
}
