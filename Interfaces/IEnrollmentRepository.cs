using courses_registration.Models;

namespace courses_registration.Interfaces
{
    public interface IEnrollmentRepository
    {
        Enrollment GetEnrollment(int id);
        bool CreateEnrollment(Enrollment enrollment);
        bool EnrollmentExists(int id);
        bool IsCompletePrerequisites(int studentId , int courseId);
        bool IsStudentRegistered(int studentId, int courseId);
        bool ChangeIsComplete(Enrollment enrollment, bool newIsCompleteValue);
        bool SoftDeleteEnrollment(Enrollment enrollment);
        bool Save();
    }
}
