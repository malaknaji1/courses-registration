using courses_registration.Data;
using courses_registration.Interfaces;
using courses_registration.Models;
using Microsoft.EntityFrameworkCore;

namespace courses_registration.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public Student GetStudent(int id)
        {
            return _context.Students.Where(s => s.Id == id && !s.IsDeleted).FirstOrDefault();
        }

        public bool CreateStudent(Student student)
        {
            _context.Add(student);
            return Save();
        }

        public bool SoftDeleteStudent(Student student)
        {
            student.IsDeleted = true;
            var enrollments =_context.Enrollments
                            .Where(p => p.StudentId == student.Id && !p.IsDeleted)
                            .ToList();

            foreach (var enrollment in enrollments)
            {
                enrollment.IsDeleted = true;
                _context.Update(enrollment);
            }

            _context.Update(student);
            return Save();
        }

        public bool UpdateStudent(Student student)
        {
            _context.Update(student);
            return Save();
        }
        public bool StudentExists(int id)
        {
            return _context.Students.Any(s => s.Id == id && !s.IsDeleted);
        }
        public List<Course> GetCoursesForStudent(int studentId)
        {
            return _context.Enrollments
                .Where(p => p.StudentId == studentId && !p.IsDeleted)
                .Select(p => p.Course)
                .ToList();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
