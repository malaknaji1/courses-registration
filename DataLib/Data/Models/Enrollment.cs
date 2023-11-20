using System.ComponentModel.DataAnnotations;

namespace courses_registration.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public required int StudentId { get; set; }
        public required int CourseId { get; set; }
        public bool Completed { get; set; }=false;
        public bool IsDeleted { get; set; } = false;
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
