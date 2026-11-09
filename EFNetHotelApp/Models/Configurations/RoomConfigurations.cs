using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models.Configurations
{
    public class RoomConfigurations : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasAlternateKey(r => r.Number);

            builder.HasOne(r => r.Comfort)
                   .WithMany(c => c.Rooms)
                   .HasForeignKey(r => r.ComfortId);
        }
    }
}
