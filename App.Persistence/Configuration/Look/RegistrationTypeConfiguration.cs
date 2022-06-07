using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    class RegistrationTypeConfiguration : IEntityTypeConfiguration<App.Domain.Entity.look.RegistrationType>
    {
        public void Configure(EntityTypeBuilder<RegistrationType> entity)
        {
            entity.ToTable("RegistrationType", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.education_seq'::regclass)");

            entity.Property(e => e.Name).HasMaxLength(200);

        }

    }
}
