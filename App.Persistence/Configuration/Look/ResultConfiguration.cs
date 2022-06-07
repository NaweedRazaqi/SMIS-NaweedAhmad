using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {

        public void Configure(EntityTypeBuilder<Result> entity)
        {
            entity.ToTable("Result", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.DariName).HasMaxLength(50);

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(e => e.PashtoName).HasMaxLength(50);
        }

    }
}
