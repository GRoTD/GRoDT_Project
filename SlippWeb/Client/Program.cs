using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Slipp.Services.BlazorServices;
using SlippWeb.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

builder.Services.AddHttpClient<IApiService, ApiService>(client =>
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]));

builder.Services
    .AddScoped<IAuthenticationService, AuthenticationService>()
    .AddScoped<ITicketAPIService, TicketAPIService>()
    .AddScoped<ILocalStorageService, LocalStorageService>();

builder.Services.AddMudServices();

var host = builder.Build();

var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
await authenticationService.Initialize();
var ticketService = host.Services.GetRequiredService<ITicketAPIService>();
await ticketService.Initialize();

await host.RunAsync();