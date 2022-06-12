using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SlippWeb.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
});
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(); //STUDY https://docs.microsoft.com/en-us/aspnet/core/blazor/security/?view=aspnetcore-6.0#authorizeview-component

builder.Services.AddMudServices();

await builder.Build().RunAsync();
