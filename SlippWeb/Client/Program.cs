using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SlippWeb.Client.BlazorServices;
using SlippWeb.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<IApiService, ApiService>(client =>
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]));

builder.Services
    .AddSingleton<IAuthenticationService, AuthenticationService>()
    .AddSingleton<ITicketAPIService, TicketAPIService>()
    .AddSingleton<ILocalStorageService, LocalStorageService>()
    .AddSingleton<OrderAPIService>()
    .AddSingleton<PageHistoryState>();


builder.Services.AddMudServices();

var host = builder.Build();

var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
await authenticationService.Initialize();


await host.RunAsync();