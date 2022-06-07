using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    class StudentClassResultSheetConfiguration : IEntityTypeConfiguration<StudentClassResultSheet>
    {
        public void Configure(EntityTypeBuilder<StudentClassResultSheet> entity)
        {
            entity.ToTable("StudentClassResultSheet", "exm");

            entity.HasIndex(e => e.ClassManagementId)
                .HasName("fki_ClassManagementID");

            entity.HasIndex(e => e.ClassTypeId)
                .HasName("fki_ClassTypeID");

            entity.HasIndex(e => e.DocumentTypeId)
                .HasName("fki_DocumentTypeID");

            entity.HasIndex(e => e.StudentSchoolId)
                .HasName("fki_SchoolID");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.ClassManagementId).HasColumnName("ClassManagementID");

            entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            entity.Property(e => e.SchoolTypeId).HasColumnName("SchoolTypeID");

            entity.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasColumnType("character varying");
        
            entity.Property(e => e.StudentSchoolId).HasColumnName("StudentSchoolID");
            
            entity.HasOne(d => d.ClassManagement)
                   .WithMany()
                   .HasForeignKey(d => d.ClassManagementId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("ClassManagementID");

            entity.HasOne(d => d.ClassType)
                .WithMany()
                .HasForeignKey(d => d.ClassTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ClassTypeID");

            entity.HasOne(d => d.DocumentType)
                    .WithMany()
                    .HasForeignKey(d => d.DocumentTypeId)
                    .HasConstraintName("FK_DoucmentType");

            entity.HasOne(d => d.SchoolType)
                .WithMany(p => p.StudentClassResultSheets)
                .HasForeignKey(d => d.SchoolTypeId)
                .HasConstraintName("FK_SchoolTypeID");
            entity.HasOne(d => d.StudentSchool)
                .WithMany()
                .HasForeignKey(d => d.StudentSchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SchoolID");
        }
    }
}
