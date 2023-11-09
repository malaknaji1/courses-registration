using courses_registration.Models;
using Microsoft.EntityFrameworkCore;

namespace courses_registration.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Prerequisite> Prerequisites { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Prerequisite>()
                .HasOne(p => p.Course)
                .WithMany(c => c.Prerequisites)
                .HasForeignKey(p => p.CourseId);

            modelBuilder.Entity<Prerequisite>()
                 .HasOne(p => p.PrerequisiteCourse)
                 .WithMany()
                 .HasForeignKey(p => p.PrerequisiteId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
