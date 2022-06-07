using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class StudentRegistrationConfiguration : IEntityTypeConfiguration<StudentRegisteration>
    {
        public void Configure(EntityTypeBuilder<StudentRegisteration> entity)
        {
            entity.ToTable("StudentRegistration", "prf");

            entity.HasIndex(e => e.ClassManagementId)
                .HasName("fki_FK_ClassmanagementID");

            entity.HasIndex(e => e.SchoolId)
                .HasName("fki_FK_SchoolId");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('prf.\"StudentRegistration_ID\"'::regclass)");

            entity.Property(e => e.ClassManagementId).HasColumnName("ClassManagementID");

            entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

            entity.Property(e => e.ModifiedBy).HasMaxLength(100);

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.ReferenceNo).HasMaxLength(10);

            entity.Property(e => e.SchoolCategoryId).HasColumnName("SchoolCategoryID");

            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.Property(e => e.SchoolTypeId).HasColumnName("SchoolTypeID");

            entity.HasOne(d => d.ClassManagement)
                .WithMany(p => p.StudentRegistration)
                .HasForeignKey(d => d.ClassManagementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassmanagementID");

            entity.HasOne(d => d.ClassType)
                .WithMany(p => p.StudentRegistration)
                .HasForeignKey(d => d.ClassTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassType");

            entity.HasOne(d => d.Pdistricts)
                .WithMany(p => p.StudentRegistration)
                .HasForeignKey(d => d.PdistrictsId)
                .HasConstraintName("FK_Pdistricts");

            entity.HasOne(d => d.Profile)
                .WithMany(p => p.StudentRegistration)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfileID");

            entity.HasOne(d => d.Province)
                .WithMany()
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_ProvinceID");

            entity.HasOne(d => d.SchoolCategory)
                .WithMany(p => p.StudentRegistration)
                .HasForeignKey(d => d.SchoolCategoryId)
                .HasConstraintName("FK_SchoolCategoryId");

            entity.HasOne(d => d.School)
                .WithMany(p => p.StudentRegistration)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SchoolId");

            entity.HasOne(d => d.SchoolType)
                .WithMany(p => p.StudentRegistration)
                .HasForeignKey(d => d.SchoolTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SchoolTypeID");

        }

    }
}
