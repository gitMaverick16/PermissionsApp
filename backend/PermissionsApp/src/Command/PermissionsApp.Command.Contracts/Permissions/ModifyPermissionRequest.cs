namespace PermissionsApp.Command.Contracts.Permissions
{
    public record ModifyPermissionRequest(
        string EmployerName,
        string EmployerLastName,
        DateTime PermissionDate,
        int PermissionTypeId);
}
