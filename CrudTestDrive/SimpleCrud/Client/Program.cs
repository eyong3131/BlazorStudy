global using SimpleCrud.Shared.Person;
global using SimpleCrud.Client.Services.PersonServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SimpleCrud.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Client Services
builder.Services.AddScoped<IPersonService, PersonService>();

await builder.Build().RunAsync();
