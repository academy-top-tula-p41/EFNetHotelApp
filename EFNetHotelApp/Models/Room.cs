using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public Comfort? Comfort { get; set; }
        public int? ComfortId { get; set; }

        public List<Checkin>? Checkins { get; set; } = new();
    }
}
