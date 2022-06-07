using Clean.Domain.Entity.Owner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Owner
{


    public class OwnerConfiguration : IEntityTypeConfiguration<AspNetOwners>
    {
        public void Configure(EntityTypeBuilder<AspNetOwners> entity)
        {

            entity.ToTable("AspNetOwners", "owner");
           
            entity.Property(e => e.Id)
                  .HasColumnName("ID")
                  .HasDefaultValueSql("nextval('AspNetOwners_ID_seq'::regclass)");

            entity.Property(e => e.ModifiedBy).HasColumnType("character varying");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.Property(e => e.ParentId).HasColumnName("ParentID");

            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

            entity.HasOne(d => d.Parent)
                .WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("fk_ParentID");

            entity.HasOne(d => d.Province)
                .WithMany()
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("fk_ProvinceID");

        }
    }
}
