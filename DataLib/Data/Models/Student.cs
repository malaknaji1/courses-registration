namespace courses_registration.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Major { get; set; }
        public string? Mobile { get; set; }
        public string? City { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Enrollment>? Enrollments { get; set; }
    }
}
