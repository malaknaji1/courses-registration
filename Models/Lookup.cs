namespace courses_registration.Models
{
    public class Lookup
    {
        public int Id { get; set; }
        public int LookupId { get; set; }
        public required string LookupName { get; set; }
        public required string LookupValue { get; set; }
        public List<User>? Users { get; set; }
        public List<UserPermission>? UserTypePermissions { get; set; }
        public List<UserPermission>? PermissionType { get; set; }

    }
}
