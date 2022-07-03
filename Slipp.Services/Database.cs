using Microsoft.AspNetCore.Identity;
using Slipp.Services.Models;

namespace Slipp.Services;

public class Database
{
    private readonly SlippDbCtx _slippDbCtx;
    private readonly UserManager<DatabaseUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public Database(SlippDbCtx slippDbCtx, UserManager<DatabaseUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _slippDbCtx = slippDbCtx;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedIfPrductionAndDbIsNotCreated()
    {
        var created = await _slippDbCtx.Database.EnsureCreatedAsync();

        if (!created) await Seed();
    }

    public async Task RecreateAndSeed()
    {
        await _slippDbCtx.Database.EnsureDeletedAsync();
        await _slippDbCtx.Database.EnsureCreatedAsync();
        await Seed();
    }

    public async Task Seed()
    {
        foreach (var role in StaticConfig.Roles)
        {
            var r = new IdentityRole(role);
            await _roleManager.CreateAsync(r);
        }

        var company = new Company
        {
            CompanyName = "Första Test Företaget"
        };

        var club = new Club
        {
            Adress = "Första Långgatan 1",
            City = "Göteborg",
            Company = company,
            Description = "Coolaste klubben på Jorden",
            Name = "EC Klubb"
        };


        await _slippDbCtx.AddAsync(club);
        var res = await _slippDbCtx.SaveChangesAsync();

        var clubUser = new DatabaseUser
        {
            Club = club,
            UserName = "clubUser@club.se",
            Email = "clubUser@club.se"
        };

        var AppUser = new DatabaseUser
        {
            UserName = "appUser@club.se", Email = "appUser@club.se"
        };

        var CompanyUser = new DatabaseUser
        {
            Company = company,
            UserName = "companyUser@club.se",
            Email = "companyUser@club.se"
        };


        await _userManager.CreateAsync(AppUser, "Passw0rd!");
        await _userManager.CreateAsync(clubUser, "Passw0rd!");
        await _userManager.CreateAsync(CompanyUser, "Passw0rd!");

        await _userManager.AddToRoleAsync(AppUser, StaticConfig.AppUserRole);
        await _userManager.AddToRoleAsync(clubUser, StaticConfig.ClubRole);
        await _userManager.AddToRoleAsync(CompanyUser, StaticConfig.CompanyRole);


        List<Ticket> ticketsAuction1 = new List<Ticket>
        {
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                Title = "Ticket 1 - Auction 1"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                Title = "Ticket 2 - Auction 1"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                Title = "Ticket 3 - Auction 1"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                Title = "Ticket 4 - Auction 1"
            },
        };
        List<Ticket> ticketsAuction2 = new List<Ticket>
        {
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                Title = "Ticket 1 - Auction 2"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                Title = "Ticket 2 - Auction 2"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                Title = "Ticket 3 - Auction 2"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                Title = "Ticket 4 - Auction 2"
            },
        };

        List<Ticket> ticketsAuction3 = new List<Ticket>
        {
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                Title = "Ticket 1 - Auction 3"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                Title = "Ticket 2 - Auction 3"
            }
        };

        List<Auction> auctions = new List<Auction>
        {
            new()
            {
                Club = club,
                ExpiryDateTime = DateTime.Today + TimeSpan.FromHours(22),
                Title = "Auction number 1",
                IssueDateTime = DateTime.Today + TimeSpan.FromHours(20),
                Tickets = ticketsAuction1
            },
            new()
            {
                Club = club,
                ExpiryDateTime = DateTime.Today + TimeSpan.FromHours(23),
                Title = "Auction number 2",
                IssueDateTime = DateTime.Today + TimeSpan.FromHours(21),
                Tickets = ticketsAuction2
            },
            new()
            {
                Club = club,
                ExpiryDateTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(22),
                Title = "Auction number 3",
                IssueDateTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(20),
                Tickets = ticketsAuction3
            },
        };

        #region Tickets without Auctions

        List<Ticket> ticketsWithoutAuction = new List<Ticket>
        {
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 150,
                Title = "Ticket 1 - No Auction"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 150,
                Title = "Ticket 2 - No Auction"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 150,
                Title = "Ticket 3 - No Auction"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 150,
                Title = "Ticket 4 - No Auction"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 150,
                Title = "Ticket 5 - No Auction"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 150,
                Title = "Ticket 6 - No Auction"
            }
        };

        #endregion

        _slippDbCtx.AddRange(ticketsWithoutAuction);
        _slippDbCtx.AddRange(auctions);
        var e = await _slippDbCtx.SaveChangesAsync();
    }
}