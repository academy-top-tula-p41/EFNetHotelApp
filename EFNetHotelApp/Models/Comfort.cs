using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models
{
    public class Comfort
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public List<Room> Rooms { get; set; } = new();
    }
}
