namespace PermissionsApp.Command.Domain.Permissions
{
    public class Permission
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public DateTime PermissionDate { get; set; }
        public int PermissionTypeId { get; set; }
        public PermissionType PermissionType { get; set; } = null!;
    }
}
