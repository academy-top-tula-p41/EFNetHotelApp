using EFNetHotelApp;
using EFNetHotelApp.AppModels;
using EFNetHotelApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.Marshalling;

//using (HotelDbContext context = new())
//{
//    var clients = context.Clients
//                         .Include(c => c.Document)
//                         .Include(c => c.Checkins)
//                            .ThenInclude(ch => ch.Room)
//                         .ToList();
//    foreach(var c in clients)
//    {
//        Console.WriteLine($"Name: {c.Name}, Phone: {c.Phone}, Documnet: {c.Document.Series}-{c.Document.Number}");
//        Console.WriteLine("Checkins: ");
//        //if(c.Checkins.Count > 0 )
//        foreach(var ch in c.Checkins)
//            Console.WriteLine($"Date: {ch.DateCheckin.ToShortDateString()}-{ch.DateCheckout?.ToShortDateString()}, Room: {ch.Room.Number}");
//    }
//}


int choise;

while(true)
{
    choise = MenuShow();
    if (choise == 0) break;


    switch(choise)
    {
        case 1: ServicesCrud(); break;
        case 2: ComfortsCrud(); break;
    }

    
}


int MenuShow(int parentId = 0)
{
    Console.Clear();

    var menu = AppDataProvider.GetMenu(parentId);
    
    var parent = AppDataProvider.GetItem(parentId);
    if(parent is not null)
    {
        Console.WriteLine($"{parent.Title}");
        Console.WriteLine(new String('-', parent.Title.Length));
    }
    foreach (var item in menu)
        Console.WriteLine($"{item.Id}. {item.Title}");
    Console.WriteLine($"0. Quit");

    Console.Write("Choise: ");

    if (!Int32.TryParse(Console.ReadLine(), out int choise))
        return 0;
    return choise;
}



void ServicesCrud()
{
    while (true)
    {
        var choiseCrud = MenuShow(1);
        if (choiseCrud == 0) break;

        switch (choiseCrud)
        {
            case 10: break;
            case 11:
                ServicesCreate();
                break;
            case 12:
                ServicesUpdate();
                break;
            case 13:
                ServicesDelete();
                break;
        }
        ServicesRead();

        Console.WriteLine("For continue press any key...");
        Console.ReadKey();
    }
}
void ServicesRead()
{
    Console.Clear();

    using(HotelDbContext context = new())
    {
        var services = context.Services.ToList();
        int maxTitleLength = context.Services.Max(s => s.Title.Length);

        Console.WriteLine("Services:\n-----------");
        foreach(var s in services)
            Console.WriteLine($"{s.Id}\t{s.Title}{new String(' ', maxTitleLength - s.Title.Length)}\t{s.Price}\t{(s.Daily ? "Daily" : "Simple")}");
        Console.WriteLine();
    }
}
void ServicesCreate()
{
    Console.Clear();

    Console.WriteLine("Create New Srevice");
    Console.WriteLine("------------------");

    Console.Write("Input Title: ");
    string? title = Console.ReadLine();
    if (title is null) return;

    Console.Write("Input Price: ");
    if (!Decimal.TryParse(Console.ReadLine(), out decimal price)) return;

    Console.Write("This Service id Daily?<y/n>: ");
    string? daily = Console.ReadLine();
    if (daily is null || !"ynYN".Contains(daily)) return;

    using(HotelDbContext context = new())
    {
        Service service = new()
        {
            Title = title,
            Price = price,
            Daily = "yY".Contains(daily)
        };

        context.Services.Add(service);
        context.SaveChanges();
    }
}
void ServicesUpdate()
{
    ServicesRead();
    Console.Write("Input Id: ");
    if (!Int32.TryParse(Console.ReadLine(), out int id)) return;

    using(HotelDbContext context = new())
    {
        var service = context.Services.FirstOrDefault(s => s.Id == id);
        if(service is null) return;

        Console.Write($"Input new Title<{service.Title}>: ");
        string? title = Console.ReadLine();
        if (String.IsNullOrEmpty(title)) 
            title = service.Title;

        Console.Write($"Input Price<{service.Price}>: ");
        if (!Decimal.TryParse(Console.ReadLine(), out decimal price))
            price = service.Price;

        Console.Write($"This Service id Daily?<{(service.Daily ? "y" : "n")}>: ");
        string? daily = Console.ReadLine();

        if (String.IsNullOrEmpty(daily))
            daily = service.Daily ? "y" : "n";

        if (!"ynYN".Contains(daily)) return;

        service.Title = title;
        service.Price = price;
        service.Daily = "yY".Contains(daily);

        context.Services.Update(service);
        context.SaveChanges();
    }
}
void ServicesDelete()
{
    ServicesRead();

    Console.Write("Input Id: ");
    if (!Int32.TryParse(Console.ReadLine(), out int id)) return;

    using(HotelDbContext context = new())
    {
        var service = context.Services.FirstOrDefault(s => s.Id ==id);
        if(service is null ) return;

        context.Services.Remove(service);
        context.SaveChanges();
    }
}

void ComfortsCrud()
{
    while (true)
    {
        var choiseCrud = MenuShow(2);
        if (choiseCrud == 0) break;

        switch (choiseCrud)
        {
            case 20: break;
            case 21:
                ComfortsCreate();
                break;
            case 22:
                ComfortsUpdate();
                break;
            case 23:
                ComfortsDelete();
                break;
        }
        ComfortsRead();

        Console.WriteLine("For continue press any key...");
        Console.ReadKey();
    }
}

void ComfortsRead()
{
    Console.Clear();

    using (HotelDbContext context = new())
    {
        var comforts = context.Comforts.ToList();
        int maxTitleLength = context.Comforts.Max(s => s.Title.Length);

        Console.WriteLine("Comforts:\n---------");
        foreach (var s in comforts)
            Console.WriteLine($"{s.Id}\t{s.Title}");
        Console.WriteLine();
    }
}

void ComfortsCreate()
{
    Console.Clear();

    Console.WriteLine("Create New Comfort");
    Console.WriteLine("------------------");

    Console.Write("Input Title: ");
    string? title = Console.ReadLine();
    if (title is null) return;


    using (HotelDbContext context = new())
    {
        Comfort comfort = new()
        {
            Title = title
        };

        context.Comforts.Add(comfort);
        context.SaveChanges();
    }
}

void ComfortsUpdate()
{
    ComfortsRead();
    Console.Write("Input Id: ");
    if (!Int32.TryParse(Console.ReadLine(), out int id)) return;

    using (HotelDbContext context = new())
    {
        var comfort = context.Comforts.FirstOrDefault(c => c.Id == id);
        if (comfort is null) return;

        Console.Write($"Input new Title<{comfort.Title}>: ");
        string? title = Console.ReadLine();
        if (String.IsNullOrEmpty(title))
            title = comfort.Title;

        comfort.Title = title;

        context.Comforts.Update(comfort);
        context.SaveChanges();
    }
}

void ComfortsDelete()
{
    ComfortsRead();

    Console.Write("Input Id: ");
    if (!Int32.TryParse(Console.ReadLine(), out int id)) return;

    using (HotelDbContext context = new())
    {
        var comfort = context.Comforts.FirstOrDefault(c => c.Id == id);
        if (comfort is null) return;

        context.Comforts.Remove(comfort);
        context.SaveChanges();
    }
}
