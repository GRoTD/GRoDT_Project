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
        [JsonPropertyName("tickets")] public List<TicketOutput> Tickets { get; set; }

        public static OrderOutput Create()
        {
            var outputOrder = new OrderOutput { 
                IssuedDateTime = DateTime.Now,
                Tickets    
            }
        }



        public static TicketOutput Create(string clubUrl, Ticket ticket)
        {
            var outputTicket = new TicketOutput
            {
                EndValidTime = ticket.EndValidTime,
                EventDescription = ticket.EventDescription,
                Price = ticket.Price,
                Id = ticket.Id,
                StartValidTime = ticket.StartValidTime,
                ClubName = ticket.Club.Name,
                ClubUrl = clubUrl, //TODO: Fix to use IUrlHelper.Action()
                Images = ticket.Images
            };

            return outputTicket;
        }
    }
}
