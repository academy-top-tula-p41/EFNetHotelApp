using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models.Configurations
{
    public class ComfortConfigurations : IEntityTypeConfiguration<Comfort>
    {
        public void Configure(EntityTypeBuilder<Comfort> builder)
        {
            //builder.HasAlternateKey(c => c.Title);
        }
    }
}
