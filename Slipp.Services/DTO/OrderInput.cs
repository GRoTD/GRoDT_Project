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
        public List<TicketOutput> Tickets { get; set; }

    }
}
