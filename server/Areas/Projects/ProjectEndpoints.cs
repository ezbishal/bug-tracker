using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Server.Authentication;
using Server.Data;
using Server.Models;

namespace Server.Areas.Projects;

public static class ProjectEndpoints
{
	public static IEndpointRouteBuilder MapProjectEndpoints(this IEndpointRouteBuilder app)
	{
		RouteGroupBuilder group = app.MapGroup("/api/projects")
			.WithTags("Project")
			.AddEndpointFilter<ApiKeyAuthenticationFilter>();

		group.MapGet("", GetAllProjects)
			.WithName(nameof(GetAllProjects))
			.WithOpenApi()
			.RequireAuthorization();

		group.MapGet("/{id}", GetProjectById)
			.WithName(nameof(GetProjectById))
			.WithOpenApi()
			.RequireAuthorization();

		group.MapPost("", CreateProject)
			.WithName(nameof(CreateProject))
			.WithOpenApi()
			.RequireAuthorization();

		group.MapPut("/{id}", UpdateProject)
			.WithName(nameof(UpdateProject))
			.WithOpenApi()
			.RequireAuthorization();

		group.MapDelete("/{id}", DeleteProject)
			.WithName(nameof(DeleteProject))
			.WithOpenApi()
			.RequireAuthorization();

		return app;
	}

	private static async Task<IResult> DeleteProject(int id, ApplicationDbContext dbContext, HttpContext context)
	{
		try
		{
			ProjectModel projectToDelete = await dbContext.Projects.SingleAsync(p => p.Id == id);
			dbContext.Projects.Remove(projectToDelete);
			dbContext.SaveChanges();

			return Results.Ok("Project successfully removed");
		}
		catch (Exception ex)
		{
			Log.Error(ex.Message);
			return Results.Problem(ex.Message);
		}
	}

	private static async Task<IResult> UpdateProject(int id, ProjectModel project, ApplicationDbContext dbContext)
	{
		try
		{
			ProjectModel projectToUpdate = await dbContext.Projects.SingleAsync(p => p.Id == id);
			projectToUpdate.Name = project.Name;
			dbContext.SaveChanges();

			return Results.Ok(projectToUpdate);
		}
		catch (Exception ex)
		{
			Log.Error(ex.Message);
			return Results.Problem(ex.Message);
		}

	}

	private static async Task<IResult> CreateProject(ProjectModel project, ApplicationDbContext dbContext)
	{
		try
		{
			await dbContext.Projects.AddAsync(project);
			dbContext.SaveChanges();
			return Results.CreatedAtRoute(
				routeName: nameof(GetProjectById),
				routeValues: new { id = project.Id },
				value: project
			);
		}
		catch (Exception ex)
		{
			Log.Error(ex.Message);
			return Results.Problem(ex.Message);
		}
	}

	private static async Task<IResult> GetAllProjects(ApplicationDbContext dbContext)
	{
		try
		{
			IEnumerable<ProjectModel> projects = await dbContext.Projects.ToListAsync();
			return Results.Ok(projects);
		}
		catch (Exception ex)
		{
			Log.Error(ex.Message);
			return Results.Problem(ex.Message);
		}

	}

	private static async Task<IResult> GetProjectById([FromRoute] int id, ApplicationDbContext dbContext)
	{
		try
		{
			ProjectModel project = await dbContext.Projects.SingleAsync(p => p.Id == id);
			return Results.Ok(project);
		}
		catch (Exception ex)
		{
			Log.Error(ex.Message);
			return Results.Problem(ex.Message);
		}
	}
}