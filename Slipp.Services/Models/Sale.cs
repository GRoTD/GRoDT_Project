namespace Slipp.Services.Models;

public class Sale
{
    public Guid Id { get; set; }
    public DateTime BoughtDateTime { get; set; }
    public Order Order { get; set; }
}