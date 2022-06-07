using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class ServiceConfiguration : IEntityTypeConfiguration<App.Domain.Entity.look.Service>
    {
        public void Configure(EntityTypeBuilder<Domain.Entity.look.Service> builder)
        {
            builder.ToTable("Service", "look");

            builder.Property(e => e.ID)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("character varying");

            builder.Property(e => e.NameDari)
                .HasColumnName("NameDari")
                .HasColumnType("character varying");

            builder.Property(e => e.NamePashto)
                 .HasColumnName("NamePashto")
                 .HasColumnType("character varying");
        }
    }
}
