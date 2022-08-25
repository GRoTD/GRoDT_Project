using Slipp.Services.Constants;
using Slipp.Services.DTO;

namespace Slipp.Services.BlazorServices;

public interface ITicketAPIService
{
    LoggedInUser User { get; }
    Task Initialize();
    Task<IEnumerable<TicketOutput>> GetTickets(Guid? clubId, string? city);
    Task<IEnumerable<TicketOutput>> GetUserTickets(string email);
    Task<TicketOutput> GetTicket(Guid? id);
    Task DeleteTicket(Guid? id);
    Task<IEnumerable<TicketOutput>> GetFavouriteTickets();
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

    public async Task<IEnumerable<TicketOutput>> GetTickets(Guid? clubId, string? city)
    {
        var tickets = await _apiService.Get<IEnumerable<TicketOutput>>(ApiPaths.TICKETCONTROLLER);
        return tickets;
    }

    public async Task<IEnumerable<TicketOutput>> GetUserTickets(string email) //TODO - Create API response
    {
        var path = ApiPaths.TICKETCONTROLLER + "/" + email;
        var tickets = await _apiService.Get<IEnumerable<TicketOutput>>(path);
        return tickets;
    }

    public async Task<IEnumerable<TicketOutput>> GetFavouriteTickets()
    {
        var path = ApiPaths.TICKETCONTROLLER + "/" + User.Email + "/" + "favourites"; //api/ticket/appuser@club.se/favourites
        var tickets = await _apiService.Get<IEnumerable<TicketOutput>>(path);
        return tickets;
    }

    public async Task<TicketOutput> GetTicket(Guid? id)
    {
        var path = ApiPaths.TICKETCONTROLLER + $"/{id}";
        var ticket = await _apiService.Get<TicketOutput>(path);

        return ticket;
    }



    public async Task DeleteTicket(Guid? id)
    {
        throw new NotImplementedException();
    }
}