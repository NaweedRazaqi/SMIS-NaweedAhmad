using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Clean.UI.ssModel
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<Case> Case { get; set; }
        public virtual DbSet<ComplainType> ComplainType { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<OperationType> OperationType { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<ProcessConnection> ProcessConnection { get; set; }
        public virtual DbSet<ProcessTracking> ProcessTracking { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Registeration> Registeration { get; set; }
        public virtual DbSet<Screen> Screen { get; set; }
        public virtual DbSet<ScreenDocument> ScreenDocument { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<SystemStatus> SystemStatus { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=localhost; port=5432; Database=ComplainsTracking; Username=postgres; Password=newmOOn@16;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DbContextObject).HasMaxLength(100);

                entity.Property(e => e.DbObjectName).HasMaxLength(100);

                entity.Property(e => e.OperationTypeId).HasColumnName("OperationTypeID");

                entity.Property(e => e.RecordId)
                    .HasColumnName("RecordID")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ValueAfter).HasMaxLength(1000);

                entity.Property(e => e.ValueBefore).HasMaxLength(1000);

                entity.HasOne(d => d.OperationType)
                    .WithMany(p => p.Audit)
                    .HasForeignKey(d => d.OperationTypeId)
                    .HasConstraintName("audit_fk");
            });

            modelBuilder.Entity<Case>(entity =>
            {
                entity.ToTable("Case", "look");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasColumnType("character varying");
            });

            modelBuilder.Entity<ComplainType>(entity =>
            {
                entity.ToTable("ComplainType", "prc");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasColumnType("character varying");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("DocumentType", "doc");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Documents>(entity =>
            {
                entity.ToTable("Documents", "doc");

                entity.HasIndex(e => e.RecordId)
                    .HasName("fki_StuentClassResultSheetID");

                entity.Property(e => e.Id).HasColumnName("ID");

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

            modelBuilder.Entity<OperationType>(entity =>
            {
                entity.ToTable("OperationType", "au");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OperationTypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.ToTable("Process", "prc");

                entity.HasIndex(e => e.ComplainTypeId)
                    .HasName("fki_ComplainType_FK");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ComplainTypeId).HasColumnName("ComplainTypeID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

                entity.Property(e => e.Sorter).HasMaxLength(10);

                entity.HasOne(d => d.ComplainType)
                    .WithMany(p => p.Process)
                    .HasForeignKey(d => d.ComplainTypeId)
                    .HasConstraintName("ComplainType_FK");

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.Process)
                    .HasForeignKey(d => d.ScreenId)
                    .HasConstraintName("Process_FK");
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
                    .HasConstraintName("ProcessConnection_FK_1");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessConnectionProcess)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProcessConnection_FK");
            });

            modelBuilder.Entity<ProcessTracking>(entity =>
            {
                entity.ToTable("ProcessTracking", "prc");

                entity.HasIndex(e => e.StatusId)
                    .HasName("fki_Status_FK");

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

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ProcessTracking)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProcessTracking_FK_2");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessTracking)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProcessTracking_FK");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile", "dbo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.FatherName).HasMaxLength(250);

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<Registeration>(entity =>
            {
                entity.ToTable("Registeration", "dbo");

                entity.HasIndex(e => e.CaseId)
                    .HasName("fki_FK_R_Case");

                entity.HasIndex(e => e.StatusId)
                    .HasName("fki_FK_R_Status");

                entity.HasIndex(e => e.UnitId)
                    .HasName("fki_FK_R_Unit");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CaseId).HasColumnName("CaseID");

                entity.Property(e => e.Discription).HasMaxLength(2000);

                entity.Property(e => e.FatherName).HasMaxLength(250);

                entity.Property(e => e.Modifiedby).HasColumnType("character varying");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Phone).HasColumnType("character varying");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.Registeration)
                    .HasForeignKey(d => d.CaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_R_Case");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Registeration)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_R_Profile");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Registeration)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_R_Status");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Registeration)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_R_Unit");
            });

            modelBuilder.Entity<Screen>(entity =>
            {
                entity.ToTable("Screen", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

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

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status", "look");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasColumnType("character varying");
            });

            modelBuilder.Entity<SystemStatus>(entity =>
            {
                entity.ToTable("SystemStatus", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Sorter)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StatusType)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("Unit", "look");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
