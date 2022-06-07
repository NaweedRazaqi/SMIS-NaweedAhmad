using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class SubjectAssignmentConfiguration : IEntityTypeConfiguration<SubjectAssignment>
    {
        public void Configure(EntityTypeBuilder<SubjectAssignment> entity)
        {
            entity.ToTable("SubjectAssignment", "adm");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.ModifiedBy).HasColumnType("character varying");

            entity.HasOne(d => d.ClassManagement)
                .WithMany(p => p.SubjectAssignments)
                .HasForeignKey(d => d.ClassManagementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subjectassignment_classmanagement_fk");

            entity.HasOne(d => d.ClassType)
                .WithMany(p => p.SubjectAssignments)
                .HasForeignKey(d => d.ClassTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subjectassignment_classtype_fk");

            entity.HasOne(d => d.SubjectManagement)
                .WithMany(p => p.SubjectAssignments)
                .HasForeignKey(d => d.SubjectManagementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subjectassignment_subjectmanagement_fk");

            entity.HasOne(d => d.Teacher)
                .WithMany(p => p.SubjectAssignments)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subjectassignment_fk");

            entity.HasOne(d => d.Year)
                .WithMany(p => p.SubjectAssignments)
                .HasForeignKey(d => d.YearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subjectassignment_year_fk");
            entity.HasOne(d => d.SchoolType)
                   .WithMany(p => p.SubjectAssignments)
                   .HasForeignKey(d => d.SchoolTypeId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_SchoolType");
        }
    }
}
