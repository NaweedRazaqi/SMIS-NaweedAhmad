using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
 public   class StudentClassMarksConfiguration : IEntityTypeConfiguration<StudentClassMarks>
    {
        public void Configure(EntityTypeBuilder<StudentClassMarks> entity)
        {
            entity.ToTable("StudentClassMarks", "exm");

            entity.HasIndex(e => e.ExamTypeId)
                .HasName("fki_ExamTypeId");

            entity.HasIndex(e => e.StudentClassId)
                .HasName("fki_FK_ClassManagemen");

            entity.HasIndex(e => e.SubjectId)
                .HasName("fki_FK_Subject");

            entity.Property(e => e.Id)
                      .HasColumnName("ID")
                      .HasIdentityOptions(null, null, null, 9223372036854L, null, null)
                      .UseIdentityAlwaysColumn();

            entity.Property(e => e.Marks).HasDefaultValueSql("0");

            entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

            entity.Property(e => e.ReferenceNo).HasMaxLength(10);

            entity.Property(e => e.StudentClassId).HasColumnName("StudentClassID");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

            entity.HasOne(d => d.ExamType)
                .WithMany(p => p.StudentClassMarks)
                .HasForeignKey(d => d.ExamTypeId)
                .HasConstraintName("ExamTypeId");

            entity.HasOne(d => d.StudentClass)
                .WithMany(p => p.StudentClassMarks)
                .HasForeignKey(d => d.StudentClassId)
                .HasConstraintName("FK_ClassManagemen");

            entity.HasOne(d => d.Subject)
                .WithMany(p => p.StudentClassMarks)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_Subject");
        }

    }
}



