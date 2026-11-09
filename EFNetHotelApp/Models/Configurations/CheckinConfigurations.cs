using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models.Configurations
{
    public class CheckinConfigurations : IEntityTypeConfiguration<Checkin>
    {
        public void Configure(EntityTypeBuilder<Checkin> builder)
        {
            builder.HasOne(ch => ch.Client)
                   .WithMany(c => c.Checkins)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ch => ch.Room)
                   .WithMany(r => r.Checkins)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.Services)
                   .WithMany(s => s.Checkins)
                   .UsingEntity<CheckinService>(
                    cs => cs.HasOne(cs => cs.Service)
                            .WithMany(s => s.CheckinsServices)
                            .HasForeignKey(cs => cs.ServiceId),
                    cs => cs.HasOne(cs => cs.Checkin)
                            .WithMany(c => c.CheckinsServices)
                            .HasForeignKey(cs => cs.CheckinId),
                    cs =>
                    {
                        //cs.HasKey(cs => new { cs.CheckinId, cs.ServiceId });
                        cs.ToTable("CheckinsServices");
                    });
        }
    }
}
