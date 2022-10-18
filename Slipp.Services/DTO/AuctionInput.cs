using Slipp.Services.Models;

namespace Slipp.Services.DTO;

public class AuctionInput
{
    public string Title { get; set; }
    public DateTime ExpiryDateTime { get; set; }
    public List<TicketInput> Tickets { get; set; }

    public Auction CreateAuction(Club club)
    {
        var auction = new Auction()
        {
            Club = club,
            ExpiryDateTime = ExpiryDateTime,
            IssueDateTime = DateTime.Now,
            Tickets = Tickets.Select(ticket => ticket.CreateTicket(club)).ToList(),
            Title = Title
        };
        return auction;
    }
}