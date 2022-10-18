using System.ComponentModel.DataAnnotations.Schema;

namespace Slipp.Services.Models;

public class Sale
{
    public Guid Id { get; set; }
    public DateTime BoughtDateTime { get; set; }
    public Guid OrderId { get; set; }
    [ForeignKey(nameof(OrderId))] public Order Order { get; set; }
}