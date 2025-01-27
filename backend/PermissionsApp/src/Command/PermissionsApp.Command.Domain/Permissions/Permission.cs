namespace PermissionsApp.Command.Domain.Permissions
{
    public class Permission
    {
        public int Id { get; set; }
        public string EmployerName { get; set; } = string.Empty;
        public string EmployerLastName { get; set; } = string.Empty;
        public DateTime PermissionDate { get; set; }
        public int PermissionTypeId { get; set; }
        public PermissionType PermissionType { get; set; } = null!;
    }
}
