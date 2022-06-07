using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{


    public class ClassUpgradationConfiguration : IEntityTypeConfiguration<ClassUpgradation>
    {

        public void Configure(EntityTypeBuilder<ClassUpgradation> entity)
        {
            entity.ToTable("ClassUpgradation", "exm");

            entity.HasIndex(e => e.ClassTypeId)
                .HasName("fki_FK_ClassType");

            entity.HasIndex(e => e.ProfileId)
                .HasName("fki_FK_Profile");

            entity.Property(e => e.Id).HasColumnName("ID")
              .HasColumnName("ID")
              .HasDefaultValueSql("nextval('exm.\"ClassUpgradation_ID_seq\"'::regclass)");

            entity.Property(e => e.ClassManagementId).HasColumnName("ClassManagementID");

            entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

            entity.Property(e => e.ModifiedBy).HasColumnType("character varying");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID`");

            entity.HasOne(d => d.ClassManagement)
                .WithMany(p => p.ClassUpgradations)
                .HasForeignKey(d => d.ClassManagementId)
                .HasConstraintName("FK_ClassManagement");

            entity.HasOne(d => d.ClassType)
                .WithMany(p => p.ClassUpgradations)
                .HasForeignKey(d => d.ClassTypeId)
                .HasConstraintName("FK_ClassType");

            
        }

    }
}
