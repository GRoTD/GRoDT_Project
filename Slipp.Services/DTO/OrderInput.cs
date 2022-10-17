using Slipp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slipp.Services.DTO
{
    public class OrderInput
    {
        public DateTime IssuedDateTime { get; set; }

        //borde den här ta in TicketOutput istället?
        public List<Ticket> Tickets { get; set; }

        public Order CreateOrder(AppUser appUser)
        {
            Order order = new Order
            {
                AppUser = appUser,
                IssuedDateTime = IssuedDateTime,
                Tickets = Tickets
                
            };
            return order;
        }

    }
}
