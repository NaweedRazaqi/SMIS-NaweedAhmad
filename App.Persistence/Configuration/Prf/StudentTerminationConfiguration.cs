using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
  public class StudentTerminationConfiguration : IEntityTypeConfiguration<Termination>
    {
      
        public void Configure(EntityTypeBuilder<Termination> entity)
        {
            entity.ToTable("Termination", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.DocumentNo).HasMaxLength(50);

            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

            entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.Reasons).HasMaxLength(2000);

            entity.HasOne(d => d.DocumentType)
                .WithMany()
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("FK_DocumentTypeID");
            entity.HasOne(d => d.ClassType)
                    .WithMany()
                    .HasForeignKey(d => d.ClassTypeId)
                    .HasConstraintName("FK_ClassTypeID");

            entity.HasOne(d => d.Profile)
                .WithMany(p => p.Termination)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_ID");
        }
    }
}
