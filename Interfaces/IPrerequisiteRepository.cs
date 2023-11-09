using courses_registration.Models;

namespace courses_registration.Interfaces
{
    public interface IPrerequisiteRepository
    {
        Prerequisite GetPrerequisite(int id);
        bool CreatePrerequisite(Prerequisite prerequisite);
        bool PrerequisiteExists(int id);
        bool IsPrerequisiteCourseExisits(int courseId,int prerequisiteId);
        bool UpdatePrerequisite(Prerequisite prerequisite);
        bool SoftDeletePrerequisite(Prerequisite prerequisite);
        bool Save();
    }
}
