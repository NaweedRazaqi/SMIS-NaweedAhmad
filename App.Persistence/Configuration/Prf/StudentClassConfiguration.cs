using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class StudentClassConfiguration : IEntityTypeConfiguration<StudentClass>
    {
        public void Configure(EntityTypeBuilder<StudentClass> entity)
        {
            entity.ToTable("StudentClass", "adm");


            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .HasIdentityOptions(null, null, null, 9223372036854L, null, null)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ClassManagementId).HasColumnName("ClassManagementID");

            entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

            entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.ReferenceNo).HasMaxLength(10);

            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.HasOne(d => d.ClassManagement)
                .WithMany(p => p.StudentClass)
                .HasForeignKey(d => d.ClassManagementId)
                .HasConstraintName("FK_StudentClass_ClassManagementID");

            entity.HasOne(d => d.ClassType)
                .WithMany(p => p.StudentClass)
                .HasForeignKey(d => d.ClassTypeId)
                .HasConstraintName("FK_StudentClass_ClassTypeID");
            entity.HasOne(d => d.School)
                .WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("Fk_SchoolID");


        }

    }
}
