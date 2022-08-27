using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Slipp.Services.BlazorServices;

namespace Slipp.Services.Helpers;

public class AppRouteView : RouteView
{
    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public IAuthenticationAPIService AuthenticationApiService { get; set; }

    protected override void Render(RenderTreeBuilder builder)
    {
        var authorize = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) != null;
        if (authorize && AuthenticationApiService.User == null)
        {
            var returnUrl = WebUtility.UrlEncode(new Uri(NavigationManager.Uri).PathAndQuery);
            NavigationManager.NavigateTo($"login?returnUrl={returnUrl}");
        }
        else
        {
            base.Render(builder);
        }
    }
}