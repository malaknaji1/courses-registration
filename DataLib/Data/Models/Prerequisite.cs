using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courses_registration.Models
{
    public class Prerequisite
    {
        [Key]
        public int Id { get; set; }
      
        public required int CourseId { get; set; }
   
        public  required int PrerequisiteId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public  Course Course { get; set; }
        public  Course PrerequisiteCourse { get; set; }
    }
}
