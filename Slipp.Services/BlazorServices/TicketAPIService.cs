using Slipp.Services.Constants;
using Slipp.Services.DTO;
using Microsoft.AspNetCore.WebUtilities;

namespace Slipp.Services.BlazorServices;

public interface ITicketAPIService
{
    LoggedInUser User { get; }
    Task Initialize();
    Task<IEnumerable<CreateTicketOutput>> GetTickets(Guid? clubId, string? city);
    Task<CreateTicketOutput> GetTicket(Guid id);
    Task<CreateTicketOutput> DeleteTicket(Guid id);
}

public class TicketAPIService : ITicketAPIService
{
    private readonly IApiService _apiService;
    private readonly ILocalStorageService _localStorageService;
    private readonly string _path = ApiPaths.TICKETCONTROLLER;

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
        IEnumerable<CreateTicketOutput> tickets;

        var queries = new Dictionary<string, string>();

        string path = _path;

        if (clubId != null) queries.Add("clubId", clubId.ToString());
        if (city != null) queries.Add("city", city);
        if (queries.Count > 0) path = QueryHelpers.AddQueryString(path, queries);

        tickets = await _apiService.Get<IEnumerable<CreateTicketOutput>>(path);


        return tickets;
    }

    public async Task<CreateTicketOutput> GetTicket(Guid id)
    {
        var path = _path + $"/{id}";
        var ticket = await _apiService.Get<CreateTicketOutput>(path);

        return ticket;
    }

    public async Task<CreateTicketOutput> DeleteTicket(Guid id)
    {
        var path = _path + $"/{id}";
        var ticket = await _apiService.Delete<CreateTicketOutput>(path);

        return ticket;
    }
}