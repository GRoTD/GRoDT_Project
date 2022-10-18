using Slipp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Slipp.Services.DTO
{
    public class OrderOutput
    {

        [JsonPropertyName("issuedDateTime")] public DateTime IssuedDateTime { get; set; }
        //Det borde nog vara public List<TicketOutput> här, men hur? 
        [JsonPropertyName("tickets")] public List<Ticket> Tickets { get; set; }

        public static OrderOutput CreateOrderOutput(Order order)
        {
            var outputOrder = new OrderOutput
            {
                IssuedDateTime = order.IssuedDateTime,
                Tickets = order.Tickets
            };
        
        return outputOrder;
    
        }
    }
}
