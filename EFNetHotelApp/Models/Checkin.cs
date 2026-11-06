using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models
{
    public class Checkin
    {
        public int Id { get; set; }
        public Client Client { get; set; } = null!;
        public Room Room { get; set; } = null!;
        public DateOnly DateCheckin { get; set; }
        public DateOnly? DateCheckout { get; set; }

        public List<Service>? Services { get; set; }
    }
}
