using Microsoft.AspNetCore.Identity;
using Slipp.Services.Models;

namespace Slipp.Services;

public class Database
{
    private readonly SlippDbCtx _slippDbCtx;
    private readonly UserManager<DatabaseUser> _userManager;

    public Database(SlippDbCtx slippDbCtx, UserManager<DatabaseUser> userManager)
    {
        _slippDbCtx = slippDbCtx;
        _userManager = userManager;
    }

    public async Task RecreateAndSeed()
    {
        await _slippDbCtx.Database.EnsureDeletedAsync();
        await _slippDbCtx.Database.EnsureCreatedAsync();
        await Seed();
    }

    public async Task Seed()
    {
        var company = new Company
        {
            CompanyName = "Första Test Företaget"
        };

        var club = new Club
        {
            Adress = "Första Långgatan 1",
            Company = company,
            Description = "Coolaste klubben på Jorden",
            Name = "EC Klubb"
        };

        await _slippDbCtx.AddAsync(club);
        await _slippDbCtx.SaveChangesAsync();
    }
}