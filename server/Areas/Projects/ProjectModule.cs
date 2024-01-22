using Microsoft.EntityFrameworkCore;
using server.Contracts;
using server.Data;
using server.Models;

namespace server.Areas.Projects;

public class ProjectModule : IModule
{
	public IEndpointRouteBuilder RegisterEndpoints(IEndpointRouteBuilder app)
	{
		RouteGroupBuilder group = app.MapGroup("api/projects");

		group.MapGet("", GetAllProjects)
			.WithOpenApi();

		group.MapGet("/{id}", GetProjectById)
			.WithName(nameof(GetProjectById))
			.WithOpenApi();

		group.MapPost("", CreateProject)
			.WithOpenApi();
			
		group.MapPut("/{id}", UpdateProject)
			.WithOpenApi();


		return app;
	}

	private async Task<IResult> UpdateProject(int id, ProjectModel project, ApplicationDbContext dbContext)
	{
    try{
      ProjectModel projectToUpdate = await dbContext.Projects.SingleAsync(p => p.Id == id);
		projectToUpdate.Id = project.Id;
		projectToUpdate.Name = project.Name;
		dbContext.SaveChanges();
		
		return Results.Ok(projectToUpdate);		

    }
    catch(Exception ex){
       throw new Exception(ex.Message); 
    }
      
			}

	private static async Task<IResult> CreateProject(ProjectModel project, ApplicationDbContext dbContext)
	{
    try{
     await dbContext.Projects.AddAsync(project);
		dbContext.SaveChanges();
		return Results.CreatedAtRoute(
			routeName: nameof(GetProjectById),
			routeValues: new { id = project.Id },
			value: project
		);
 
    }
    catch(Exception ex){
      throw new Exception(ex.Message);
    }
			}

	private static async Task<IResult> GetAllProjects(ApplicationDbContext dbContext)
	{
		IEnumerable<ProjectModel> projects = await dbContext.Projects.ToListAsync();
		return Results.Ok(projects);
	}

	private static async Task<IResult> GetProjectById(int id, ApplicationDbContext dbContext)
	{
		ProjectModel project = await dbContext.Projects.SingleAsync(p => p.Id == id);
		return Results.Ok(project);
	}
}
