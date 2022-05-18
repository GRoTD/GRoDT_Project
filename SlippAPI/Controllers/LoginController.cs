using Microsoft.AspNetCore.Mvc;

namespace SlippAPI.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> LoginUser()
    {
        //TODO: return a JWT with a loginManager service.
        throw new NotImplementedException();
    }
}