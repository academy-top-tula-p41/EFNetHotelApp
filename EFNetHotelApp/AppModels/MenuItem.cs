using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.AppModels
{
    public class MenuItem
    {
        public int Id { get; set; }
        public int ParendId { get; set; }
        public string Title { get; set; } = null!;
    }
}
