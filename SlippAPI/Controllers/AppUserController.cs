using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slipp.Services.Constants;

namespace SlippAPI.Controllers;

[Route(ApiPaths.APPUSERCONTROLLER)]
[ApiController]
public class AppUserController : ControllerBase
{
    private readonly UserService _userService;

    public AppUserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("{email}")]
    public async Task<ActionResult> GetAppUser(string email)
    {
        var user = await _userService.GetAppUser(email);

        if (user is null) return NotFound();

        var returnUser = new CreatedAppUserReturn
        {
            Email = user.Email,
            FirstName = user.AppUser.FirstName,
            LastName = user.AppUser.LastName,
            Id = user.Id
        };

        return Ok(returnUser);
    }

    [HttpPost]
    public async Task<ActionResult> PostAppUser(CreateAppUserInput input)
    {
        var createdUser = await _userService.CreateAppUser(input);

        if (createdUser is null) return NotFound(); //TODO: Something else probably?

        //return Conflict(); /*TODO - Add try/catch*/

        var returnUser = new CreatedAppUserReturn
        {
            Email = createdUser.Email,
            FirstName = createdUser.AppUser.FirstName,
            LastName = createdUser.AppUser.LastName,
            Id = createdUser.Id
        };

        return CreatedAtAction(nameof(GetAppUser), new {email = returnUser.Email}, returnUser);
    }

    [HttpPut]
    [Authorize(Roles = StaticConfig.AppUserRole)]
    [Route("/favourites/{ticketId}")] //TODO: Byt när vi publishar igen. "favourites/{ticketId}" såhär.!
    public async Task<ActionResult<bool>> ToggleFavouriteTicket(Guid ticketId)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        await _userService.ToggleFavouriteTicket(ticketId, userId);

        return Ok(true);
    }
}