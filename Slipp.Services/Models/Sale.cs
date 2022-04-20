namespace Slipp.Services.Models;

public class Sale
{
    public int Id { get; set; }
    public bool IsPayed { get; set; }
    public DateTime PaymentDeadline { get; set; }
    public DateTime BoughtDateTime { get; set; }
    public AppUser Buyer { get; set; }
    public List<Ticket> Tickets { get; set; }
}