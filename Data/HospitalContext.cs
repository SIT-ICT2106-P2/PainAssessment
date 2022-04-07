using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PainAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PainAssessment.Areas.Admin.Models;


namespace PainAssessment.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<ClinicalArea> ClinicalAreas { get; set; }
        public DbSet<PracticeType> PracticeTypes { get; set; }
        public DbSet<PainEducation> PainEducations { get; set; }

        public DbSet<Practitioner> Practitioners { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DbSet<PractitionerPatient> PractitionerPatients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //1. Use EntityFrameworkCoreExtensions (add DynamicDataMasking support)
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<TemplateChecklist> TemplateChecklists { get; set; }
        public DbSet<DefaultQuestion> DefaultQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalContext).Assembly);
            modelBuilder.Entity<DefaultQuestion>().HasKey(dQ => dQ.DQID);

        }
    }
}
