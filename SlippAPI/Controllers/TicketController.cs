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
    private readonly Database _db;

    public TicketController(TicketService ticketService, Database db)
    {
        _ticketService = ticketService;
        _db = db;
    }

    [HttpPost]
    [Authorize(Roles = $"{StaticConfig.ClubRole}")]
    public async Task<ActionResult<List<TicketOutput>>> CreateTickets(Guid clubId, int amount,
        TicketInput ticketInput)
    {
        var userClubId = User.Claims
            .FirstOrDefault(c => c.Type == SlippClaimTypes.CLUBID
                                 && c.Value == clubId.ToString());
        if (userClubId == null) return Unauthorized();

        var tickets = await _ticketService.CreateTickets(clubId, amount, ticketInput);

        var createdTickets =
            tickets.Select(ticket =>
                    TicketOutput.Create(Url.Action("Get", "Club", new {id = ticket.Club.Id}, "https"), ticket))
                .ToList();

        return Ok(createdTickets);
    }

    [HttpGet]
    public async Task<ActionResult<List<Ticket>>> GetTickets(DateTime? date, City? city)
    {
        var result = await _ticketService.GetAvailableTickets(date, city);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<IGrouping<DateTime, TicketOutput>>>> GetTickets1([FromQuery] Guid? clubId,
        [FromQuery] string? city)
    {
        //TODO: Catch errors

        var tickets = new List<Ticket>();

        //Should be easy to refactor to be better/easier to read.
        if (clubId == null && city == null) tickets = await _ticketService.GetUnsoldTicketsWithoutAuctions();
        else if (clubId != null && city == null) tickets = await _ticketService.GetUnsoldTicketsWithoutAuctions(clubId);
        else tickets = await _ticketService.GetUnsoldTicketsAtCity(city);

        var returnTickets =
            tickets.Select(ticket =>
                    TicketOutput.Create(Url.Action("Get", "Club", new {id = ticket.Club.Id}, "https"), ticket))
                .ToList();

        var listToReturn = new List<IGrouping<DateTime, TicketOutput>>();

        var ticketsGroupedByClub = returnTickets.GroupBy(t => t.ClubName).ToList();
        foreach (var list in ticketsGroupedByClub)
        {
            var ticketsGroupedByDate = list.GroupBy(t => t.StartValidTime).ToList();

            listToReturn.AddRange(ticketsGroupedByDate);
        }

        return Ok(listToReturn);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<List<TicketOutput>>> GetTicket(Guid id)
    {
        //TODO: Catch errors
        var ticket = await _ticketService.GetTicket(id);

        var returnTicket =
            TicketOutput.Create(Url.Action("Get", "Club", new {id = ticket.Club.Id}), ticket);

        return Ok(returnTicket);
    }

    [HttpGet]
    [Route("/user/{email}")]
    [Authorize(Roles = StaticConfig.AppUserRole)]
    public async Task<ActionResult> GetUserTickets(string email)
    {
        List<Ticket> userTickets = await _ticketService.GetUserTickets(email);

        var returnTickets = CreateTicketOutputList(userTickets);

        return Ok(returnTickets);
    }

    [HttpGet]
    [Route("{email}/favourites")]
    [Authorize(Roles = StaticConfig.AppUserRole)]
    public async Task<ActionResult> GetUserFavouriteTickets(string email)
    {
        List<Ticket> userTickets = await _ticketService.GetUserFavouriteTickets(email);

        var returnTickets = CreateTicketOutputList(userTickets);

        return Ok(returnTickets);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = $"{StaticConfig.ClubRole}, {StaticConfig.AdminRole}")]
    public async Task<ActionResult> DeleteTicket(Guid id)
    {
        //TODO: Check if club owns the ticket. 
        bool deleted = await _ticketService.DeleteTicket(id);

        if (deleted) return Ok();

        return BadRequest();
    }

    #region HelperMethods

    private List<TicketOutput> CreateTicketOutputList(List<Ticket> tickets)
    {
        return tickets.Select(ticket => CreateOneTicketOutput(ticket))
            .ToList();
    }

    private TicketOutput CreateOneTicketOutput(Ticket ticket)
    {
        return TicketOutput.Create(Url.Action("Get", "Club", new {id = ticket.Club.Id}, "https"), ticket);
    }

    #endregion
}