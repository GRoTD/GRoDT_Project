namespace Slipp.Services.Models;

public class Club
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Adress { get; set; }
    public string Website { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Description { get; set; }
    public Company Company { get; set; }
    public List<Image> Images { get; set; }
    public List<Ticket>? Tickets { get; set; }
    public List<Auction>? Auctions { get; set; }
}