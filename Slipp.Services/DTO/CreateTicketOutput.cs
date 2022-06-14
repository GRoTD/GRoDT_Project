using Slipp.Services.Models;
using System.Text.Json.Serialization;

namespace Slipp.Services.DTO;

public class CreateTicketOutput
{
    [JsonPropertyName ("id")]public Guid Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("price")] public decimal Price { get; set; }
    [JsonPropertyName("startValidTime")] public DateTime StartValidTime { get; set; }
    [JsonPropertyName("endValidTime")] public DateTime EndValidTime { get; set; }
    [JsonPropertyName("clubUrl")] public string ClubUrl { get; set; }
    [JsonPropertyName("images")] public List<Image> Images { get; set; }

    public static CreateTicketOutput Create(string clubUrl, Ticket ticket)
    {
        var outputTicket = new CreateTicketOutput
        {
            EndValidTime = ticket.EndValidTime,
            Title = ticket.Title,
            Price = ticket.Price,
            Id = ticket.Id,
            StartValidTime = ticket.StartValidTime,
            ClubUrl = clubUrl, //TODO: Fix to use IUrlHelper.Action()
            Images = ticket.Images
        };

        return outputTicket;
    }
}