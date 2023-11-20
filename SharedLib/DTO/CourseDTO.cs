namespace courses_registration.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Capacity { get; set; }
    }
}
