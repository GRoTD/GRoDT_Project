using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Slipp.Services.Models;
using SlippAPI.DTOs;

namespace Slipp.Services;

public class UserService
{
    private readonly UserManager<DatabaseUser> _userManager;
    private readonly SlippDbCtx _ctx;

    public UserService(UserManager<DatabaseUser> userManager, SlippDbCtx ctx)
    {
        _userManager = userManager;
        _ctx = ctx;
    }

    public async Task<DatabaseUser> CreateAppUser(CreateAppUserInput input)
    {
        AppUser appUser = new AppUser
        {
            FirstName = input.FirstName,
            LastName = input.LastName
        };
        DatabaseUser dbUser = new DatabaseUser
        {
            UserName = input.Email,
            AppUser = appUser
        };

        var result = await _userManager.CreateAsync(dbUser, input.Password);

        if (!result.Succeeded) return null; //TODO: Throw maybe?

        var emailResult = await _userManager.SetEmailAsync(dbUser, input.Email);

        if (!emailResult.Succeeded) return null; //TODO: Throw maybe?

        return await _userManager.FindByEmailAsync(input.Email);
    }

    public async Task<DatabaseUser> GetAppUser(string email)
    {
        var user = await _ctx.Users.Include(u => u.AppUser).FirstOrDefaultAsync(u => u.Email == email);

        if (user is null) return null;

        return user;
    }
}