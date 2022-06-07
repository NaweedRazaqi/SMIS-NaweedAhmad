using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{

    class QuranChapterMemorizeConfiguration : IEntityTypeConfiguration<QuranChapterMemorize>
    {
        public void Configure(EntityTypeBuilder<QuranChapterMemorize> entity)
        {

            entity.ToTable("QuranChapterMemorize", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Chapter).HasColumnType("character varying");

            entity.Property(e => e.ModifiedBy).HasMaxLength(100);

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.HasOne(d => d.Profile)
                .WithMany()
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_ID");
        }
    }
}
