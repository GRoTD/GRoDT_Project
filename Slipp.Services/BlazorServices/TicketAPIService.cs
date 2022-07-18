﻿using Slipp.Services.Constants;
using Slipp.Services.DTO;
using Slipp.Services.Models;

namespace Slipp.Services.BlazorServices;

public interface ITicketAPIService
{
    LoggedInUser User { get; }
    Task Initialize();
    Task<IEnumerable<CreateTicketOutput>> GetTickets(Guid? clubId, string? city);
    Task <CreateTicketOutput> GetTicket(Guid? id);
    Task DeleteTicket(Guid? id);
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

    public Task<IEnumerable<CreateTicketOutput>> GetTickets(Guid? clubId, string? city)
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

    public Task DeleteTicket(Guid? id)
    {
        throw new NotImplementedException();
    }
}