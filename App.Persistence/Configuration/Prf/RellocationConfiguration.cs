using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{

    class RellocationConfiguration : IEntityTypeConfiguration<Rellocation>
    {
        public void Configure(EntityTypeBuilder<Rellocation> entity)
        {
               entity.ToTable("Rellocation", "rel");
            entity.HasIndex(e => e.District)
                  .HasName("fki_FK_Disctrict_Location");
            entity.HasIndex(e => e.SchoolLocationId)
                    .HasName("fki_FK_LocationID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasIdentityOptions(null, null, null, 2147483647L, null, null)
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

                entity.Property(e => e.NewSchoolId).HasColumnName("NewSchoolID");

                entity.Property(e => e.OldSchoolId).HasColumnName("OldSchoolID");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.ReferenceNo).HasMaxLength(10);

                entity.Property(e => e.SchoolLocationId).HasColumnName("SchoolLocationID");
              entity.Property(e => e.SchoolTypeId).HasColumnName("SchoolTypeID");
            entity.HasOne(d => d.NewSchool)
                    .WithMany(p => p.RellocationNewSchool)
                    .HasForeignKey(d => d.NewSchoolId)
                    .HasConstraintName("FK_Rellocation_NewSchoolID");

                entity.HasOne(d => d.OldSchool)
                    .WithMany(p => p.RellocationOldSchool)
                    .HasForeignKey(d => d.OldSchoolId)
                    .HasConstraintName("FK_Rellocation_OldSchoolID");
                entity.HasOne(d => d.DistrictNavigation)
                    .WithMany(p => p.RellocationDistrictNavigation)
                    .HasForeignKey(d => d.District)
                    .HasConstraintName("FK_Disctrict_Location");
            entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Rellocations)
                    .HasForeignKey(d => d.ProfileId)
                    .HasConstraintName("FK_Rellocation_ProfileID");

                entity.HasOne(d => d.SchoolLocation)
                    .WithMany(p => p.Rellocation)
                    .HasForeignKey(d => d.SchoolLocationId)
                    .HasConstraintName("FK_LocationID");
            entity.HasOne(d => d.SchoolType)
              .WithMany(p => p.Rellocations)
              .HasForeignKey(d => d.SchoolTypeId)
              .HasConstraintName("FK_SchoolTypeID");
        }
    }
}