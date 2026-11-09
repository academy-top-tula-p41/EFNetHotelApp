using EFNetHotelApp;
using EFNetHotelApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.Marshalling;

using (HotelDbContext context = new())
{
    var clients = context.Clients
                         .Include(c => c.Document)
                         .Include(c => c.Checkins)
                            .ThenInclude(ch => ch.Room)
                         .ToList();

    foreach(var c in clients)
    {
        Console.WriteLine($"Name: {c.Name}, Phone: {c.Phone}, Documnet: {c.Document.Series}-{c.Document.Number}");
        Console.WriteLine("Checkins: ");
        //if(c.Checkins.Count > 0 )
        foreach(var ch in c.Checkins)
            Console.WriteLine($"Date: {ch.DateCheckin.ToShortDateString()}-{ch.DateCheckout?.ToShortDateString()}, Room: {ch.Room.Number}");
    }
        
}
