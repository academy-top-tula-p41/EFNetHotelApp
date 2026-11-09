using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public bool Daily { get; set; } = false;

        public List<Checkin>? Checkins { get; set; } = new();
        public List<CheckinService> CheckinsServices { get; set; } = new();
    }
}
