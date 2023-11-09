using courses_registration.Models;

namespace courses_registration.Interfaces
{
    public interface ICourseRepository
    {
        Course GetCourse(int id);
        bool CourseExists(int id);
        bool CreateCourse(Course course);
        bool IsCourseNameExisits(string name, int? id=null);
        bool UpdateCourse(Course course);
        public List<Course> GetPrerequisiteCourses(int courseId);
        List<Student> GetStudentsForCourse(int courseId);
        bool SoftDeleteCourse(Course course);
        bool Save();

    }
}
