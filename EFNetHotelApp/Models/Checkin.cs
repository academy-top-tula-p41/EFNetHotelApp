using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models
{
    public class Checkin
    {
        public int Id { get; set; }
        public Client Client { get; set; } = new();
        public Room Room { get; set; } = new();
        public DateOnly DateCheckin { get; set; }
        public DateOnly? DateCheckout { get; set; }

        public List<Service>? Services { get; set; } = new();
        public List<CheckinService> CheckinsServices { get; set; } = new();
    }
}
