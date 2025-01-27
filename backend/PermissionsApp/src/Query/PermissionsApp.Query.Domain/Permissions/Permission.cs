namespace PermissionsApp.Query.Domain.Permissions
{
    public class Permission
    {
        public int Id { get; set; }
        public string EmployerName { get; set; }
        public string EmployerLastName { get; set; }
        public DateTime PermissionDate { get; set; }
        public PermissionType PermissionType { get; set; }
    }
}
