using courses_registration.Data;
using courses_registration.Models;
using Microsoft.EntityFrameworkCore;

namespace courses_registration.Seeders
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;
        public DatabaseSeeder(AppDbContext context)
        {
            _context = context;
        }
        public void SeedDataContext()
        {

            if (!_context.Courses.Any())
            {
                _context.Courses.AddRange(
                    new Course { Name = "Math" ,Capacity=20},
                    new Course { Name = "History", Capacity = 30 },
                    new Course { Name = "Computer Science", Capacity = 25 }
                );
            }

            if (!_context.Students.Any())
            {
                _context.Students.AddRange(
                    new Student { FirstName = "Sara", LastName = "Ali", Email = "sara.ali@example.com", Major = "Computer Science", City = "Hebron", Mobile = "0599499071" },
                    new Student { FirstName = "Haneen", LastName = "Khalid", Email = "haneen.khalid@example.com", Major = "Mathematics", City = "Hebron", Mobile = "0599499071" },
                    new Student { FirstName = "Malak", LastName = "naji", Email = "malak.naji@example.com", Major = "Computer Science", City = "Hebron", Mobile = "0599499071" }
                );
            }
          /*  if (!_context.Prerequisites.Any())
            {
                _context.Prerequisites.AddRange(
                    new Prerequisite { CourseId = 1, PrerequisiteId = 2 },
                    new Prerequisite { CourseId = 1, PrerequisiteId = 3 },
                    new Prerequisite { CourseId = 2, PrerequisiteId = 3 }
                );
            }

            if (!_context.Enrollments.Any())
            {
                _context.Enrollments.AddRange(
                    new Enrollment { StudentId = 1, CourseId = 3 },
                    new Enrollment { StudentId = 2, CourseId = 3, Completed = true },
                    new Enrollment { StudentId = 3, CourseId = 3 }
                );
            }*/

            _context.SaveChanges();
            
        }
    }
}
