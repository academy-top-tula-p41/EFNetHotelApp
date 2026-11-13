using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.AppModels
{
    public static class AppDataProvider
    {
        static List<MenuItem> menu = new();

        static AppDataProvider()
        {
            MenuItem serviceMenuItem = new()
            {
                Id = 1,
                ParendId = 0,
                Title = "Services"
            };

            MenuItem comfortMenuItem = new()
            {
                Id = 2,
                ParendId = 0,
                Title = "Comfort"
            };

            MenuItem clientMenuItem = new()
            {
                Id = 3,
                ParendId = 0,
                Title = "Clients"
            };

            MenuItem roomMenuItem = new()
            {
                Id = 4,
                ParendId = 0,
                Title = "Rooms"
            };

            MenuItem checkinMenuItem = new()
            {
                Id = 5,
                ParendId = 0,
                Title = "Checkins"
            };

            MenuItem selectsMenuItem = new()
            {
                Id = 6,
                ParendId = 0,
                Title = "Selects"
            };

            menu.AddRange(serviceMenuItem, comfortMenuItem, clientMenuItem, roomMenuItem, checkinMenuItem);
            menu.Add(selectsMenuItem);

            MenuItem serviceRead = new()
            {
                Id = 10,
                ParendId = 1,
                Title = "Read All"
            };
            MenuItem serviceCreate = new()
            {
                Id = 11,
                ParendId = 1,
                Title = "Create"
            };
            MenuItem serviceUpdate = new()
            {
                Id = 12,
                ParendId = 1,
                Title = "Edit"
            };
            MenuItem serviceDelete = new()
            {
                Id = 13,
                ParendId = 1,
                Title = "Delete"
            };
            menu.AddRange(serviceRead, serviceCreate, serviceUpdate, serviceDelete);

            MenuItem comfortRead = new()
            {
                Id = 20,
                ParendId = 2,
                Title = "Read All"
            };
            MenuItem comfortCreate = new()
            {
                Id = 21,
                ParendId = 2,
                Title = "Create"
            };
            MenuItem comfortUpdate = new()
            {
                Id = 22,
                ParendId = 2,
                Title = "Edit"
            };
            MenuItem comfortDelete = new()
            {
                Id = 23,
                ParendId = 2,
                Title = "Delete"
            };
            menu.AddRange(comfortRead, comfortCreate, comfortUpdate, comfortDelete);



            MenuItem roomRead = new()
            {
                Id = 40,
                ParendId = 4,
                Title = "Read All"
            };
            MenuItem roomCreate = new()
            {
                Id = 41,
                ParendId = 4,
                Title = "Create"
            };
            MenuItem roomUpdate = new()
            {
                Id = 42,
                ParendId = 4,
                Title = "Edit"
            };
            MenuItem roomDelete = new()
            {
                Id = 43,
                ParendId = 4,
                Title = "Delete"
            };
            menu.AddRange(roomRead, roomCreate, roomUpdate, roomDelete);


            MenuItem clientsDocumentsMenuItem = new()
            {
                Id = 60,
                ParendId = 6,
                Title = "Select Clients with Documents"
            };
            menu.Add(clientsDocumentsMenuItem);

            MenuItem roomsComfortMenuItem = new()
            {
                Id = 61,
                ParendId = 6,
                Title = "Select Rooms Filter Comfort"
            };
            menu.Add(roomsComfortMenuItem);
        }

        public static List<MenuItem> GetMenu(int parentId = 0)
        {
            return menu.Where(m => m.ParendId == parentId).ToList();
        }
        
        public static MenuItem? GetItem(int id)
        {
            return menu.FirstOrDefault(i => i.Id == id);
        }
    }
}
