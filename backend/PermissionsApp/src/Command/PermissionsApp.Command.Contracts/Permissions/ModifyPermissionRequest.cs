namespace PermissionsApp.Command.Contracts.Permissions
{
    public record ModifyPermissionRequest(
        string EmployeeName,
        string EmployeeLastName,
        DateTime PermissionDate,
        int PermissionTypeId);
}
