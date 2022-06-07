using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class SubjectManagementConfiguration : IEntityTypeConfiguration<SubjectManagement>
    {

        public void Configure(EntityTypeBuilder<SubjectManagement> entity)
        {
            entity.ToTable("SubjectManagement", "adm");

            entity.Property(e => e.Id)
                   .HasColumnName("ID")
                   .HasIdentityOptions(null, null, null, 9223372036854L, null, null)
                   .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp without time zone");

            entity.Property(e => e.ModifiedBy).HasMaxLength(1000);

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.Property(e => e.NameEng).HasMaxLength(100);

            entity.Property(e => e.ReferenceNo).HasMaxLength(10);

            entity.Property(e => e.Remarks).HasMaxLength(1000);

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.Property(e => e.SchoolTypeId).HasColumnName("SchoolTypeID");
            entity.HasOne(d => d.SchoolType)
                .WithMany(p => p.SubjectManagements)
                .HasForeignKey(d => d.SchoolTypeId)
                .HasConstraintName("FK_SchoolType");

        }
    }
}
