using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class ExamTypeConfiguration : IEntityTypeConfiguration<ExamType>
    {
        public void Configure(EntityTypeBuilder<ExamType> entity)
        {
            entity.ToTable("ExamType", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();
            entity.Property(e => e.Dari).HasMaxLength(25);

            entity.Property(e => e.Name).HasMaxLength(25);

            entity.Property(e => e.Pashto).HasMaxLength(25);
        }

    }
}