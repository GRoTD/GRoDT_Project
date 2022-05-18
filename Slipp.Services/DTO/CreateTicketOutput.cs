using Slipp.Services.Models;

namespace Slipp.Services.DTO;

public class CreateTicketOutput
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public DateTime StartValidTime { get; set; }
    public DateTime EndValidTime { get; set; }
    public string ClubUrl { get; set; }

    public static CreateTicketOutput Create(string clubUrl, Ticket ticket)
    {
        var outputTicket = new CreateTicketOutput
        {
            EndValidTime = ticket.EndValidTime,
            Title = ticket.Title,
            Price = ticket.Price,
            Id = ticket.Id,
            StartValidTime = ticket.StartValidTime,
            ClubUrl = clubUrl //TODO: Fix to use IUrlHelper.Action()
        };

        return outputTicket;
    }
}