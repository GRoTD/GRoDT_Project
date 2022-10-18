using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Slipp.Services.Models;

namespace Slipp.Services;

public class SlippDbCtx : IdentityDbContext<DatabaseUser, IdentityRole, string>
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Image> Images { get; set; }

    public SlippDbCtx(DbContextOptions<SlippDbCtx> options)
        : base(options)
    {
    }
}