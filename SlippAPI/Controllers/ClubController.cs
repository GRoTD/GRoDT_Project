using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SlippAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        #region Auctions

        [HttpGet]
        [Route("auctions")]
        public async Task<ActionResult> GetAuctions()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("auctions/{id}")]
        public async Task<ActionResult> GetAuction(string id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Employees

        [HttpGet]
        [Route("employees/{id}")]
        public async Task<ActionResult> GetEmployee(string id)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Tickets

        #endregion
    }
}