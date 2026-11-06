using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFNetHotelApp.Models.Configurations
{
    public class DocumentConfigurations : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasAlternateKey(d => new { d.Series, d.Number });

            builder.HasOne(d => d.Client)
                   .WithOne(c => c.Document)
                   .HasForeignKey<Document>(d => d.ClientId);
        }
    }
}
