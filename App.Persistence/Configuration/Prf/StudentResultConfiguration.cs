using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{

    public class StudentResultConfiguration : IEntityTypeConfiguration<StudentResult>
    {
        public void Configure(EntityTypeBuilder<StudentResult> entity)
        {
            entity.ToTable("StudentResult", "exm");

            entity.HasIndex(e => e.ClassManagementId)
                .HasName("fki_FK_ClassManagement");

            entity.HasIndex(e => e.ResultId)
                .HasName("fki_FK_ResultID");

            entity.Property(e => e.Id)
               .HasColumnName("ID")
               .UseIdentityAlwaysColumn();

            entity.Property(e => e.ClassManagementId).HasColumnName("ClassManagementID");

            entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

            entity.Property(e => e.ModifiedBy).HasColumnType("character varying");
            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");


            entity.Property(e => e.ResultId).HasColumnName("ResultID");

            entity.HasOne(d => d.ClassManagement)
                .WithMany(p => p.StudentResults)
                .HasForeignKey(d => d.ClassManagementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassManagement");

            entity.HasOne(d => d.ClassType)
                .WithMany(p => p.StudentResults)
                .HasForeignKey(d => d.ClassTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassTypeID");

            entity.HasOne(d => d.Result)
                .WithMany(p => p.StudentResults)
                .HasForeignKey(d => d.ResultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultID");
           
            entity.HasOne(d => d.Profile)
                  .WithMany(p => p.StudentClassResults)
                  .HasForeignKey(d => d.ProfileId)
                  .HasConstraintName("FK_ProfileID");

        }
    }
}

