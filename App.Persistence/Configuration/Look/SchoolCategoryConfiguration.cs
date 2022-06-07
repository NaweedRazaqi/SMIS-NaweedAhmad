using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{



    public class SchoolCategoryConfiguration : IEntityTypeConfiguration<SchoolCategory>
    {
        public void Configure(EntityTypeBuilder<SchoolCategory> entity)
        {

            entity.ToTable("SchoolCategory", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.Name).HasMaxLength(250);

            entity.Property(e => e.NameEng).HasMaxLength(250);
        }
    }
}