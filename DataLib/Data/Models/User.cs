namespace courses_registration.Models
{
    public class User
    {
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required int UserTypeId { get; set; }
        public required Lookup UserType { get; set; }
        public bool IsDeleted { get; set; } = false;
       
    }
}
