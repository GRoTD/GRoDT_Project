using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slipp.Services.Constants;

namespace SlippAPI.Controllers;

[Route(ApiPaths.DATABASECONTROLLER)]
[ApiController]
public class DatabaseController : ControllerBase
{
    private readonly Database _db;

    public DatabaseController(Database db)
    {
        _db = db;
    }

    [HttpGet]
    [Authorize]
    [Route("reset-db")]
    public async Task<ActionResult> ResetDb()
    {
        await _db.RecreateAndSeed();
        return Ok("Database reset!");
    }
}