using courses_registration.Data;
using courses_registration.Interfaces;
using courses_registration.Models;
using System.Linq;

namespace courses_registration.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool CreateEnrollment(Enrollment enrollment)
        {
            _context.Add(enrollment);
            return Save();
        }

        public bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.Id == id && !e.IsDeleted);
        }

        public Enrollment GetEnrollment(int id)
        {
            return _context.Enrollments.Where(e => e.Id == id && !e.IsDeleted).FirstOrDefault();
        }

        public bool IsCompletePrerequisites(int studentId, int courseId)
        {
            var prerequisiteIds = _context.Prerequisites
                                    .Where(p => p.CourseId == courseId && !p.IsDeleted)
                                    .Select(p => p.PrerequisiteId)
                                    .ToList();


            var completedPrerequisites = _context.Enrollments
                                            .Where(e => e.StudentId == studentId && prerequisiteIds.Contains(e.CourseId) && e.Completed && !e.IsDeleted)
                                            .Count();

            return prerequisiteIds.Count == completedPrerequisites;
        }
        public bool IsStudentRegistered(int studentId, int courseId)
        {
            return _context.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId && !e.IsDeleted);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool ChangeIsComplete(Enrollment enrollment, bool newIsCompleteValue)
        {
            enrollment.Completed = newIsCompleteValue;
            _context.Update(enrollment);
            return Save();
        }
        public bool SoftDeleteEnrollment(Enrollment enrollment)
        {
            enrollment.IsDeleted = true;
            _context.Update(enrollment);
            return Save();
        }

    }
}
