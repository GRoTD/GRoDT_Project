using System.ComponentModel.DataAnnotations.Schema;

namespace Slipp.Services.Models;

public class Bid
{
    public int Id { get; set; }
    [Column(TypeName = "money")] public decimal BidAmountPerTicket { get; set; }
    public DateTime BidDateTime { get; set; }
    public AppUser AppUser { get; set; }
    public Auction Auction { get; set; }
}