using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models
{
    public class Document
    {
        public Client Client { get; set; } = null!;
        public int ClientId { get; set; }

        public string? Series { get; set; }
        public string Number { get; set; } = null!;
    }
}
