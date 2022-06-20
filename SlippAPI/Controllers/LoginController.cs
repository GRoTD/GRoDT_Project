using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlippAPI.Services;

namespace SlippAPI.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly UserManager<DatabaseUser> _userManager;
    private readonly JwtService _jwtService;

    public LoginController(UserManager<DatabaseUser> userManager, JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost]
    public async Task<ActionResult> LoginUser(LoginInput input)
    {
        //TODO: return a JWT with a loginManager service.

        var user = await _userManager.Users
            .Include(u => u.AppUser)
            .Include(u => u.Club)
            .Include(u => u.Company)
            .FirstOrDefaultAsync(u => u.Email == input.Email);

        if (user == null)
            return BadRequest("Failed login attempt");

        var loginResult = await _userManager.CheckPasswordAsync(user, input.Password);
        if (!loginResult)
            return BadRequest("Failed login attempt");

        var token = await _jwtService.GenerateJwtToken(user);

        var loggedInUser = new LoggedInUser()
        {
            Token = token,
            Email = user.NormalizedEmail,
            FirstName = user.AppUser?.FirstName,
            LastName = user.AppUser?.LastName
        };

        return Ok(loggedInUser);
    }
}