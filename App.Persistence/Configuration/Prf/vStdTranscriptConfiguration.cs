using App.Domain.Entity.prf.view;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{



    public class vStdTranscriptConfiguration : IEntityTypeConfiguration<VStudentsTranscript>
    {
        public void Configure(EntityTypeBuilder<VStudentsTranscript> entity)
        {

            entity.HasNoKey();

            entity.ToTable("vStudentTranscript", "exm");

            entity.Property(e => e.FatherName).HasMaxLength(90);

            entity.Property(e => e.FirstName).HasMaxLength(90);

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.StudentSubjects).HasMaxLength(100);

            entity.Property(e => e._10classMark).HasColumnName("10ClassMark");

            entity.Property(e => e._10studentclasid).HasColumnName("10studentclasid");

            entity.Property(e => e._10thsubject).HasColumnName("10thsubject");

            entity.Property(e => e._11classMark).HasColumnName("11ClassMark");

            entity.Property(e => e._11studentclassid).HasColumnName("11studentclassid");

            entity.Property(e => e._11subjects).HasColumnName("11subjects");

            entity.Property(e => e._12classMark).HasColumnName("12ClassMark");

            entity.Property(e => e._12studetnclassid).HasColumnName("12studetnclassid");

            entity.Property(e => e._12subjects).HasColumnName("12subjects");
        }

    }
}