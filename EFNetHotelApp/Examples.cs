using EFNetHotelApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp
{
    public static class Examples
    {
        public static void AddData(HotelDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            List<Comfort> comforts = new()
            {
                new(){ Title = "Сюит" },
                new(){ Title = "Люкс" },
                new(){ Title = "Апартаменты" },
                new(){ Title = "Студия" },
                new(){ Title = "Стандарт" },
                new(){ Title = "Эконом" },
            };

            context.Comforts.AddRange(comforts);

            List<Service> services = new()
            {
                new(){ Title = "Питание", Price = 1000, Daily = true },
                new(){ Title = "Трансфер", Price = 2500 },
                new(){ Title = "Спа", Price = 3000 },
                new(){ Title = "Бизнес-услуги", Price = 5000 },
                new(){ Title = "Экскурсии", Price = 1500 },
                new(){ Title = "Прокат спортинвенторя", Price = 1000 },
                new(){ Title = "Мини-бар", Price = 500, Daily = true },
            };

            context.Services.AddRange(services);

            List<Room> rooms = new();

            Random random = new Random();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < random.Next(5, 10); j++)
                {
                    string number = (i + 1).ToString()
                                    +
                                    ((j < 10) ? "0" : "")
                                    + j.ToString();
                    int capacity = random.Next(2, 5);
                    decimal price = random.Next(300, 1001) * 10;
                    Comfort comfort = comforts[random.Next(comforts.Count())];
                    rooms.Add(new()
                    {
                        Number = number,
                        Capacity = capacity,
                        Price = price,
                        Comfort = comfort
                    });
                }
            context.Rooms.AddRange(rooms);

            List<Client> clients = new();
            string[] names = { "Bobby", "Jimmy", "Willy", "Donny", "Sammy", "Tommy", "Genry" };
            for (int i = 0; i < 10; i++)
            {
                string name = names[random.Next(names.Length)];

                string phone = "+7";
                for (int j = 0; j < 10; j++)
                    phone += random.Next(10).ToString();

                Document document = new();
                for (int j = 0; j < 6; j++)
                    document.Series += random.Next(10).ToString();
                for (int j = 0; j < 8; j++)
                    document.Number += random.Next(10).ToString();

                Client client = new()
                {
                    Name = name,
                    Phone = phone,
                    Document = document,
                };
                document.Client = client;

                clients.Add(client);
            }
            context.Clients.AddRange(clients);



            List<Checkin> checkins = new();
            for (int i = 0; i < 50; i++)
            {
                Checkin checkin = new();
                checkin.Client = clients[random.Next(clients.Count())];
                checkin.Room = rooms[random.Next(rooms.Count())];

                int days = random.Next(10, 100);
                DateOnly dateCheckin = DateOnly.FromDateTime(DateTime.Now.AddDays(-days));
                DateOnly? dateCheckout = null;
                if (days > 12)
                    dateCheckout = dateCheckin.AddDays(random.Next(11, 31));

                checkin.DateCheckin = dateCheckin;
                checkin.DateCheckout = dateCheckout;

                for (int j = 0; j < random.Next(0, 5); j++)
                {
                    var service = services[random.Next(services.Count())];
                    
                    CheckinService serviceCheckin = new();
                    serviceCheckin.Service = service;
                    serviceCheckin.Checkin = checkin;
                    if(service.Daily)
                    {
                        if (dateCheckout is not null)
                            serviceCheckin.Quantity = ((DateOnly)dateCheckout).DayNumber - dateCheckin.DayNumber;
                        else
                            serviceCheckin.Quantity = DateOnly.FromDateTime(DateTime.Now).DayNumber - dateCheckin.DayNumber;
                    }
                    else
                    {
                        serviceCheckin.Quantity = random.Next(0, 4);
                    }
                    service.CheckinsServices.Add(serviceCheckin);
                    checkin.CheckinsServices.Add(serviceCheckin);
                }

                checkins.Add(checkin);
            }
            context.Checkins.AddRange(checkins);

            context.SaveChanges();
        }
    }
}
