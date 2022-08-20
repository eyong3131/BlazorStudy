global using TestDrive.Shared;
using TestDrive.Client.Services.TestDriveServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TestDrive.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// GetList Services
builder.Services.AddScoped<ITestDriveService, TestDriveService>();

await builder.Build().RunAsync();
