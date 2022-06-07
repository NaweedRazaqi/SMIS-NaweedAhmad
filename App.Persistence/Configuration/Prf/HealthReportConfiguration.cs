using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class HealthReportConfiguration : IEntityTypeConfiguration<StudentHealthReport>
    {
        public void Configure(EntityTypeBuilder<StudentHealthReport> entity)
        {
            entity.ToTable("StudentHealthReport", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.AttachmentPath).HasColumnType("character varying");

            entity.Property(e => e.Description).HasColumnType("character varying");

            entity.Property(e => e.ModifiedBy).HasMaxLength(200);

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.HasOne(d => d.Profile)
                .WithMany(p => p.StudentHealthReports)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_ID");
        }
    }
}
