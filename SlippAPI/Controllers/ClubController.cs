using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slipp.Services.Constants;

namespace SlippAPI.Controllers;

[Route(ApiPaths.CLUBCONTROLLER)]
[ApiController]
[Authorize]
public class ClubController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    #region Auctions

    [HttpGet]
    [Route("{clubId}/auctions")]
    [AllowAnonymous]
    public async Task<ActionResult> GetAuctions(Guid clubId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("auctions/{auctionId:guid}")]
    public async Task<ActionResult> GetAuction(Guid auctionId)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("auctions")]
    public async Task<ActionResult> PostAuction() //TODO: Create a DTO for posting Auction eg. AuctionInputDTO
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Route("auctions/{auctionId:guid}")]
    public async Task<ActionResult> UpdateAuction(Guid auctionId)
    {
        throw new NotImplementedException();
    }


    [HttpDelete]
    [Route("auctions/{auctionId:guid}")]
    public async Task<ActionResult> DeletAuction(Guid auctionId)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Employees

    [HttpGet]
    [Route("employees/{employeeId}")]
    public async Task<ActionResult> GetEmployee(string employeeId)
    {
        throw new NotImplementedException();
    }

    #endregion


    #region Tickets

    [HttpGet]
    [Route("tickets")]
    public async Task<ActionResult> GetTickets()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("tickets/{ticketId:guid}")]
    public async Task<ActionResult> GetTicket(Guid ticketId)
    {
        throw new NotImplementedException();
    }

    #endregion
}