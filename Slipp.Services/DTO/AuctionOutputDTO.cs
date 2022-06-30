namespace Slipp.Services.DTO;

public class AuctionOutputDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime IssueDateTime { get; set; }
    public DateTime ExpiryDateTime { get; set; }
    public List<BidOutputDTO> Bids { get; set; } //Make a link to it instead https://slipp.se/api/auctions/id/bids eg.

    public List<CreateTicketOutput>
        Tickets
    { get; set; } //Make a link to it instead https://slipp.se/api/auctions/id/tickets eg.

    public Guid ClubId { get; set; } //Make a link to it instead https://slipp.se/api/clubs/clubId eg.
}