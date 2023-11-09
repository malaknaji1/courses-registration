using courses_registration.Data;
using courses_registration.Interfaces;
using courses_registration.Models;
using System.Xml.Linq;

namespace courses_registration.Repositories
{
    public class PrerequisiteRepository : IPrerequisiteRepository
    {
        private readonly AppDbContext _context;

        public PrerequisiteRepository(AppDbContext context)
        {
            _context = context;
        }
        public Prerequisite GetPrerequisite(int id)
        {
            return _context.Prerequisites.Where(p => p.Id == id && !p.IsDeleted).FirstOrDefault();
        }
        public bool CreatePrerequisite(Prerequisite prerequisite)
        {
            _context.Add(prerequisite);
            return Save();
        }
        public bool UpdatePrerequisite(Prerequisite prerequisite)
        {
            _context.Update(prerequisite);
            return Save();
        }
        public bool SoftDeletePrerequisite(Prerequisite prerequisite)
        {
            prerequisite.IsDeleted = true;
            _context.Update(prerequisite);
            return Save();
        }
        public bool IsPrerequisiteCourseExisits(int courseId, int prerequisiteId)
        {
            return _context.Prerequisites.Any(p =>( p.CourseId == courseId && p.PrerequisiteId == prerequisiteId && !p.IsDeleted)  ||( p.CourseId == prerequisiteId && p.PrerequisiteId == courseId && !p.IsDeleted));
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool PrerequisiteExists(int id)
        {
            return _context.Prerequisites.Any(p => p.Id == id && !p.IsDeleted);
        }
    }
}
