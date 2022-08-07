using System.ComponentModel.DataAnnotations.Schema;

namespace Slipp.Services.Models;

public class Ticket
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    [Column(TypeName = "money")] public decimal Price { get; set; }
    public DateTime StartValidTime { get; set; }
    public DateTime EndValidTime { get; set; }
    public Club Club { get; set; }
    public Order? Order { get; set; }
    public Auction? Auction { get; set; }
    public List<Image> Images { get; set; }
    public List<AppUser> SavedByUsers { get; set; }
}