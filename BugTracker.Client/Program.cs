using BugTracker.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.ConfigureSharedServices();
await builder.Build().RunAsync();
