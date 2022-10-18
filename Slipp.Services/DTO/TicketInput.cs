using Slipp.Services.Models;

namespace Slipp.Services.DTO;

public class TicketInput
{
    public string EventDescription { get; set; }
    public decimal Price { get; set; }
    public DateTime StartValidTime { get; set; }
    public DateTime EndValidTime { get; set; }
    public List<Image> Images { get; set; }
    public Ticket CreateTicket(Club club)
    {
        Ticket ticket = new Ticket
        {
            Club = club,
            EventDescription = EventDescription,
            StartValidTime = StartValidTime,
            EndValidTime = EndValidTime,
            Price = Price,
            Images = Images
        };
        return ticket;
    }
}