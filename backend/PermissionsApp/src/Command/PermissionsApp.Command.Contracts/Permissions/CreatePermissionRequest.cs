namespace PermissionsApp.Command.Contracts.Permissions
{
    public record CreatePermissionRequest(
        string EmployeeName,
        string EmployeeLastName,
        DateTime PermissionDate,
        int PermissionTypeId);
}
