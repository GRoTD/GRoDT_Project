using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slipp.Services.Constants;
using SlippAPI.Services;

namespace SlippAPI.Controllers;

[Route(ApiPaths.TICKETCONTROLLER)]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly TicketService _ticketService;

    public TicketController(TicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost]
    [Authorize(Roles = $"{StaticConfig.ClubRole}")]
    public async Task<ActionResult<List<CreateTicketOutput>>> CreateTickets(Guid clubId, int amount,
        CreateTicketInput ticketInput)
    {
        var userClubId = User.Claims
            .FirstOrDefault(c => c.Type == SlippClaimTypes.CLUBID
                                 && c.Value == clubId.ToString());
        if (userClubId == null) return Unauthorized();

        var tickets = await _ticketService.CreateTickets(clubId, amount, ticketInput);

        var createdTickets =
            tickets.Select(ticket =>
                    CreateTicketOutput.Create(Url.Action("Get", "Club", new {id = ticket.Club.Id}, "https"), ticket))
                .ToList();

        return Ok(createdTickets);
    }

    [HttpGet]
    public async Task<ActionResult<List<CreateTicketOutput>>> GetTickets([FromQuery] Guid? hotelId)
    {
        //TODO: Catch errors

        var tickets = new List<Ticket>();
        if (hotelId == null) tickets = await _ticketService.GetUnsoldTicketsWithoutAuctions();
        else tickets = await _ticketService.GetUnsoldTicketsWithoutAuctions(hotelId);

        var returnTickets =
            tickets.Select(ticket =>
                    CreateTicketOutput.Create(Url.Action("Get", "Club", new {id = ticket.Club.Id}, "https"), ticket))
                .ToList();

        return Ok(returnTickets);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<List<CreateTicketOutput>>> GetTicket(Guid id)
    {
        //TODO: Catch errors
        var ticket = await _ticketService.GetTicket(id);

        var returnTicket =
            CreateTicketOutput.Create(Url.Action("Get", "Club", new {id = ticket.Club.Id}), ticket);

        return Ok(returnTicket);
    }
}