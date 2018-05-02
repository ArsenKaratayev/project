using System;
using EF.Models;
using Microsoft.EntityFrameworkCore;

namespace EF.Models
{
    public class RypDbContext : DbContext
    {
        public RypDbContext()
        {
            
        }

        public RypDbContext(DbContextOptions<RypDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ryp.db");
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<SemesterSubject>().HasKey(t => new { t.SemesterId, t.SubjectId });
            modelBuilder.Entity<SemesterElectiveGroup>().HasKey(t => new { t.SemesterId, t.ElectiveGroupId });
            modelBuilder.Entity<SubjectElectiveGroup>().HasKey(t => new { t.ElectiveGroupId, t.SubjectId });

            modelBuilder.Entity<Subject>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<SubjectType>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Specialty>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Ryp>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Semester>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<ElectiveGroup>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<SubjectPrerequisiteSubject>()
                .HasKey(e => new { e.Id });
                //.HasKey(e => new { e.PrimaryId, e.RelatedId });

            modelBuilder.Entity<SubjectPrerequisiteSubject>()
                .HasOne(d => d.Primary)
                .WithMany(p => p.RelatedItems)
                .HasForeignKey(d => d.PrimaryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubjectPrerequisiteSubject>()
                .HasOne(d => d.Related)
                .WithMany(p => p.RelatedTo)
                .HasForeignKey(d => d.RelatedId)
                .OnDelete(DeleteBehavior.Restrict);
            
            //modelBuilder.Entity<Subject>()
                //.HasMany(oj => oj.Prerequisites)
                //.WithOne(j => j.Parent)
                //.HasForeignKey(j => j.ParentId);

            base.OnModelCreating(modelBuilder);
		}

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectType> SubjectTypes { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<ElectiveGroup> ElectiveGroups { get; set; }
        public DbSet<Ryp> Ryps { get; set; }

        public DbSet<Semester> Semesters { get; set; }
        public DbSet<SemesterSubject> SemesterSubjects { get; set; }
        public DbSet<SemesterElectiveGroup> SemesterElectiveGroups { get; set; }

        public DbSet<SubjectElectiveGroup> SubjectElectiveGroups { get; set; }
        public DbSet<SubjectPrerequisiteSubject> SubjectPrerequisiteSubjects { get; set; }
    }
}
