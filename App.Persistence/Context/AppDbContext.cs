
using App.Domain.Entity.look;
using App.Domain.Entity.prf;
using App.Domain.Entity.prf.view;
using Clean.Common;
using Clean.Domain.Entity.look;
using Clean.Persistence.Context;
using Clean.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace App.Persistence.Context
{
    public class AppDbContext : BaseContext
    {
        public static readonly ILoggerFactory DbLogger = LoggerFactory.Create(ex => ex.AddConsole());

        public AppDbContext(DbContextOptions<AppDbContext> options, UserManager<AppUser> manager) : base(options, manager)
        {

        }

        public virtual DbSet<Clean.Domain.Entity.look.Country> Countries { get; set; }
        
        
        public virtual DbSet<Clean.Domain.Entity.look.District> Districts { get; set; }
        public virtual DbSet<App.Domain.Entity.look.Pdistrict> Pdistricts { get; set; }
        
        public virtual DbSet<Occupation> Occupations { get; set; }
        public virtual DbSet<Domain.Entity.look.Office> Offices { get; set; }

        public virtual DbSet<Domain.Entity.prf.Profile>  Profiles { get; set; }
        public virtual DbSet<Domain.Entity.prf.QuranChapterMemorize>  QuranChapterMemorizes { get; set; }

        public virtual DbSet<Domain.Entity.prf.Application> Applications { get; set; }
        public virtual DbSet<StudentResultSheet> StudentResultSheets { get; set; }
        public virtual DbSet<VStudentSawaneh> VStudentSawanehs { get; set; }
        public virtual DbSet<ProfileProcess> ProfileProcesses { get; set; }
        public virtual DbSet<App.Domain.Entity.look.Location> Locations { get; set; }
        public virtual DbSet<StudentSubjectMarks> StudentSubjectMarks { get; set; }
        public virtual DbSet<VStudentsTranscript> VStudentsTranscripts { get; set; }
        
        public virtual DbSet<App.Domain.Entity.look.Ethnicity> Ethnicities { get; set; }

        public virtual DbSet<App.Domain.Entity.look.Religion> Religions { get; set; }
        
        public virtual DbSet<Clean.Domain.Entity.look.Province> Provinces { get; set; }
        public virtual DbSet<App.Domain.Entity.look.Gender> Genders { get; set; }

        public virtual DbSet<App.Domain.Entity.look.BloodGroup> BloodGroups { get; set; }

        public virtual DbSet<App.Domain.Entity.look.MaritalStatus> MaritalStatus { get; set; }

        public virtual DbSet<App.Domain.Entity.look.ClassType> ClassTypes { get; set; }
        public virtual DbSet<App.Domain.Entity.look.SchoolCategory> SchoolCategories { get; set; }
        public virtual DbSet<App.Domain.Entity.look.School> Schools { get; set; }
        public virtual DbSet<App.Domain.Entity.look.Languages> Languages { get; set; }
        public virtual DbSet<App.Domain.Entity.look.SchoolType> SchoolTypes { get; set; }

        public virtual DbSet<App.Domain.Entity.look.RelativesType> RetativesTypes { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.StudentClass> StudentClasses { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.Relatives> Relatives { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.Termination> Terminations { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.StudentHealthReport> StudentHealthReports { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.Jobs> Jobs { get; set; }

        public virtual DbSet<App.Domain.Entity.prf.Rellocation> Rellocations { get; set; }
        public virtual DbSet<App.Domain.Entity.look.Profession> Professions { get; set; }
        public virtual DbSet<App.Domain.Entity.look.ExamType> ExamTypes { get; set; }
        public virtual DbSet<App.Domain.Entity.look.IslamicEducationType> IslamicEducationTypes { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.StudentRegisteration> StudentRegisterations { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.ScheduleExam> ScheduleExams { get; set; }

        public virtual DbSet<RegistrationType> RegistrationType { get; set; }


        public virtual DbSet<App.Domain.Entity.prf.ClassSubjectManagement> ClassSubjectManagements { get; set; }

        public virtual DbSet<App.Domain.Entity.prf.ClassManagement> ClassManagements { get; set; }

        public virtual DbSet<App.Domain.Entity.prf.PrimarySecondaryResult> PrimarySecondaryResults { get; set; }

        public virtual DbSet<App.Domain.Entity.look.Result> Results { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.StudentResult> StudentResults { get; set; }


        public virtual DbSet<Domain.Entity.prf.SubjectManagement> SubjectManagements { get; set; }
        public virtual DbSet<App.Domain.Entity.look.Status> Status { get; set; }
        public virtual DbSet<App.Domain.Entity.look.Year> Years { get; set; }
        public virtual DbSet<Domain.Entity.prf.StudentClassMarks> StudentClassMarks { get; set; }
        public virtual DbSet<Domain.Entity.prf.ClassUpgradation> ClassUpgradations { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.Teacher> Teachers { get; set; }
        //
        public virtual DbSet<App.Domain.Entity.look.Service> Services { get; set; }
        public virtual DbSet<App.Domain.Entity.look.ExamType> ExamType { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.ResultSheet> ResultSheets { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.SubjectAssignment> SubjectAssignments { get; set; }
        public virtual DbSet<App.Domain.Entity.prf.StudentClassResultSheet> StudentClassResultSheets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseLoggerFactory(DbLogger);
                options.EnableSensitiveDataLogging(true);
                options.UseNpgsql(AppConfig.BaseConnectionString, (opts) =>
                {
                    
                });
            }
            base.OnConfiguring(options);
        }
    }
}
