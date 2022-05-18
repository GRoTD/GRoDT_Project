using System.Text.Json.Serialization;
using Slipp.Services.Models;

namespace Slipp.Services.DTO;

//TODO: Rename to better name. TicketOutput?
public class CreateTicketOutput
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("price")] public decimal Price { get; set; }
    [JsonPropertyName("startValidTime")] public DateTime StartValidTime { get; set; }
    [JsonPropertyName("endValidTime")] public DateTime EndValidTime { get; set; }
    [JsonPropertyName("clubName")] public string ClubName { get; set; }
    [JsonPropertyName("clubUrl")] public string ClubUrl { get; set; }

    public static CreateTicketOutput Create(string clubUrl, Ticket ticket)
    {
        var outputTicket = new CreateTicketOutput
        {
            EndValidTime = ticket.EndValidTime,
            Title = ticket.Title,
            Price = ticket.Price,
            Id = ticket.Id,
            StartValidTime = ticket.StartValidTime,
            ClubName = ticket.Club.Name,
            ClubUrl = clubUrl //TODO: Fix to use IUrlHelper.Action()
        };

        return outputTicket;
    }
}