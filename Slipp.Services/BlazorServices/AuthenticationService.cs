using Microsoft.AspNetCore.Components;
using Slipp.Services.DTO;

namespace Slipp.Services.BlazorServices;

public interface IAuthenticationService
{
    LoggedInUser User { get; }
    Task Initialize();
    Task Login(string username, string password);
    Task Logout();
}

public class AuthenticationService : IAuthenticationService
{
    private IApiService _apiService;
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;

    public LoggedInUser User { get; private set; }

    public AuthenticationService(
        IApiService apiService,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService
    )
    {
        _apiService = apiService;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
    }

    public async Task Initialize()
    {
        User = await _localStorageService.GetItem<LoggedInUser>("user");
    }

    public async Task Login(string username, string password)
    {
        LoginInput input = new() {Email = username, Password = password};
        User = await _apiService.Post<LoggedInUser>("/api/login",
            input); //TODO: Rätt uri här
        await _localStorageService.SetItem("user", User);
    }

    public async Task Logout()
    {
        User = null;
        await _localStorageService.RemoveItem("user");
        _navigationManager.NavigateTo("login");
    }
}