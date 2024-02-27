using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Server.Data;
using Server.Models;

namespace Server.Areas.Projects;

public static class ProjectEndpoints
{
	public static IEndpointRouteBuilder MapProjectEndpoints(this IEndpointRouteBuilder app)
	{
		RouteGroupBuilder group = app.MapGroup("/api/projects").WithTags("Project");

		group.MapGet("", GetAllProjects)
			.WithName(nameof(GetAllProjects))
			.WithOpenApi();

		group.MapGet("/{id}", GetProjectById)
			.WithName(nameof(GetProjectById))
			.WithOpenApi();

		group.MapPost("", CreateProject)
			.WithName(nameof(CreateProject))
			.WithOpenApi();

		group.MapPut("/{id}", UpdateProject)
			.WithName(nameof(UpdateProject))
			.WithOpenApi();


		return app;
	}
	
	private static async Task<IResult> UpdateProject(int id, BugModel project, ApplicationDbContext dbContext)
	{
		try
		{
			BugModel projectToUpdate = await dbContext.Projects.SingleAsync(p => p.Id == id);
			projectToUpdate.Name = project.Name;
			dbContext.SaveChanges();

			return Results.Ok(projectToUpdate);
		}
		catch (Exception ex)
		{
			Log.Error(ex.Message);
			throw new Exception(ex.Message);
		}

	}

	private static async Task<IResult> CreateProject(BugModel project, ApplicationDbContext dbContext)
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
			throw new Exception(ex.Message);
		}
	}

	private static async Task<IResult> GetAllProjects(ApplicationDbContext dbContext)
	{
		// try
		// {
		// 	IEnumerable<ProjectModel> projects = await dbContext.Projects.ToListAsync();
		// 	return Results.Ok(projects);
		// }
		// catch (Exception ex)
		// {
		// 	Log.Error(ex.Message);
		// 	throw new Exception(ex.Message);
		// }

		return Results.Ok(AutoFaker.Generate<BugModel>(5));

	}

	private static async Task<IResult> GetProjectById([FromRoute] int id, ApplicationDbContext dbContext)
	{
		try
		{
			// ProjectModel project = await dbContext.Projects.SingleAsync(p => p.Id == id);
			// return Results.Ok(project);
			var project = AutoFaker.Generate<BugModel>(1).First();
			project.Id = id; 
			return Results.Ok(project);
		}
		catch (Exception ex)
		{
			Log.Error(ex.Message);
			throw new Exception(ex.Message);
		}
	}
}
