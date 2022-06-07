using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    class ClassManagementConfiguration : IEntityTypeConfiguration<ClassManagement>
    {

        public void Configure(EntityTypeBuilder<ClassManagement> entity)
        {

            entity.ToTable("ClassManagement", "adm");

            entity.Property(e => e.Id)
                  .HasColumnName("ID")
                  .HasIdentityOptions(null, null, null, 9223372036854L, null, null)
                  .UseIdentityAlwaysColumn();

            entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

            entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

            entity.Property(e => e.Name).HasMaxLength(500);

            entity.Property(e => e.NameEng).HasMaxLength(500);

            entity.Property(e => e.ReferenceNo).HasMaxLength(10);

            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.HasOne(d => d.ClassType)
                .WithMany(p => p.ClassManagement)
                .HasForeignKey(d => d.ClassTypeId)
                .HasConstraintName("FK_ClassManagement_ClassTypeID");

            entity.HasOne(d => d.School)
                .WithMany(p => p.ClassManagement)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK_ClassManagement_SchoolID");


        }


   }
}
