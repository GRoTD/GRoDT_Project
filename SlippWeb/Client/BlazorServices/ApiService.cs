using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Slipp.Services.DTO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SlippWeb.Client.BlazorServices;

public interface IApiService
{
    Task<T> Get<T>(string uri, string token = "");
    Task<T> Post<T>(string uri, object value, string token = "");
    Task<T> Put<T>(string uri, string token = "");
}

public class ApiService : IApiService
{
    private HttpClient _httpClient;
    private NavigationManager _navigationManager;
    private IConfiguration _configuration;

    public ApiService(
        HttpClient httpClient,
        NavigationManager navigationManager,
        IConfiguration configuration
    )
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _configuration = configuration;
    }

    public async Task<T> Get<T>(string uri, string token = "")
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request, token);
    }

    public async Task<T> Post<T>(string uri, object value, string token = "")
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await SendRequest<T>(request, token);
    }

    public async Task<T> Put<T>(string uri, string token = "")
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri);
        return await SendRequest<T>(request, token);
    }


    // helper methods

    private async Task<T> SendRequest<T>(HttpRequestMessage request, string token = "")
    {
        // add jwt auth header if user is logged in and request is to the api url

        var isApiUrl = !request.RequestUri.IsAbsoluteUri;

        if (isApiUrl)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        using var response = await _httpClient.SendAsync(request);

        // auto nav to home on 401 response
        //TODO: Länka till en logout så småningom.
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("/");
            return default;
        }

        // throw exception on error response
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new Exception(error["message"]);
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }
}