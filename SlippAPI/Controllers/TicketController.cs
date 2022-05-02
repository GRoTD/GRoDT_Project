using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using SlippAPI.DTOs;
using SlippAPI.Services;

namespace SlippAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
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
        }

        //TODO: Implement. Plan is to find something that belongs to the same auction or something like that.
        /*[HttpGet]
        public async Task<ActionResult<List<CreateTicketOutput>>> GetTicketsWithIdRange(int minId, int maxId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<List<CreateTicketOutput>>> GetTicketsFromClub(int clubId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<List<CreateTicketOutput>>> GetTicketsFromAuctionId(int auctionId)
        {
            throw new NotImplementedException();
        }*/
    }
}