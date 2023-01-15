using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.EntityFrameworkCore;
using Slipp.Services.Models;
using SlippAPI.Options;

namespace SlippAPI.Services;

public class TicketService
{
    private readonly SlippDbCtx _slippDbCtx;
    private readonly FirebaseClient _firebaseClient;
    private readonly RealtimeDbOptions _realtimeDbOptions;

    public TicketService(SlippDbCtx slippDbCtx, FirebaseClient firebaseClient, RealtimeDbOptions realtimeDbOptions)
    {
        _slippDbCtx = slippDbCtx;
        _firebaseClient = firebaseClient;
        _realtimeDbOptions = realtimeDbOptions;
    }

    /// <summary>
    /// Creates tickets for the club
    /// </summary>
    /// <param name="amount">Amount of tickets being createx'd</param>
    /// <param name="ticketInput">Ticket input</param>
    /// <returns>A list of Tickets that have been added to the database</returns>
    public async Task<List<Ticket>> CreateTickets(Guid clubId, int amount, TicketInput ticketInput)
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
        var ticket = await _firebaseClient.Child("tickets").Child(id.ToString()).OnceSingleAsync<Ticket>();

        if (ticket == null) throw new TicketNotFoundException();
        return ticket;
    }

    public async Task<List<Ticket>> GetAvailableTickets(DateTime? date = null, City? city = null)
    {
        var firebaseCollection = await _firebaseClient.Child("tickets").OnceAsync<Ticket>();
        var ticketsCollection = firebaseCollection.Select(x => x.Object).ToList();

        if (date == null)
        {
            ticketsCollection = ticketsCollection.Where(x => x.StartValidTime > DateTime.Now && x.StartValidTime < DateTime.Now.AddDays(1)).ToList();
        }
        else
        {
            ticketsCollection = ticketsCollection.Where(x => x.StartValidTime > date && x.StartValidTime < date.Value.AddDays(1)).ToList();
        }

        if (city == null)
        {
            return ticketsCollection;
        }

        return ticketsCollection.Where(x => x.Club.City == city).ToList();
    }

    //public async Task<Ticket> GetTicket1(Guid id)
    //{
    //    var query = _slippDbCtx.Tickets
    //        .Include(t => t.Club)
    //        .Include(t => t.Order).ThenInclude(o => o.Sale)
    //        .Include(t => t.Auction)
    //        .Include(t => t.Images);

    //    var ticket = await query.FirstOrDefaultAsync(t => t.Id == id);

    //    if (ticket is null) throw new TicketNotFoundException();

    //    return ticket;
    //}

    public async Task<List<Ticket>> GetUnsoldTicketsWithoutAuctions(Guid? clubId)
    {
        var query = _slippDbCtx.Tickets
            .Include(t => t.Club)
            .Include(t => t.Order).ThenInclude(o => o.Sale)
            .Include(t => t.Images)
            .Where(t => t.Auction == null)
            .Where(t => t.Order.Sale == null)
            .OrderBy(t => t.StartValidTime)
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
            .Include(t => t.Order).ThenInclude(o => o.Sale)
            .Include(t => t.Images)
            .Where(t => t.Auction == null)
            .Where(t => t.Order.Sale == null)
            .Where(t => t.Club.City.ToUpper() == city.ToUpper())
            .AsQueryable();

        var tickets = await query.ToListAsync();

        return tickets;
    }

    public async Task<bool> DeleteTicket(Guid id)
    {
        var ticket = new Ticket() {Id = id};

        _slippDbCtx.Tickets.Remove(ticket);

        await _slippDbCtx.SaveChangesAsync();

        return true;
    }

    public async Task<List<Ticket>> GetUserTickets(string email)
    {
        var query = _slippDbCtx.Tickets
            .Include(t => t.Club)
            .Include(t => t.Order).ThenInclude(o => o.Sale)
            .Include(t => t.Images)
            .AsQueryable();

        var tickets = await query.Where(t => t.Order.AppUser.DatabaseUser.NormalizedEmail == email.ToUpper())
            .ToListAsync();

        return tickets;
    }

    public async Task<List<Ticket>> GetUser
        (string email)
    {
        var query = _slippDbCtx.AppUsers
            .Include(u => u.FavouriteTickets)
            .ThenInclude(t => t.Club)
            .Include(u => u.FavouriteTickets)
            .ThenInclude(t => t.Order).ThenInclude(o => o.Sale)
            .Include(u => u.FavouriteTickets)
            .ThenInclude(t => t.Images)
            .AsQueryable();


        var user = await query.FirstOrDefaultAsync(u => u.DatabaseUser.NormalizedEmail == email.ToUpper());


        if (user == null) return null; //TODO Change to handleException
        var tickets = user.FavouriteTickets;

        return tickets;
    }
}

public class TicketNotFoundException : Exception
{
    public TicketNotFoundException() : base("There is no ticket with that id")
    {
    }
}