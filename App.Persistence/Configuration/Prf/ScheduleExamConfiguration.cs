using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{

    public class ScheduleExamConfiguration : IEntityTypeConfiguration<ScheduleExam>
    {
        public void Configure(EntityTypeBuilder<ScheduleExam> entity)
        {
            entity.ToTable("ScheduleExam", "exm");

            entity.HasIndex(e => e.ClassManagementId)
                .HasName("fki_FK_ClassManagementID");

            entity.HasIndex(e => e.ClassTypeId)
                            .HasName("fki_FK_ClassTypeID");

            entity.HasIndex(e => e.SubjectId)
                            .HasName("fki_FK_SubjectId");

            entity.Property(e => e.Id)
                            .HasColumnName("ID")
                            .UseIdentityAlwaysColumn();

            entity.Property(e => e.ClassManagementId).HasColumnName("ClassManagementID");

            entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

            entity.Property(e => e.ExmName).HasMaxLength(500);
            entity.Property(e => e.ExamTimeEnd).HasColumnType("time without time zone");
            entity.Property(e => e.ExamTimeStart).HasColumnType("time without time zone");
            entity.Property(e => e.StudentClassId).HasColumnName("StudentClassID");
            entity.Property(e => e.SchoolTypeId).HasColumnName("SchoolTypeID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

            entity.HasOne(d => d.ClassManagement)
                            .WithMany(p => p.ScheduleExams)
                            .HasForeignKey(d => d.ClassManagementId)
                            .HasConstraintName("FK_ClassManagementID");

            entity.HasOne(d => d.ClassType)
                            .WithMany(p => p.ScheduleExams)
                            .HasForeignKey(d => d.ClassTypeId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_ClassTypeID");

            entity.HasOne(d => d.Subject)
                            .WithMany(p => p.ScheduleExams)
                            .HasForeignKey(d => d.SubjectId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_SubjectId");
           entity.HasOne(d => d.SchoolType)
                  .WithMany(p => p.ScheduleExams)
                  .HasForeignKey(d => d.SchoolTypeId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_SchoolTypeId");
        }
    }


  
}
