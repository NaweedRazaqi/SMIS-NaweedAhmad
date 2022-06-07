using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class IslamicEducationTypeConfiguration : IEntityTypeConfiguration<IslamicEducationType>
    {
        public void Configure(EntityTypeBuilder<IslamicEducationType> entity)
        {
            entity.ToTable("IslamicEducationType", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Dari).HasColumnType("character varying");

            entity.Property(e => e.Name).HasColumnType("character varying");
        }
    }
}
