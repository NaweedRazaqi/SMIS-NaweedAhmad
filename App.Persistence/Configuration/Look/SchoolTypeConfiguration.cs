using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{

    public class SchoolTypeConfiguration : IEntityTypeConfiguration<SchoolType>
    {
        public void Configure(EntityTypeBuilder<SchoolType> entity)
        {
            entity.ToTable("SchoolType", "look");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.NameDari).HasMaxLength(100);
        }
    }
}
