using System.Reflection;
using server;
using server.Contracts;
using server.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

builder.Services.AddSwaggerGen();

var app = builder.Build();

var env = app.Environment;

if(env.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();

app.UseStaticFiles();

app.UseAntiforgery();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

await app.SeedRoles();

RegisterAllEndpoints(app);

app.Run();

static IEndpointRouteBuilder RegisterAllEndpoints(IEndpointRouteBuilder app)
{
	var types = Assembly.GetExecutingAssembly().GetTypes()
		.Where(t => t.GetInterfaces().Contains(typeof(IModule)) && !t.IsAbstract);
		
	foreach (var type in types)
	{
		var instance = Activator.CreateInstance(type) as IModule;
		instance?.RegisterEndpoints(app);
 
	}

	return app;
}

