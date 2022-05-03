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
            Company = company,
            Description = "Coolaste klubben på Jorden",
            Name = "EC Klubb"
        };

        var clubUser = new DatabaseUser
        {
            Club = club,
            UserName = "clubUser@club.se"
        };

        var AppUser = new DatabaseUser
        {
            UserName = "appUser@club.se"
        };

        var CompanyUser = new DatabaseUser
        {
            Company = company,
            UserName = "companyUser@club.se"
        };


        await _userManager.CreateAsync(AppUser, "Passw0rd!");
        await _userManager.CreateAsync(clubUser, "Passw0rd!");
        await _userManager.CreateAsync(CompanyUser, "Passw0rd!");

        await _userManager.AddToRoleAsync(AppUser, StaticConfig.AppUserRole);
        await _userManager.AddToRoleAsync(clubUser, StaticConfig.ClubRole);
        await _userManager.AddToRoleAsync(CompanyUser, StaticConfig.CompanyRole);

        await _userManager.SetEmailAsync(AppUser, AppUser.UserName);
        await _userManager.SetEmailAsync(clubUser, clubUser.UserName);
        await _userManager.SetEmailAsync(CompanyUser, CompanyUser.UserName);

        await _slippDbCtx.AddAsync(club);
        await _slippDbCtx.SaveChangesAsync();
    }
}