using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models
{
    public class CheckinService
    {
        public int Id { get; set; }

        public Checkin Checkin { get; set; }
        public int CheckinId { get; set; }

        public Service Service { get; set; }
        public int ServiceId { get; set; }

        public int Quantity { get; set; }
    }
}
