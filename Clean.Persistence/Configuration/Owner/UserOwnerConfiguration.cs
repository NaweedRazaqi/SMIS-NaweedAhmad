using Clean.Domain.Entity.Owner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Owner
{


    public class UserOwnerConfiguration : IEntityTypeConfiguration<UserOwner>
    {
        public void Configure(EntityTypeBuilder<UserOwner> entity)
        {

            entity.ToTable("UserOwner", "owner");

            entity.HasIndex(e => e.OwnerId)
                .HasName("fki_FK_Owner");

            entity.HasIndex(e => e.UserId)
                .HasName("fki_Fk_User");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ModifiedBy).HasColumnType("character varying");

            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            //entity.HasOne(d => d.User)
            //    .WithMany(p => p.UserOwner)
            //    .HasForeignKey(d => d.UserId)
            //    .HasConstraintName("Fk_User");
        }
    }
}



