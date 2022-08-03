using Microsoft.AspNetCore.Components;
using System.Net;


namespace SlippWeb.Client.Utils
{
    public class NavigationUtil
    {
        private static NavigationManager _navigationManager;

        public NavigationUtil(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        /// <summary>
        /// Method takes page url and optional query and navigates to that page and includes a return URL.
        /// </summary>
        /// <param name="pageUrl">Page url path to navigate to as a string</param>
        /// <param name="pageQuery">Page query as a string or empty</param>
        public static void GoToNextpageIncludeReturnUrl(string pageUrl,
            string? pageQuery)
        {

            var goToUrl = pageUrl + "/" + pageQuery;

            var returnUrl = WebUtility.UrlEncode(new Uri(_navigationManager.Uri).PathAndQuery);

            _navigationManager.NavigateTo(
                  $"{goToUrl}?returnUrl={returnUrl}");

        }

    }
}
