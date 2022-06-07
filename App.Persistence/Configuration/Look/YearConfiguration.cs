using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class YearConfiguration : IEntityTypeConfiguration<Year>
    {
        public void Configure(EntityTypeBuilder<Year> entity)
        {
            entity.ToTable("Year", "look");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Dari).HasColumnType("character varying");

            entity.Property(e => e.Name).HasColumnType("character varying");
        }
    }
}
