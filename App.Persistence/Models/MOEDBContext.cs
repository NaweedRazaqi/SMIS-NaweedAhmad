using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App.Persistence.Models
{
    public partial class MOEDBContext : DbContext
    {
        public MOEDBContext()
        {
        }

        public MOEDBContext(DbContextOptions<MOEDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<BloodGroup> BloodGroup { get; set; }
        public virtual DbSet<ClassManagement> ClassManagement { get; set; }
        public virtual DbSet<ClassSubjectManagement> ClassSubjectManagement { get; set; }
        public virtual DbSet<ClassType> ClassType { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<DisableType> DisableType { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<Ethnicity> Ethnicity { get; set; }
        public virtual DbSet<EventReason> EventReason { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<HighSchoolStudentClassMarks> HighSchoolStudentClassMarks { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<LocationType> LocationType { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<MinistryRec> MinistryRec { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Office> Office { get; set; }
        public virtual DbSet<OperationType> OperationType { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<OrganizationType> OrganizationType { get; set; }
        public virtual DbSet<PrimarySecondaryResult> PrimarySecondaryResult { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<ProcessConnection> ProcessConnection { get; set; }
        public virtual DbSet<ProcessTracking> ProcessTracking { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Rank> Rank { get; set; }
        public virtual DbSet<Relation> Relation { get; set; }
        public virtual DbSet<Religion> Religion { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<RoleScreen> RoleScreen { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<SchoolType> SchoolType { get; set; }
        public virtual DbSet<Screen> Screen { get; set; }
        public virtual DbSet<ScreenDocument> ScreenDocument { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<StudentClass> StudentClass { get; set; }
        public virtual DbSet<StudentClassResult> StudentClassResult { get; set; }
        public virtual DbSet<SubjectManagement> SubjectManagement { get; set; }
        public virtual DbSet<VProfileProcess> VProfileProcess { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server = localhost; port=5432; User Id=postgres; Password=newmOOn@16; Database=MOEDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.ToTable("AddressType", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("Application", "prf");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApplicationReasonId).HasColumnName("ApplicationReasonID");

                entity.Property(e => e.ApplicationTypeId).HasColumnName("ApplicationTypeID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.ReferenceNo).HasMaxLength(50);

                entity.Property(e => e.RegistrationTypeId).HasColumnName("RegistrationTypeID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.HasIndex(e => e.OfficeId);

                entity.HasIndex(e => e.OrganizationId);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEnd).HasColumnType("timestamp with time zone");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("Audit", "au");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DbContextObject).HasMaxLength(100);

                entity.Property(e => e.DbObjectName).HasMaxLength(100);

                entity.Property(e => e.OperationTypeId).HasColumnName("OperationTypeID");

                entity.Property(e => e.RecordId)
                    .HasColumnName("RecordID")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ValueAfter).HasColumnType("character varying");

                entity.Property(e => e.ValueBefore).HasColumnType("character varying");

                entity.HasOne(d => d.OperationType)
                    .WithMany(p => p.Audit)
                    .HasForeignKey(d => d.OperationTypeId)
                    .HasConstraintName("audit_fk");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Bank", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<BloodGroup>(entity =>
            {
                entity.ToTable("BloodGroup", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.bloodgroup_seq'::regclass)");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<ClassManagement>(entity =>
            {
                entity.ToTable("ClassManagement", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('prf.\"ClassManagement_ID_Seq\"'::regclass)");

                entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

                entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.NameEng).HasMaxLength(500);

                entity.Property(e => e.ReferenceNo).HasMaxLength(10);

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.HasOne(d => d.ClassType)
                    .WithMany(p => p.ClassManagement)
                    .HasForeignKey(d => d.ClassTypeId)
                    .HasConstraintName("FK_ClassManagement_ClassTypeID");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ClassManagement)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_ClassManagement_SchoolID");
            });

            modelBuilder.Entity<ClassSubjectManagement>(entity =>
            {
                entity.ToTable("ClassSubjectManagement", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('prf.\"ClassSubject_ID_Seq\"'::regclass)");

                entity.Property(e => e.ClassManagementId).HasColumnName("ClassManagementID");

                entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

                entity.Property(e => e.ReferenceNo).HasMaxLength(10);

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.HasOne(d => d.ClassManagement)
                    .WithMany(p => p.ClassSubjectManagement)
                    .HasForeignKey(d => d.ClassManagementId)
                    .HasConstraintName("FK_ClassSubjectManagement_ClassManagementID");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.ClassSubjectManagement)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_ClassSubjectManagement_SubjectID");
            });

            modelBuilder.Entity<ClassType>(entity =>
            {
                entity.ToTable("ClassType", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DariName).HasColumnType("character varying");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PashtoName).HasColumnType("character varying");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<DisableType>(entity =>
            {
                entity.ToTable("DisableType", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.disabletype_seq'::regclass)");

                entity.Property(e => e.DFrom).HasColumnName("D_From");

                entity.Property(e => e.DTo).HasColumnName("D_To");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("DocumentType", "doc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Category).HasColumnType("character varying");

                entity.Property(e => e.Description).HasColumnType("character varying");

                entity.Property(e => e.Name).HasColumnType("character varying");
            });

            modelBuilder.Entity<Documents>(entity =>
            {
                entity.ToTable("Documents", "doc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DocumentDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.DocumentNumber).HasMaxLength(100);

                entity.Property(e => e.DocumentSource).HasMaxLength(100);

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.EncryptionKey).HasMaxLength(500);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastDownloadDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ObjectName).HasMaxLength(100);

                entity.Property(e => e.ObjectSchema).HasMaxLength(100);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Root).HasMaxLength(200);

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UploadDate).HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_Documents__FK");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("Education", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.education_seq'::regclass)");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Ethnicity>(entity =>
            {
                entity.ToTable("Ethnicity", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<EventReason>(entity =>
            {
                entity.ToTable("EventReason", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.eventreason_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.gender_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<HighSchoolStudentClassMarks>(entity =>
            {
                entity.ToTable("HighSchoolStudentClassMarks", "prf");

                entity.HasIndex(e => e.StudentClassId)
                    .HasName("fki_FK_ClassManagemen");

                entity.HasIndex(e => e.SubjectId)
                    .HasName("fki_FK_Subject");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('prf.\"HighSchool_Student_Class_Marks_ID_Seq\"'::regclass)");

                entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

                entity.Property(e => e.ReferenceNo).HasMaxLength(10);

                entity.Property(e => e.StudentClassId).HasColumnName("StudentClassID");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.HighSchoolStudentClassMarks)
                    .HasForeignKey(d => d.StudentClassId)
                    .HasConstraintName("FK_ClassManagemen");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.HighSchoolStudentClassMarks)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_Subject");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(e => e.Dari)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Path).HasMaxLength(400);

                entity.Property(e => e.PathDari).HasMaxLength(400);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<LocationType>(entity =>
            {
                entity.ToTable("LocationType", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.ToTable("MaritalStatus", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.maritalstatus_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MinistryRec>(entity =>
            {
                entity.ToTable("MinistryRec", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.ministryrec_seq'::regclass)");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("Module", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.ToTable("Office", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.OfficeTypeId).HasColumnName("OfficeTypeID");

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Office)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("office_fk");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Office)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("office_fk_1");
            });

            modelBuilder.Entity<OperationType>(entity =>
            {
                entity.ToTable("OperationType", "au");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OperationTypeName).HasColumnType("character varying");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization", "look");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Dari)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Pashto)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.OrganizationType)
                    .WithMany(p => p.Organization)
                    .HasForeignKey(d => d.OrganizationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Organization_OrganizationType_OrganizationTypeID");
            });

            modelBuilder.Entity<OrganizationType>(entity =>
            {
                entity.ToTable("OrganizationType", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.organizationtype_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PrimarySecondaryResult>(entity =>
            {
                entity.ToTable("PrimarySecondaryResult", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('prf.\"Primary_Secondary_ID_Seq\"'::regclass)");

                entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

                entity.Property(e => e.Pathfile)
                    .HasColumnName("pathfile")
                    .HasMaxLength(500);

                entity.Property(e => e.ReferenceNo).HasMaxLength(10);

                entity.Property(e => e.ResultId).HasColumnName("ResultID");

                entity.Property(e => e.StudentClassId).HasColumnName("StudentClassID");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.PrimarySecondaryResult)
                    .HasForeignKey(d => d.ResultId)
                    .HasConstraintName("FK_Primary_Secondary_Result_ResultID");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.PrimarySecondaryResult)
                    .HasForeignKey(d => d.StudentClassId)
                    .HasConstraintName("FK_Primary_Secondary_Result_StudentClassID");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.ToTable("Process", "prc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

                entity.Property(e => e.Sorter).HasMaxLength(10);
            });

            modelBuilder.Entity<ProcessConnection>(entity =>
            {
                entity.ToTable("ProcessConnection", "prc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.HasOne(d => d.ConnectedToNavigation)
                    .WithMany(p => p.ProcessConnectionConnectedToNavigation)
                    .HasForeignKey(d => d.ConnectedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessConnection__FK_1");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessConnectionProcess)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessConnection__FK");
            });

            modelBuilder.Entity<ProcessTracking>(entity =>
            {
                entity.ToTable("ProcessTracking", "prc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.ReferedProcessId).HasColumnName("ReferedProcessID");

                entity.Property(e => e.Remarks).HasMaxLength(1000);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.ToUserId).HasColumnName("ToUserID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessTracking)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessTracking__FK");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile", "prf");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BirthLocationId).HasColumnName("BirthLocationID");

                entity.Property(e => e.BloodGroupId).HasColumnName("BloodGroupID");

                entity.Property(e => e.Cdistrict).HasColumnName("CDistrict");

                entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Cprovince).HasColumnName("CProvince");

                entity.Property(e => e.Cvillage)
                    .HasColumnName("CVillage")
                    .HasMaxLength(100);

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.EducationTypeId).HasColumnName("EducationTypeID");

                entity.Property(e => e.EthnicityId).HasColumnName("EthnicityID");

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasMaxLength(90);

                entity.Property(e => e.FatherNameEng)
                    .IsRequired()
                    .HasMaxLength(90);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(90);

                entity.Property(e => e.FirstNameEng)
                    .IsRequired()
                    .HasMaxLength(90);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.GrandFatherName)
                    .IsRequired()
                    .HasMaxLength(90);

                entity.Property(e => e.LastName).HasMaxLength(90);

                entity.Property(e => e.LastNameEng)
                    .IsRequired()
                    .HasMaxLength(90);

                entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

                entity.Property(e => e.Mobile).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

                entity.Property(e => e.NationalId)
                    .IsRequired()
                    .HasColumnName("NationalID")
                    .HasMaxLength(100);

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.PhotoPath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ReferenceNo).HasMaxLength(10);

                entity.Property(e => e.ReligionId).HasColumnName("ReligionID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.BirthLocation)
                    .WithMany(p => p.ProfileBirthLocation)
                    .HasForeignKey(d => d.BirthLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profile_Location");

                entity.HasOne(d => d.BloodGroup)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.BloodGroupId)
                    .HasConstraintName("FK_Profile_BloodGroup");

                entity.HasOne(d => d.CdistrictNavigation)
                    .WithMany(p => p.ProfileCdistrictNavigation)
                    .HasForeignKey(d => d.Cdistrict)
                    .HasConstraintName("FK_Profile_Location3");

                entity.HasOne(d => d.ClassType)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.ClassTypeId)
                    .HasConstraintName("FK_ClassTypeID");

                entity.HasOne(d => d.CprovinceNavigation)
                    .WithMany(p => p.ProfileCprovinceNavigation)
                    .HasForeignKey(d => d.Cprovince)
                    .HasConstraintName("FK_Profile_Location4");

                entity.HasOne(d => d.DistrictNavigation)
                    .WithMany(p => p.ProfileDistrictNavigation)
                    .HasForeignKey(d => d.District)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profile_Location2");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profile_DocumentType");

                entity.HasOne(d => d.EducationType)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.EducationTypeId)
                    .HasConstraintName("FK_Profile_Education");

                entity.HasOne(d => d.Ethnicity)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.EthnicityId)
                    .HasConstraintName("FK_Profile_Ethnicity");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profile_Gender");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profile_MaritalStatus");

                entity.HasOne(d => d.ProvinceNavigation)
                    .WithMany(p => p.ProfileProvinceNavigation)
                    .HasForeignKey(d => d.Province)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profile_Location1");

                entity.HasOne(d => d.Religion)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.ReligionId)
                    .HasConstraintName("FK_Profile_Religion");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolID");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("Rank", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.rank_seq'::regclass)");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<Relation>(entity =>
            {
                entity.ToTable("Relation", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Religion>(entity =>
            {
                entity.ToTable("Religion", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("Result", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DariName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PashtoName).HasMaxLength(50);
            });

            modelBuilder.Entity<RoleScreen>(entity =>
            {
                entity.ToTable("RoleScreen", "look");

                entity.HasIndex(e => e.ScreenId);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("School", "look");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Dari)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Pashto)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.SchoolType)
                    .WithMany(p => p.School)
                    .HasForeignKey(d => d.SchoolTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_School_SchoolType_SchoolTypeID");
            });

            modelBuilder.Entity<SchoolType>(entity =>
            {
                entity.ToTable("SchoolType", "look");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Screen>(entity =>
            {
                entity.ToTable("Screen", "look");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.DirectoryPath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Screen)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("screen_fk");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("screen_parent_fk");
            });

            modelBuilder.Entity<ScreenDocument>(entity =>
            {
                entity.ToTable("ScreenDocument", "doc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.ScreenDocument)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .HasConstraintName("_ScreenDocument__FK");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.status_seq'::regclass)");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.Dari).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Pashto).HasMaxLength(50);
            });

            modelBuilder.Entity<StudentClass>(entity =>
            {
                entity.ToTable("StudentClass", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('prf.\"StudentClass_ID_Seq\"'::regclass)");

                entity.Property(e => e.ClassManagementId).HasColumnName("ClassManagementID");

                entity.Property(e => e.ClassTypeId).HasColumnName("ClassTypeID");

                entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.ReferenceNo).HasMaxLength(10);

                entity.HasOne(d => d.ClassManagement)
                    .WithMany(p => p.StudentClass)
                    .HasForeignKey(d => d.ClassManagementId)
                    .HasConstraintName("FK_StudentClass_ClassManagementID");

                entity.HasOne(d => d.ClassType)
                    .WithMany(p => p.StudentClass)
                    .HasForeignKey(d => d.ClassTypeId)
                    .HasConstraintName("FK_StudentClass_ClassTypeID");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.StudentClass)
                    .HasForeignKey(d => d.ProfileId)
                    .HasConstraintName("FK_StudentClass_ProfileID");
            });

            modelBuilder.Entity<StudentClassResult>(entity =>
            {
                entity.ToTable("StudentClassResult", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('prf.\"Student_Class_Results_ID_Seq\"'::regclass)");

                entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

                entity.Property(e => e.ReferenceNo).HasMaxLength(10);

                entity.Property(e => e.ResultId).HasColumnName("ResultID");

                entity.Property(e => e.StudentClassId).HasColumnName("StudentClassID");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.StudentClassResult)
                    .HasForeignKey(d => d.ResultId)
                    .HasConstraintName("FK_ResultID");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.StudentClassResult)
                    .HasForeignKey(d => d.StudentClassId)
                    .HasConstraintName("FK_StudentClassID");
            });

            modelBuilder.Entity<SubjectManagement>(entity =>
            {
                entity.ToTable("SubjectManagement", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('prf.subjectmanagement_seq'::regclass)");

                entity.Property(e => e.CreatedOn).HasColumnType("time without time zone");

                entity.Property(e => e.ModifiedBy).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.NameEng).HasMaxLength(100);

                entity.Property(e => e.ReferenceNo).HasMaxLength(10);

                entity.Property(e => e.Remarks).HasMaxLength(1000);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<VProfileProcess>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vProfileProcess", "prf");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.ServiceTypeId).HasColumnName("ServiceTypeID");
            });

            modelBuilder.HasSequence("applicationreason_seq", "look").StartsAt(11);

            modelBuilder.HasSequence("applicationtype_seq", "look").StartsAt(3);

            modelBuilder.HasSequence("bloodgroup_seq", "look").StartsAt(10);

            modelBuilder.HasSequence("categorytype_seq", "look").StartsAt(8);

            modelBuilder.HasSequence("disabletype_seq", "look").StartsAt(3);

            modelBuilder.HasSequence("documenttype_seq", "look").StartsAt(27);

            modelBuilder.HasSequence("education_seq", "look").StartsAt(5);

            modelBuilder.HasSequence("eventreason_seq", "look").StartsAt(1007);

            modelBuilder.HasSequence("gender_seq", "look").StartsAt(3);

            modelBuilder.HasSequence("heritagejob_seq", "look").StartsAt(3);

            modelBuilder.HasSequence("lawyertype_seq", "look").StartsAt(4);

            modelBuilder.HasSequence("maritalstatus_seq", "look").StartsAt(5);

            modelBuilder.HasSequence("ministryrec_seq", "look").StartsAt(4);

            modelBuilder.HasSequence("organizationtype_seq", "look").StartsAt(2);

            modelBuilder.HasSequence("paymentperiod_seq", "look").StartsAt(4);

            modelBuilder.HasSequence("processconnection_seq", "look").StartsAt(23);

            modelBuilder.HasSequence("province_seq", "look");

            modelBuilder.HasSequence("rank_seq", "look").StartsAt(106);

            modelBuilder.HasSequence("rolescreens_seq", "look").StartsAt(1207);

            modelBuilder.HasSequence("serviecetype_seq", "look").StartsAt(3);

            modelBuilder.HasSequence("status_seq", "look").StartsAt(6);

            modelBuilder.HasSequence("address_seq", "prf");

            modelBuilder.HasSequence("application_seq", "prf").StartsAt(30199);

            modelBuilder.HasSequence("ClassManagement_ID_Seq", "prf").StartsAt(2);

            modelBuilder.HasSequence("ClassSubject_ID_Seq", "prf").StartsAt(2);

            modelBuilder.HasSequence("documents_seq", "prf").StartsAt(90);

            modelBuilder.HasSequence("HighSchool_Student_Class_Marks_ID_Seq", "prf");

            modelBuilder.HasSequence("job_seq", "prf").StartsAt(20160);

            modelBuilder.HasSequence("Primary_Secondary_ID_Seq", "prf").StartsAt(2);

            modelBuilder.HasSequence("profile_seq", "prf").StartsAt(72490);

            modelBuilder.HasSequence("screendocuments_seq", "prf").StartsAt(48);

            modelBuilder.HasSequence("Student_Class_Results_ID_Seq", "prf");

            modelBuilder.HasSequence("StudentClass_ID_Seq", "prf").StartsAt(2);

            modelBuilder.HasSequence("subjectmanagement_seq", "prf").StartsAt(73225);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
