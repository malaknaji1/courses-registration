namespace courses_registration.DTO
{
    public class EnrollmentDTO
    {
        public int Id { get; set; }
        public required int StudentId { get; set; }
        public required int CourseId { get; set; }
    }
}
