using System.Security.AccessControl;

namespace Slipp.Services.Models;

public class Auction
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime IssueDateTime { get; set; }
    public DateTime ExpiryDateTime { get; set; }
    public List<Bid> Bids { get; set; }
    public List<Ticket> Tickets { get; set; }
    public Club Club { get; set; }
}