using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slipp.Services.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public DateTime IssuedDateTime { get; set; }

        public bool IsSaved { get; set; }

        public AppUser AppUser { get; set; }

        public Sale? Sale { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}