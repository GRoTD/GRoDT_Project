using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Slipp.Services.DTO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Slipp.Services.BlazorServices;

public interface IApiService
{
    Task<T> Get<T>(string uri);
    Task<T> Post<T>(string uri, object value);
    Task<T> Delete<T>(string uri);
}

public class ApiService : IApiService
{
    private HttpClient _httpClient;
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;
    private IConfiguration _configuration;

    public ApiService(
        HttpClient httpClient,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService,
        IConfiguration configuration
    )
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        _configuration = configuration;
    }

    public async Task<T> Get<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request);
    }

    public async Task<T> Post<T>(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await SendRequest<T>(request);
    }

    public async Task<T> Delete<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri); //TODO: Test this one!
        return await SendRequest<T>(request);
    }

    // helper methods

    private async Task<T> SendRequest<T>(HttpRequestMessage request)
    {
        // add jwt auth header if user is logged in and request is to the api url
        var user = await _localStorageService.GetItem<LoggedInUser>("user");
        var isApiUrl = !request.RequestUri.IsAbsoluteUri;
        if (user != null && isApiUrl)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

        using var response = await _httpClient.SendAsync(request);

        // auto logout on 401 response
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("logout");
            return default;
        }

        // throw exception on error response
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw
                new Exception(
                    error["message"]); //Send only the message of the exception, so to easier see what happened.
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }
}