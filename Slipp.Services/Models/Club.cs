namespace Slipp.Services.Models;

public class Club
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Adress { get; set; }
    public string Description { get; set; }
    public Company Company { get; set; }
    public List<Ticket>? Tickets { get; set; }
    public List<Auction>? Auctions { get; set; }
}