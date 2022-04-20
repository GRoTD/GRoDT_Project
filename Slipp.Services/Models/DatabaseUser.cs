using Microsoft.AspNetCore.Identity;

namespace Slipp.Services.Models;

public class DatabaseUser : IdentityUser
{
    public AppUser? AppUser { get; set; }
    public Company? Company { get; set; }
    public Club? Club { get; set; }
}