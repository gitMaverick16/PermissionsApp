namespace PermissionsApp.Command.Contracts.Permissions
{
    public record CreatePermissionRequest(
        string EmployerName,
        string EmployerLastName,
        DateTime PermissionDate,
        int PermissionId);
}
