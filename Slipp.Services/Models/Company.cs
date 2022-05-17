namespace Slipp.Services.Models;

public class Company
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public List<DatabaseUser> Users { get; set; }
    public List<Club> Clubs { get; set; }
}