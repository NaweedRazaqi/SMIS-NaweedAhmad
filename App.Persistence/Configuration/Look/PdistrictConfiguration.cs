using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{

    public class PdistrictConfiguration : IEntityTypeConfiguration<Pdistrict>
    {
        public void Configure(EntityTypeBuilder<Pdistrict> entity)
        {

            entity.ToTable("Pdistrict", "look");

            entity.HasIndex(e => e.ProvinceId)
                .HasName("fki_FK_Province");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.Name).HasColumnType("character varying");

            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

            entity.HasOne(d => d.Province)
                .WithMany()
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_Province");
        }
    }
}