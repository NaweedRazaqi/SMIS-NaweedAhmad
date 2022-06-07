using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    class TeacherProfileConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> entity)
        {
            entity.ToTable("Profile", "teacher");

            entity.HasIndex(e => e.BirthLocationId)
                .HasName("fki_FK_BirthLocatonID");

            entity.HasIndex(e => e.District)
                .HasName("fki_FK_District");

            entity.HasIndex(e => e.DocumentTypeId)
                .HasName("fki_FK_DocumentTypeID");

            entity.HasIndex(e => e.EthnicityId)
                .HasName("fki_FK_EthinicityID");

            entity.HasIndex(e => e.GenderId)
                .HasName("fki_GenderId");

            entity.HasIndex(e => e.MaritalStatusId)
                .HasName("fki_FK_MaritalStatus");

            entity.HasIndex(e => e.OfficeId)
                .HasName("fki_FK_OfficeID");

            entity.HasIndex(e => e.Province)
                .HasName("fki_FK_Province");

            entity.HasIndex(e => e.ReligionId)
                .HasName("fki_FK_ReligionID");

            entity.HasIndex(e => e.SarviceTypId)
                .HasName("fki_FK_ServiceTypeID");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.BirthLocationId).HasColumnName("BirthLocationID");

            entity.Property(e => e.Code).HasMaxLength(50);

            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

            entity.Property(e => e.Email).HasMaxLength(150);

            entity.Property(e => e.EthnicityId).HasColumnName("EthnicityID");

            entity.Property(e => e.FatherName).HasMaxLength(90);

            entity.Property(e => e.FatherNameEng).HasColumnType("character varying");

            entity.Property(e => e.FirstNameEng).HasMaxLength(250);

            entity.Property(e => e.LastName).HasColumnType("character varying");

            entity.Property(e => e.LastNameEng).HasColumnType("character varying");

            entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

            entity.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.Name).HasMaxLength(90);

            entity.Property(e => e.NationalId)
                .HasColumnName("NationalID")
                .HasMaxLength(100);

            entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

            entity.Property(e => e.Phone).HasMaxLength(100);

            entity.Property(e => e.PhotoPath).HasMaxLength(500);

            entity.Property(e => e.ReligionId).HasColumnName("ReligionID");

            entity.Property(e => e.Salary).HasMaxLength(200);

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.BirthLocation)
                .WithMany(p => p.Teachers)
                .HasForeignKey(d => d.BirthLocationId)
                .HasConstraintName("FK_BirthLocatonID");

            entity.HasOne(d => d.DistrictNavigation)
                .WithMany(p => p.TeachDistrictNavigation)
                .HasForeignKey(d => d.District)
                .HasConstraintName("FK_District");

            entity.HasOne(d => d.DocumentType)
                .WithMany()
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("FK_DocumentTypeID");

            entity.HasOne(d => d.Ethnicity)
                .WithMany(p => p.Teachers)
                .HasForeignKey(d => d.EthnicityId)
                .HasConstraintName("FK_EthinicityID");

            entity.HasOne(d => d.Gender)
                .WithMany(p => p.Teacher)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("GenderId");

            entity.HasOne(d => d.MaritalStatus)
                .WithMany(p => p.Teachers)
                .HasForeignKey(d => d.MaritalStatusId)
                .HasConstraintName("FK_MaritalStatus");

            entity.HasOne(d => d.Office)
                .WithMany(p => p.Teachers)
                .HasForeignKey(d => d.OfficeId)
                .HasConstraintName("FK_OfficeID");

            entity.HasOne(d => d.ProvinceNavigation)
                .WithMany(p => p.TeachProviceNavigation)
                .HasForeignKey(d => d.Province)
                .HasConstraintName("FK_Province");

            entity.HasOne(d => d.Religion)
                .WithMany(p => p.Teachers)
                .HasForeignKey(d => d.ReligionId)
                .HasConstraintName("FK_ReligionID");

            entity.HasOne(d => d.SarviceTyp)
                .WithMany(p => p.Teacher)
                .HasForeignKey(d => d.SarviceTypId)
                .HasConstraintName("FK_ServiceTypeID");
        }
    }
}
