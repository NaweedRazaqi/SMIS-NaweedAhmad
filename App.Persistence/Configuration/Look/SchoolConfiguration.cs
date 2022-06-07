using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class SchoolConfiguration:IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> entity)
        {


            entity.ToTable("School", "look");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Code)
                        .IsRequired()
                        .HasMaxLength(10);
            entity.HasIndex(e => e.SchoolCategoryId)
                  .HasName("fki_FK_CategoryId");

            entity.Property(e => e.Dari)
                        .IsRequired()
                        .HasMaxLength(100);

            entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(100);

            entity.Property(e => e.Pashto)
                        .IsRequired()
                        .HasMaxLength(100);

            entity.HasOne(d => d.SchoolType)
                        .WithMany(p => p.School)
                        .HasForeignKey(d => d.SchoolTypeId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_School_SchoolType_SchoolTypeID");

            entity.HasOne(d => d.SchoolCategory)
                .WithMany()
                .HasForeignKey(d => d.SchoolCategoryId)
                .HasConstraintName("FK_CategoryId");

        }
    }
}
