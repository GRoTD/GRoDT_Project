using Microsoft.AspNetCore.Components;
using Slipp.Services.Constants;
using Slipp.Services.DTO;

namespace Slipp.Services.BlazorServices;

public interface IAuthenticationService
{
    LoggedInUser User { get; }
    public event Action? OnChange;
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
    public event Action? OnChange;

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
        NotifyStateChanged();
    }

    public async Task Logout()
    {
        User = null;
        await _localStorageService.RemoveItem("user");
        NotifyStateChanged();
        _navigationManager.NavigateTo("login");
    }

    public async Task Register(CreateAppUserInput newUser)
    {
        await _apiService.Post<CreatedAppUserReturn>(
            ApiPaths.APPUSERCONTROLLER, newUser);
        _navigationManager.NavigateTo("/login");
    }

    public async Task ToggleFavouriteTicket(Guid ticketId)
    {
        var path = "/favourites/" + ticketId; //TODO: Ändra till den nya Pathen när den är skapad.
        await _apiService.Put<bool>(path);
    }


    private void NotifyStateChanged() => OnChange?.Invoke();
}