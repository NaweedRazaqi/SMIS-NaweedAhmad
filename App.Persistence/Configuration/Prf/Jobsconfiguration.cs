using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class Jobsconfiguration : IEntityTypeConfiguration<Jobs>
    {
        public void Configure(EntityTypeBuilder<Jobs> entity)
        {
            entity.ToTable("Jobs", "prf");

            entity.HasIndex(e => e.JobLocationId)
                .HasName("fki_FK_Location");

            entity.HasIndex(e => e.ProfessionTypeId)
                .HasName("fki_FK_ProfessionType");

            entity.HasIndex(e => e.ProfileId)
                .HasName("fki_FK_ProfileID");

            entity.HasIndex(e => e.RelativeTypeId)
                .HasName("fki_FK_Relation");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.GurrenterFatherName).HasMaxLength(100);

            entity.Property(e => e.GurrenterName).HasMaxLength(100);

            entity.Property(e => e.JobLocationId).HasColumnName("JobLocationID");

            entity.Property(e => e.ModifiedBy).HasColumnType("character varying");

            entity.Property(e => e.PositionName).HasMaxLength(100);

            entity.Property(e => e.ProfessionTypeId).HasColumnName("ProfessionTypeID");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.RelativeTypeId).HasColumnName("RelativeTypeID");

            entity.HasOne(d => d.JobLocation)
                .WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobLocationId)
                .HasConstraintName("FK_Location");

            entity.HasOne(d => d.ProfessionType)
                .WithMany(p => p.Jobs)
                .HasForeignKey(d => d.ProfessionTypeId)
                .HasConstraintName("FK_ProfessionType");

           

            entity.HasOne(d => d.RelativeType)
                .WithMany(p => p.Jobs)
                .HasForeignKey(d => d.RelativeTypeId)
                .HasConstraintName("FK_RelativeTypeID");

        }
    }
}
