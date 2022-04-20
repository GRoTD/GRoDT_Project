using SlippAPI.DTOs;

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
    /// <param name="club">Club that issued the ticket</param>
    /// <returns>A list of Tickets that have been added to the database</returns>
    public async Task<List<Ticket>> CreateTickets(int amount, CreateTicketInput ticketInput)
    {
        var dbClub = await _slippDbCtx.Clubs.FindAsync(ticketInput.ClubId);

        //TODO: Instead Throw exception to show what actually went wrong.
        if (dbClub is null) return null;

        var tickets = new List<Ticket>();

        for (int i = 0; i < amount; i++)
        {
            var ticket = new Ticket
            {
                Club = dbClub,
                Title = ticketInput.Title,
                Price = ticketInput.Price,
                StartValidTime = ticketInput.StartValidTime,
                EndValidTime = ticketInput.EndValidTime
            };

            tickets.Add(ticket);
        }

        await _slippDbCtx.AddRangeAsync(tickets);
        var result = await _slippDbCtx.SaveChangesAsync();

        //TODO: Check if some goes in to the db, or all goes in. Instead Throw exception to show what actually went wrong.
        if (result != amount) return null;
        return tickets;
    }
}