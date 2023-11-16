namespace courses_registration.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required int UserTypeId { get; set; }
    }
}
