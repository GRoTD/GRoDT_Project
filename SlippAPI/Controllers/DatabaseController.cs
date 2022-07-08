using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SlippAPI.Controllers;

[Route("api/database")]
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
    [Route("resetDb")]
    public async Task<ActionResult> ResetDb()
    {
        await _db.RecreateAndSeed();
        return Ok();
    }
}