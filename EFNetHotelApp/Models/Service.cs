using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public List<Checkin>? Checkins { get; set; }
    }
}
