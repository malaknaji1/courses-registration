namespace courses_registration.DTO
{
    public class PrerequisiteDTO
    {
        public int Id { get; set; }
        public required int CourseId { get; set; }
        public required int PrerequisiteId { get; set; }

    }
}
