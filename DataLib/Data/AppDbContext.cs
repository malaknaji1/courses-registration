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
        public DbSet<User> Users { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

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

            modelBuilder.Entity<User>()
            .HasOne(u => u.UserType)
            .WithMany(l => l.Users)
            .HasForeignKey(u => u.UserTypeId);

            modelBuilder.Entity<UserPermission>()
            .HasOne(up => up.UserType)
            .WithMany(l => l.UserTypePermissions)
            .HasForeignKey(up => up.UserTypeId);

            modelBuilder.Entity<UserPermission>()
                .HasOne(up => up.PermissionType)
                .WithMany(l => l.PermissionType)
                .HasForeignKey(up => up.PermissionTypeId);

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}
