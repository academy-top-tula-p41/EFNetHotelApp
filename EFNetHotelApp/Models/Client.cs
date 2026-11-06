using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public Document Document { get; set; } = null!;

        public List<Checkin>? Checkins { get; set; }
    }
}
