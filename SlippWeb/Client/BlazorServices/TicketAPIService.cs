using Slipp.Services.Constants;
using Slipp.Services.DTO;


namespace SlippWeb.Client.BlazorServices;

public interface ITicketAPIService
{
    Task<IEnumerable<IEnumerable<TicketOutput>>> GetTickets(Guid? clubId, string? city);
    Task<IEnumerable<TicketOutput>> GetUserTickets(string email);
    Task<TicketOutput> GetTicket(Guid? id);
    Task DeleteTicket(Guid? id);
    Task<IEnumerable<TicketOutput>> GetFavouriteTickets();
}

public class TicketAPIService : ITicketAPIService
{
    private readonly IApiService _apiService;
    private readonly IAuthenticationService _authentication;

    public TicketAPIService(
        IApiService apiService,
        IAuthenticationService authentication
    )
    {
        _apiService = apiService;
        _authentication = authentication;
    }
  
    public async Task<IEnumerable<IEnumerable<TicketOutput>>> GetTickets(Guid? clubId, string? city)
    {
        var tickets = await _apiService.Get<IEnumerable<IEnumerable<TicketOutput>>>(ApiPaths.TICKETCONTROLLER);
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
        var path = ApiPaths.TICKETCONTROLLER + "/" + _authentication.User.Email + "/" +
                   "favourites"; //api/ticket/appuser@club.se/favourites
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