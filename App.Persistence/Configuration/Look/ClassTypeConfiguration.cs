using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class ClassTypeConfiguration: IEntityTypeConfiguration<ClassType>
    {
        public void Configure(EntityTypeBuilder<ClassType> entity)
        {

            entity.ToTable("ClassType", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.DariName).HasColumnType("character varying");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(e => e.PashtoName).HasColumnType("character varying");


        }
    }
}
