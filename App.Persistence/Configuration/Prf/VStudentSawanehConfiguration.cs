using App.Domain.Entity.prf.view;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{


    public class VStudentSawanehConfiguration : IEntityTypeConfiguration<VStudentSawaneh>
    {

        public void Configure(EntityTypeBuilder<VStudentSawaneh> entity)
        {
            entity.HasNoKey();

            entity.ToTable("vStudentSawaneh", "prf");

            entity.Property(e => e.BirthLocationId).HasColumnName("BirthLocationID");

            entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

            entity.Property(e => e.Classtype)
                .HasColumnName("classtype")
                .HasColumnType("character varying");

            entity.Property(e => e.Code).HasMaxLength(50);

            entity.Property(e => e.FatherName).HasMaxLength(90);

            entity.Property(e => e.FatherNameEng).HasMaxLength(90);

            entity.Property(e => e.FatherProfession).HasColumnType("character varying");

            entity.Property(e => e.FirstName).HasMaxLength(90);

            entity.Property(e => e.FirstNameEng).HasMaxLength(90);

            entity.Property(e => e.GrandFatherName).HasMaxLength(90);

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.LastName).HasMaxLength(90);

            entity.Property(e => e.LastNameEng).HasMaxLength(90);

            entity.Property(e => e.MotherLanguage).HasMaxLength(100);

            entity.Property(e => e.MotherLanguageId).HasColumnName("MotherLanguageID");

            entity.Property(e => e.PermenentLocation).HasMaxLength(100);

            entity.Property(e => e.PhotoPath).HasMaxLength(500);

            entity.Property(e => e.ProfessionTypeId).HasColumnName("ProfessionTypeID");

            entity.Property(e => e.Reasons).HasMaxLength(2000);

            entity.Property(e => e.RelativeName).HasMaxLength(100);

            entity.Property(e => e.RelativeType).HasMaxLength(50);

            entity.Property(e => e.RelativeTypeId).HasColumnName("RelativeTypeID");

            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.Property(e => e.SchoolName).HasMaxLength(100);

            entity.Property(e => e.StudentHealth).HasMaxLength(1000);

            entity.Property(e => e.TdocumentNo)
                .HasColumnName("TDocumentNo")
                .HasMaxLength(50);
        }
    }
}