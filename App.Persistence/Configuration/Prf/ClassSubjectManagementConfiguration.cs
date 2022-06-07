using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class ClassSubjectManagementConfiguration : IEntityTypeConfiguration<ClassSubjectManagement>
    {

        public void Configure(EntityTypeBuilder<ClassSubjectManagement> entity)
        {
            entity.ToTable("ClassSubjectManagement", "adm");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .HasIdentityOptions(null, null, null, 9223372036854L, null, null)
                .UseIdentityAlwaysColumn();


            entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

            entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

            entity.Property(e => e.ReferenceNo).HasMaxLength(10);

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.SchoolTypeId).HasColumnName("SchoolTypeID");
            

            entity.HasOne(d => d.ClassType)
                .WithMany(p => p.ClassSubjectManagements)
                .HasForeignKey(d => d.ClassTypeId)
                .HasConstraintName("FK_ClassSubjectManagement_ClassTypeID");

            entity.HasOne(d => d.Subject)
                .WithMany(p => p.ClassSubjectManagement)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_SubjectID");

            entity.HasOne(d => d.SchoolType)
                   .WithMany(p => p.ClassSubjectManagements)
                   .HasForeignKey(d => d.SchoolTypeId)
                   .HasConstraintName("FK_SchoolTypeID");
        }
    }
}
