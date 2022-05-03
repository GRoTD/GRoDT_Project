using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slipp.Services.Models;

public class AppUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Sale> Sales { get; set; }
    public List<Bid> Bids { get; set; }
    public string Id { get; set; }
    [Key][ForeignKey("Id")] public DatabaseUser DatabaseUser { get; set; }
}