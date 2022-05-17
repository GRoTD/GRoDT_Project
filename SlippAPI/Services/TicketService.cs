using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace SlippAPI.Services;

public class TicketService
{
    private readonly SlippDbCtx _slippDbCtx;

    public TicketService(SlippDbCtx slippDbCtx)
    {
        _slippDbCtx = slippDbCtx;
    }

    /// <summary>
    /// Creates tickets for the club
    /// </summary>
    /// <param name="amount">Amount of tickets being createx'd</param>
    /// <param name="ticketInput">Ticket input</param>
    /// <returns>A list of Tickets that have been added to the database</returns>
    public async Task<List<Ticket>> CreateTickets(Guid clubId, int amount, CreateTicketInput ticketInput)
    {
        var dbClub = await _slippDbCtx.Clubs.FindAsync(clubId);

        //TODO: Instead Throw exception to show what actually went wrong.
        if (dbClub is null) return null;

        var tickets = new List<Ticket>();

        for (int i = 0; i < amount; i++)
        {
            var ticket = ticketInput.CreateTicket(dbClub);

            tickets.Add(ticket);
        }

        await _slippDbCtx.AddRangeAsync(tickets);
        var result = await _slippDbCtx.SaveChangesAsync();

        //TODO: Check if some goes in to the db, or all goes in. Instead Throw exception to show what actually went wrong.
        if (result != amount) return null;
        return tickets;
    }

    public async Task<List<Ticket>> GetTicketsWithoutAuctions(Guid? clubId) //Add lon, lat, radius
    {
        var query = _slippDbCtx.Tickets
            .Include(t => t.Club)
            .Include(t => t.Sale)
            .Where(t => t.Auction == null)
            .AsQueryable();

        //TODO: Can add filtering here. Ex clubId below. 
        if (clubId != null) query = query.Where(t => t.Club.Id == clubId);

        var tickets = await query.ToListAsync();

        return tickets;
    }

    public async Task<List<Ticket>> GetTicketsWithoutAuctions()
    {
        return await GetTicketsWithoutAuctions(null);
    }


    public async Task<Ticket> GetTicket(Guid id)
    {
        var query = _slippDbCtx.Tickets
            .Include(t => t.Club)
            .Include(t => t.Sale)
            .Include(t => t.Auction);

        var ticket = await query.FirstOrDefaultAsync(t => t.Id == id);

        if (ticket is null) throw new TicketNotFoundException("There is no ticket with that id");

        return ticket;
    }
}

public class TicketNotFoundException : Exception
{
    public TicketNotFoundException(string message) : base(message)
    {
    }
}