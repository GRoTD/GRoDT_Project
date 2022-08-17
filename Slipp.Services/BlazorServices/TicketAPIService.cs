using Slipp.Services.Constants;
using Slipp.Services.DTO;

namespace Slipp.Services.BlazorServices;

public interface ITicketAPIService
{
    LoggedInUser User { get; }
    Task Initialize();
    Task<IEnumerable<CreateTicketOutput>> GetTickets(Guid? clubId, string? city);
    Task<IEnumerable<CreateTicketOutput>> GetUserTickets(string email);
    Task<CreateTicketOutput> GetTicket(Guid? id);
    Task DeleteTicket(Guid? id);
    Task<IEnumerable<CreateTicketOutput>> GetFavouriteTickets(object? o, object? o1);
}

public class TicketAPIService : ITicketAPIService
{
    private readonly IApiService _apiService;
    private readonly ILocalStorageService _localStorageService;

    public TicketAPIService(
        IApiService apiService,
        ILocalStorageService localStorageService
    )
    {
        _apiService = apiService;
        _localStorageService = localStorageService;
    }

    public LoggedInUser User { get; private set; }

    public async Task Initialize()
    {
        User = await _localStorageService.GetItem<LoggedInUser>("user");
    }

    public async Task<IEnumerable<CreateTicketOutput>> GetTickets(Guid? clubId, string? city)
    {
        var tickets = await _apiService.Get<IEnumerable<CreateTicketOutput>>(ApiPaths.TICKETCONTROLLER);
        return tickets;
    }

    public async Task<IEnumerable<CreateTicketOutput>> GetUserTickets(string email) //TODO - Create API response
    {
        var path = ApiPaths.TICKETCONTROLLER + "/" + email;
        var tickets = await _apiService.Get<IEnumerable<CreateTicketOutput>>(path);
        return tickets;
    }

    public Task<IEnumerable<CreateTicketOutput>> GetFavouriteTickets(object? o, object? o1)
    {
        var tickets = _apiService.Get<IEnumerable<CreateTicketOutput>>(ApiPaths.TICKETCONTROLLER);
        return tickets;
    }

    public async Task<CreateTicketOutput> GetTicket(Guid? id)
    {
        var path = ApiPaths.TICKETCONTROLLER + $"/{id}";
        var ticket = await _apiService.Get<CreateTicketOutput>(path);

        return ticket;
    }

    public async Task DeleteTicket(Guid? id)
    {
        throw new NotImplementedException();
    }
}