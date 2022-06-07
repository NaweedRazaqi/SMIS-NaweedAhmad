using App.Domain.Entity.prf.view;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{

    public class vStudentSubjectMarksConfiguration : IEntityTypeConfiguration<StudentSubjectMarks>
    {
        public void Configure(EntityTypeBuilder<StudentSubjectMarks> entity)
        {
            entity.HasNoKey();

            entity.ToTable("vStudentSubjectMarks", "exm");

            entity.Property(e => e.FatherName).HasMaxLength(90);

            entity.Property(e => e.FirstName).HasMaxLength(90);

            entity.Property(e => e.MarksId).HasColumnName("MarksID");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.StudentClass).HasColumnType("character varying");

            entity.Property(e => e.StudentSchool).HasMaxLength(100);

            entity.Property(e => e.StudentSubjects).HasMaxLength(100);
        }
    }
}
