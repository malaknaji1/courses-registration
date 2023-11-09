namespace courses_registration.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Major { get; set; }
        public string? Mobile { get; set; }
        public string? City { get; set; }
    }
}
