using EFNetHotelApp.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Comfort> Comforts { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Checkin> Checkins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hotel_Db;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DocumentConfigurations());
            modelBuilder.ApplyConfiguration(new ComfortConfigurations());
            modelBuilder.ApplyConfiguration(new ClientConfigurations());
            modelBuilder.ApplyConfiguration(new ServiceConfigurations());
            modelBuilder.ApplyConfiguration(new RoomConfigurations());
            modelBuilder.ApplyConfiguration(new CheckinConfigurations());

        }
    }
}
