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

    public async Task<Ticket> GetTicket(Guid id)
    {
        var query = _slippDbCtx.Tickets
            .Include(t => t.Club)
            .Include(t => t.Sale)
            .Include(t => t.Auction)
            .Include(t => t.Images);

        var ticket = await query.FirstOrDefaultAsync(t => t.Id == id);

        if (ticket is null) throw new TicketNotFoundException();

        return ticket;
    }

    public async Task<List<Ticket>> GetUnsoldTicketsWithoutAuctions(Guid? clubId)
    {
        var query = _slippDbCtx.Tickets
            .Include(t => t.Club)
            .Include(t => t.Sale)
            .Include(t => t.Images)
            .Where(t => t.Auction == null)
            .Where(t => t.Sale == null)
            .AsQueryable();

        //TODO: Can add filtering here. Ex clubId below. 
        if (clubId != null) query = query.Where(t => t.Club.Id == clubId);

        var tickets = await query.ToListAsync();

        return tickets;
    }

    public async Task<List<Ticket>> GetUnsoldTicketsWithoutAuctions()
    {
        return await GetUnsoldTicketsWithoutAuctions(null);
    }

    public async Task<List<Ticket>> GetUnsoldTicketsAtCity(string city)
    {
        var query = _slippDbCtx.Tickets
            .Include(t => t.Club)
            .Include(t => t.Sale)
            .Include(t => t.Images)
            .Where(t => t.Auction == null)
            .Where(t => t.Sale == null)
            .Where(t => t.Club.City.ToUpper() == city.ToUpper())
            .AsQueryable();

        var tickets = await query.ToListAsync();

        return tickets;
    }

    /*public async Task<List<Ticket>> GetUnsoldTicketsWithPosition(double lon, double lat, double radius)
    {
        var tickets = await _slippDbCtx.Tickets
            .Include(t => t.Club)
            .Where(t => t.Sale == null)
            .ToListAsync();

        var returnTickets = tickets.Where(t => CalculateDistance(lon, lat, t.Club.Longitude, t.Club.Latitude) <= radius)
            .ToList();

        return returnTickets;
    }*/

    /* private double CalculateDistance(double lon1, double lat1, double lon2, double lat2)
     {
         var d1 = lat1 * (Math.PI / 180.0);
         var num1 = lon1 * (Math.PI / 180.0);
         var d2 = lat2 * (Math.PI / 180.0);
         var num2 = lon2 * (Math.PI / 180.0) - num1;
         var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                  Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
         return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
     }*/
}

public class TicketNotFoundException : Exception
{
    public TicketNotFoundException() : base("There is no ticket with that id")
    {
    }
}