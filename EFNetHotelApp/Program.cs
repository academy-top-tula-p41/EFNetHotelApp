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
        case 4: RoomsCrud(); break;
        case 6: Selects(); break;
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
void ComfortsRead(bool isClear = true)
{
    if(isClear)
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


void RoomsCrud()
{
    while (true)
    {
        var choiseCrud = MenuShow(4);
        if (choiseCrud == 0) break;

        switch (choiseCrud)
        {
            case 40: break;
            case 41:
                RoomsCreate();
                break;
            case 42:
                RoomsUpdate();
                break;
            case 43:
                ComfortsDelete();
                break;
        }
        RoomsRead();

        Console.WriteLine("For continue press any key...");
        Console.ReadKey();
    }
}
void RoomsRead(bool isPaging = true, bool isClear = true)
{
    if(isClear)
        Console.Clear();

    using (HotelDbContext context = new())
    {
        var rooms = context.Rooms
                              .Join(context.Comforts,
                              r => r.ComfortId,
                              c => c.Id,
                              (r, c) => new
                              {
                                  r.Id,
                                  r.Number,
                                  r.Capacity,
                                  r.Price,
                                  Comfort = c.Title
                              });

        //var rooms = context.Rooms
        //                       .Include(r => r.Comfort);

        int pageSize = 10;
        int pageCurrent = 0;
        int pagesCount = (int)Math.Ceiling(rooms.Count() / (double)pageSize);

        
        int maxComfortLength = context.Comforts.Max(c => c.Title.Length);

        bool isExit = false;

        while(!isExit)
        {
            Console.Clear();

            Console.WriteLine("Rooms:");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("< - To Left, > - To Right, Esc - Exit");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"Page {pageCurrent + 1} | Pages: {pagesCount}");
            Console.WriteLine("-------------------------------------");
            foreach (var r in rooms.Skip(pageCurrent * pageSize)
                                   .Take(pageSize))
                Console.WriteLine($"{r.Id}\t{r.Number}\t{r.Comfort}{new String(' ', maxComfortLength - r.Comfort.Length)}\t{r.Capacity}\t{r.Price}");

            var key = Console.ReadKey();
            
            switch(key.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (pageCurrent > 0) pageCurrent--;
                    break;
                case ConsoleKey.RightArrow:
                    if(pageCurrent < pagesCount - 1) pageCurrent++;
                    break;
                case ConsoleKey.Escape:
                    isExit = true;
                    break;
            }
        }
        
    }
}
void RoomsCreate()
{
    Console.Clear();

    Console.WriteLine("Create New Room");
    Console.WriteLine("------------------");

    Console.Write("Input Number: ");
    string? number = Console.ReadLine();
    if (number is null) return;

    Console.Write("Input Capacity: ");
    if (!Int32.TryParse(Console.ReadLine(), out int capacity)) return;

    ComfortsRead(false);
    Console.Write("Input Comfort: ");
    if (!Int32.TryParse(Console.ReadLine(), out int comfort)) return;

    Console.Write("Input Price: ");
    if (!Decimal.TryParse(Console.ReadLine(), out decimal price)) return;

    using (HotelDbContext context = new())
    {
        Room room = new()
        {
            Number = number,
            Capacity = capacity,
            Comfort = context.Comforts.Find(comfort),
            Price = price
        };

        context.Add(room);
        context.SaveChanges();
    }
}
void RoomsUpdate()
{
    RoomsRead();

    Console.Write("Input Id: ");
    if (!Int32.TryParse(Console.ReadLine(), out int id)) return;

    using(HotelDbContext context = new())
    {
        var room = context.Rooms.FirstOrDefault(s => s.Id == id);
        if (room is null) return;

        Console.Write($"Input new Number<{room.Number}>: ");
        string? number = Console.ReadLine();
        if (String.IsNullOrEmpty(number))
            number = room.Number;

        Console.Write($"Input Capacity<{room.Capacity}>: ");
        if (!Int32.TryParse(Console.ReadLine(), out int capacity))
            capacity = room.Capacity;

        Console.Write($"Input Price<{room.Price}>: ");
        if (!Decimal.TryParse(Console.ReadLine(), out decimal price))
            price = room.Price;

        //ComfortsRead(false);
        //Console.Write($"Input Comfort<{context.Comforts.FirstOrDefault(c => c.Id == room.ComfortId)?.Title}>: ");
        //if (!Int32.TryParse(Console.ReadLine(), out int comfort))
        //    comfort = (int)room.ComfortId;

        var comforts = context.Comforts.ToList();
        var comfortId = room.ComfortId;
        var position = Console.GetCursorPosition();

        bool isSelect = false;
        while(!isSelect)
        {
            Console.SetCursorPosition(position.Left, position.Top);
            Console.Write(new String(' ', 50));
            Console.SetCursorPosition(position.Left, position.Top);
            Console.Write($"Select Comfort<{comforts.Find(c => c.Id == room.ComfortId)?.Title}>: {comforts.Find(c => c.Id == comfortId)?.Title}");
            var key = Console.ReadKey();

            switch(key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (comfortId > 0) comfortId--;
                    break;
                case ConsoleKey.DownArrow:
                    if (comfortId < comforts.Count - 1) comfortId++;
                    break;
                case ConsoleKey.Enter:
                    isSelect = true;
                    break;

            }
        }

        room.Number = number;
        room.Capacity = capacity;
        room.Price = price;
        room.Comfort = context.Comforts.Find(comfortId);

        context.Rooms.Update(room);
        context.SaveChanges();
    }
}


void Selects()
{
    while (true)
    {
        var choiseSelect = MenuShow(6);
        if (choiseSelect == 0) break;

        switch (choiseSelect)
        {
            case 60: SelectClientsDocumenmts(); break;
            case 61: SelectRoomsFilterComfort(); break;

        }

        Console.WriteLine("For continue press any key...");
        Console.ReadKey();
    }
}

void SelectClientsDocumenmts()
{
    using(HotelDbContext context = new())
    {
        //var clients = context.Clients
        //                     .Include(c => c.Document)
        //                     .Select(c => new
        //                     {
        //                         ClientName = c.Name,
        //                         ClientDocument = c.Document,
        //                     })
        //                     .Distinct();

        var clients = from client in context.Clients
                                            .Include(c => c.Document)
                      select new
                      {
                          ClientName = client.Name,
                          ClientDocument = client.Document,
                      };


        Console.WriteLine("Clients Names & Documents:");
        Console.WriteLine("--------------------------");

        foreach (var client in clients)
            Console.WriteLine($"{client.ClientName} [{client.ClientDocument.Series} {client.ClientDocument.Number}]");
    }
    
}
void SelectRoomsFilterComfort()
{
    using(HotelDbContext context = new())
    {
        var comforts = context.Comforts.ToList();
        comforts.Insert(0, new() { Id = 0, Title = "Все типы" });

        bool isExit = false;
        
        int pageSize = 10;
        int pageCurrent = 0;
        int pagesCount;

        int comfortIndex = 0;

        while(!isExit)
        {
            Console.Clear();
            var position = Console.GetCursorPosition();
            bool isSelectFilter = false;
            while(!isSelectFilter)
            {
                Console.SetCursorPosition(position.Left, position.Top);
                Console.Write(new String(' ', 50));
                Console.SetCursorPosition(position.Left + 1, position.Top);
                Console.Write($"Select: {comforts[comfortIndex].Title}");

                var key = Console.ReadKey();
                switch(key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (comfortIndex > 0) 
                            comfortIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (comfortIndex < comforts.Count - 1) 
                            comfortIndex++;
                        break;
                    case ConsoleKey.Enter:
                        isSelectFilter = true;
                        pageCurrent = 0;
                        break;
                    case ConsoleKey.Escape:
                        isSelectFilter = true;
                        isExit = true;
                        break;
                }
            }
            if (isExit) break;
            
            int? comfortId = comforts[comfortIndex].Id;

            var rooms = context.Rooms.ToList();
            string title = comforts[comfortIndex].Title;

            if(comfortId != 0)
                rooms = context.Rooms
                               .Where(r => r.ComfortId == comfortId)
                               .ToList();

            rooms = rooms.Skip(pageCurrent * pageSize)
                         .Take(pageSize)
                         .ToList();
            
            pagesCount = (int)Math.Ceiling((double)rooms.Count / pageSize);

            while(!isExit)
            {
                Console.Clear();
                Console.WriteLine($"Rooms: {title}");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("< - To Left, > - To Right, Esc - Exit");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine($"Page {pageCurrent + 1} | Pages: {pagesCount}");
                Console.WriteLine("-------------------------------------");

                foreach (var room in rooms)
                    Console.WriteLine($"{room.Number}\t{room.Price}");

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (pageCurrent > 0) pageCurrent--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (pageCurrent < pagesCount - 1) pageCurrent++;
                        break;
                    case ConsoleKey.Escape:
                        isExit = true;
                        break;
                }
            }
            isExit = false;
        }
    }
}