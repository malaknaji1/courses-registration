using System.ComponentModel.DataAnnotations;

namespace courses_registration.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Capacity { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Enrollment>? Enrollments { get; set; }
        public List<Prerequisite>? Prerequisites { get; set; }
    }
}
