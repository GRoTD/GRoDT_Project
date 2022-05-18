﻿using Microsoft.AspNetCore.Mvc;
using Slipp.Services.DTO;

namespace SlippAPI.Controllers;

[Route("api/user")]
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

        var returnUser = new CreatedAppUserReturn
        {
            Email = createdUser.Email,
            FirstName = createdUser.AppUser.FirstName,
            LastName = createdUser.AppUser.LastName,
            Id = createdUser.Id
        };

        return CreatedAtAction(nameof(GetAppUser), new {email = returnUser.Email}, returnUser);
    }
}