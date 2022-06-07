using Clean.Domain.Entity.au;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Au
{
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> entity)
        {
            entity.ToTable("Audit", "au");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('au.\"Audit_ID_Seq\"'::regclass)");

            entity.Property(e => e.DbContextObject).HasMaxLength(100);

            entity.Property(e => e.DbObjectName).HasMaxLength(100);

            entity.Property(e => e.OperationTypeId).HasColumnName("OperationTypeID");

            entity.Property(e => e.RecordId)
                .HasColumnName("RecordID")
                .HasMaxLength(200);

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.Property(e => e.ValueAfter).HasMaxLength(1000);

            entity.Property(e => e.ValueBefore).HasMaxLength(1000);

            entity.HasOne(d => d.OperationType)
                .WithMany(p => p.Audit)
                .HasForeignKey(d => d.OperationTypeId)
                .HasConstraintName("audit_fk");
        }
    }
}
