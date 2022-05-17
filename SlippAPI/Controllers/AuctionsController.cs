using Microsoft.AspNetCore.Mvc;
using SlippAPI.Services;

namespace SlippAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuctionsController : ControllerBase
{
    private readonly AuctionService _auctionService;

    public AuctionsController(AuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionOutputDTO>>> GetAuctions()
    {
        //TODO: Add filtering
        var auctions = _auctionService.GetAuctions(null);

        //TODO: Make auctions into a List of Outputs
        return Ok(auctions);
    }
}