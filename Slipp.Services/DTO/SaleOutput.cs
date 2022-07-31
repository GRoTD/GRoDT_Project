using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Slipp.Services.Models;

namespace Slipp.Services.DTO;

public class SaleOutput
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
    [JsonPropertyName("tickets")] public List<CreateTicketOutput> Tickets { get; set; }
    [JsonPropertyName("buyerId")] public string BuyerId { get; set; }
    [JsonPropertyName("isPayed")] public bool IsPayed { get; set; }
    [JsonPropertyName("payementDeadLine")] public DateTime PaymentDeadLine { get; set; }
    [JsonPropertyName("boughtDateTime")] public DateTime BoughtDateTime { get; set; }
    [JsonPropertyName("priceTotal")] public double PriceTotal { get; set; }

    public static SaleOutput Create(string clubUrl, Sale sale)
    {
        var saleOutput = new SaleOutput()
        {
            Id = sale.Id,
            BoughtDateTime = sale.BoughtDateTime,
            PaymentDeadLine = sale.PaymentDeadline,
            IsPayed = sale.IsPayed,
            Tickets = new List<CreateTicketOutput>(),
            BuyerId = sale.Buyer.Id
        };

        foreach (var ticket in sale.Tickets)
        {
            var ticketOutput = CreateTicketOutput.Create(clubUrl, ticket);
            saleOutput.Tickets.Add(ticketOutput);
        }

        return saleOutput;
    }
}