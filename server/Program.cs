using Server;
using Server.Helpers;
using System.Reflection;
using Server.Areas.Projects;
using Server.Authentication;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

builder.Services.AddSwaggerGen(c => 
{	
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

var env = app.Environment;

if (env.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("AllowOrigins");

app.UseStaticFiles();

app.UseAntiforgery();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapProjectEndpoints();
app.MapAuthEndpoints();

await app.SeedRoles();

app.Run();

