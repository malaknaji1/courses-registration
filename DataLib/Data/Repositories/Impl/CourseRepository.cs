using courses_registration.Data;
using courses_registration.Interfaces;
using courses_registration.Models;
using Microsoft.EntityFrameworkCore;

namespace courses_registration.Repositories
{
    public class CourseRepository :ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public Course GetCourse(int id)
        {
            return _context.Courses.Where(c => c.Id == id && !c.IsDeleted).FirstOrDefault();
        }

        public bool CreateCourse(Course course)
        {
            _context.Add(course);
            return Save();
        }
        public bool UpdateCourse(Course course)
        {
            _context.Update(course);
            return Save();
        }
        public bool SoftDeleteCourse(Course course)
        {
            course.IsDeleted = true;

            var enrollments = _context.Enrollments
                           .Where(p => p.CourseId == course.Id && !p.IsDeleted)
                           .ToList();

            foreach (var enrollment in enrollments)
            {
                enrollment.IsDeleted = true;
                _context.Update(enrollment);
            }

            var prerequisites = _context.Prerequisites
                                .Where(p => p.CourseId == course.Id || p.PrerequisiteId==course.Id)
                                .ToList();

            foreach (var prerequisite in prerequisites)
            {
                prerequisite.IsDeleted = true;
                _context.Update(prerequisite);
            }

            _context.Update(course);
            return Save();

        }
        public bool IsCourseNameExisits(string name, int? id)
        {
            var query = _context.Courses.Where(course => course.Name.ToUpper() == name.ToUpper());

            if (id.HasValue)
            {
                query = query.Where(course => course.Id != id.Value);
            }

            return query.Any();
        }
        public List<Course> GetPrerequisiteCourses(int courseId)
        {
            return _context.Prerequisites
                .Where(p => p.CourseId == courseId)
                .Select(p => p.PrerequisiteCourse)
                .ToList();
        }
        public List<Student> GetStudentsForCourse(int courseId)
        {
            return _context.Enrollments
                .Where(p => p.CourseId == courseId && !p.IsDeleted && !p.Completed)
                .Select(p => p.Student)
                .ToList();
        }
        public bool CourseExists(int id)
        {
            return _context.Courses.Any(c => c.Id == id && !c.IsDeleted) ;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        } 
    }
}
