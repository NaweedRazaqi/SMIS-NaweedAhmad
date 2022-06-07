using App.Domain.Entity.prf.view;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{


    public class vStudentClassResultSheetConfiguration : IEntityTypeConfiguration<StudentResultSheet>
    {
        public void Configure(EntityTypeBuilder<StudentResultSheet> entity)
        {
            entity.HasNoKey();

            entity.ToTable("vStudentClassResultSheet", "prf");

            entity.Property(e => e.BirthLocationId).HasColumnName("BirthLocationID");

            entity.Property(e => e.ClassManagementId).HasColumnName("ClassManagementID");

            entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

            entity.Property(e => e.Code).HasMaxLength(50);

            entity.Property(e => e.EthnicityId).HasColumnName("EthnicityID");

            entity.Property(e => e.FatherName).HasMaxLength(90);

            entity.Property(e => e.FatherNameEng).HasMaxLength(90);

            entity.Property(e => e.FirstName).HasMaxLength(90);

            entity.Property(e => e.FirstNameEng).HasMaxLength(90);

            entity.Property(e => e.GenderId).HasColumnName("GenderID");

            entity.Property(e => e.GrandFatherName).HasMaxLength(90);

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.LastName).HasMaxLength(90);

            entity.Property(e => e.LastNameEng).HasMaxLength(90);

            entity.Property(e => e.Mobile).HasMaxLength(500);

            entity.Property(e => e.MotherLanguageId).HasColumnName("MotherLanguageID");

            entity.Property(e => e.NationalId)
                .HasColumnName("NationalID")
                .HasMaxLength(100);

            entity.Property(e => e.PhotoPath).HasMaxLength(500);

            entity.Property(e => e.Prefix).HasMaxLength(50);

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.Property(e => e.SchoolTypeId).HasColumnName("SchoolTypeID");

            entity.Property(e => e.StudentClassManagement).HasMaxLength(500);

            entity.Property(e => e.StudentClassName).HasColumnType("character varying");

            entity.Property(e => e.StudentSchoolName).HasMaxLength(100);

            entity.Property(e => e.StudentSchoolType).HasMaxLength(100);
        }
    }
}
