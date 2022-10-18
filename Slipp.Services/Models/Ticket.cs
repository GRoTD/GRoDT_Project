using System.ComponentModel.DataAnnotations.Schema;

namespace Slipp.Services.Models;

public class Ticket
{
    public Guid Id { get; set; }
    public string EventDescription { get; set; }

    [Column(TypeName = "money")] public decimal Price { get; set; }
    public DateTime StartValidTime { get; set; } //Avser när klubben öppnar
    public DateTime EndValidTime { get; set; } //Avser när klubben stänger
    public Club Club { get; set; }
    public Order? Order { get; set; }
    public Auction? Auction { get; set; }
    public List<Image> Images { get; set; }

    public List<AppUser> SavedByUsers { get; set; }
}