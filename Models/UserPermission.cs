namespace courses_registration.Models
{
    public class UserPermission
    {
        public int Id { get; set; }
        public required int UserTypeId { get; set; }
        public required Lookup UserType { get; set; }
        public required int PermissionTypeId { get; set; }
        public required Lookup PermissionType { get; set; }
    }
}
