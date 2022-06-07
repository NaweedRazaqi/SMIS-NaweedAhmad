using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class RelativesTypeConfiguration : IEntityTypeConfiguration<RelativesType>
    {
        public void Configure(EntityTypeBuilder<RelativesType> entity)
        {
            entity.ToTable("RelativesType", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(e => e.NameDari).HasMaxLength(50);

            entity.Property(e => e.NamePashto).HasMaxLength(50);
        }
    }
}
