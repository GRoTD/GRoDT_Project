using Microsoft.AspNetCore.Components;
using Slipp.Services.Constants;
using Slipp.Services.DTO;

namespace Slipp.Services.BlazorServices;

public interface IAuthenticationService
{
    LoggedInUser User { get; }
    Task Initialize();
    Task Login(LoginInput input);
    Task Logout();
    Task Register(CreateAppUserInput newUser);
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

    public async Task Login(LoginInput input)
    {
        User = await _apiService.Post<LoggedInUser>(ApiPaths.LOGINUSER,
            input); //TODO: Rätt uri här
        await _localStorageService.SetItem("user", User);
    }

    public async Task Logout()
    {
        User = null;
        await _localStorageService.RemoveItem("user");
        _navigationManager.NavigateTo("login");
    }

    public async Task Register(CreateAppUserInput newUser)
    {
        await _apiService.Post<CreatedAppUserReturn>(
              ApiPaths.APPUSERCONTROLLER, newUser);
        _navigationManager.NavigateTo("login");
    }

}