using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slipp.Services.Constants;
using SlippAPI.Services;

namespace SlippAPI.Controllers;

[Route(ApiPaths.SALECONTROLLER)]
[ApiController]
public class SaleController : ControllerBase
{
    private readonly SaleService _saleService;

    public SaleController(SaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateSale(SaleInput input)
    {
        var sale = await _saleService.CreateSale(input);

        var clubUrl = Url.Action("Get", "Club", new {id = input.ClubId}, "https");

        var saleOutput = SaleOutput.Create(clubUrl, sale);


        return CreatedAtAction("GetSale", new {id = saleOutput.Id}, saleOutput);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetSale(Guid id)
    {
        var sale = await _saleService.GetSale(id);

        if (sale == null) return NotFound();

        var clubUrl = Url.Action("Get", "Club", new {id = sale.Order.Tickets[0].Club.Id}, "https");

        var saleOutput = SaleOutput.Create(clubUrl, sale);

        return Ok(saleOutput);
    }
}