using Microsoft.AspNetCore.Mvc;
using SlippAPI.Services;

namespace SlippAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly TicketService _ticketService;

    public TicketController(TicketService ticketService)
    {
        _ticketService = ticketService;
    }

    /* [HttpPost]
     public async Task<ActionResult<List<CreateTicketOutput>>> CreateTickets(int amount,
         CreateTicketInput ticketInput)
     {
         var tickets = await _ticketService.CreateTickets(amount, ticketInput);
 
         var createdTickets = new List<CreateTicketOutput>();
 
         foreach (var ticket in tickets)
         {
             var createdTicket = new CreateTicketOutput
             {
                 Id = ticket.Id,
                 Title = ticket.Title,
                 Price = ticket.Price,
                 StartValidTime = ticket.StartValidTime,
                 EndValidTime = ticket.EndValidTime
             };
 
             createdTickets.Add(createdTicket);
         }
 
         return Ok(createdTickets);
     }*/

    [HttpGet]
    public async Task<ActionResult<List<CreateTicketOutput>>> GetTickets()
    {
        //TODO: Catch errors
        var tickets = await _ticketService.GetTicketsWithoutAuctions();

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