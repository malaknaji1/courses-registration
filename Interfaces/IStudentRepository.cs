using courses_registration.Models;
using Microsoft.EntityFrameworkCore;

namespace courses_registration.Interfaces
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);
        bool StudentExists(int id);
        bool CreateStudent(Student student);
        bool UpdateStudent(Student student);
        bool SoftDeleteStudent(Student student);
        List<Course> GetCoursesForStudent(int studentId);
        bool Save();
    }
}
