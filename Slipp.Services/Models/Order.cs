namespace Slipp.Services.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Sale? Sale { get; set; }
    public AppUser Buyer { get; set; }
    public List<Ticket> Tickets { get; set; }
}