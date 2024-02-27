using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Server.Data;
using Server.Models;

namespace Server.Areas.Bugs;

public static class BugEndpoints
{
	public static IEndpointRouteBuilder MapBugEndpoints(this IEndpointRouteBuilder app)
	{
		RouteGroupBuilder group = app.MapGroup("/api/bugs").WithTags("Bugs");

		group.MapGet("/{id}", GetBugById)
			.WithName(nameof(GetBugById))
			.WithOpenApi();

		group.MapPost("", CreateBug)
			.WithName(nameof(CreateBug))
			.WithOpenApi();

		group.MapPut("/{id}", UpdateBug)
			.WithName(nameof(UpdateBug))
			.WithOpenApi();

		group.MapDelete("/{id}", DeleteBug)
			.WithName(nameof(DeleteBug))
			.WithOpenApi();


		return app;
	}

    private static async Task DeleteBug(int bugId, HttpContext context, ApplicationDbContext dbContext)
    {
        try{
			var bugToRemove = await dbContext.Bugs.SingleAsync(b => b.Id == bugId); 
			dbContext.Bugs.Remove(bugToRemove); 
			dbContext.SaveChanges();
		}
		catch (Exception ex){
			Log.Error(ex.Message);
			throw new Exception(ex.Message);
		}
    }

    private static async Task<IResult> UpdateBug(int id, BugModel bug, ApplicationDbContext dbContext)
	{
		try
		{
			BugModel bugToUpdate = await dbContext.Bugs.SingleAsync(b => b.Id == id);
			bugToUpdate.ProjectId = bug.ProjectId;
			bugToUpdate.ReproductionSteps = bug.ReproductionSteps;
			dbContext.SaveChanges();

			return Results.Ok(bugToUpdate);
		}
		catch (Exception ex)
		{
			Log.Error(ex.Message);
			throw new Exception(ex.Message);
		}

	}

	private static async Task<IResult> CreateBug(BugModel bug, ApplicationDbContext dbContext)
	{
		try
		{
			await dbContext.Projects.AddAsync(bug);
			dbContext.SaveChanges();
			return Results.CreatedAtRoute(
				routeName: nameof(GetBugById),
				routeValues: new { id = bug.Id },
				value: bug
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

		return Results.Ok(AutoFaker.Generate<ProjectModel>(5));

	}

	private static async Task<IResult> GetBugById([FromRoute] int id, ApplicationDbContext dbContext)
	{
		try
		{
			// ProjectModel project = await dbContext.Projects.SingleAsync(p => p.Id == id);
			// return Results.Ok(project);
			var project = AutoFaker.Generate<ProjectModel>(1).First();
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
