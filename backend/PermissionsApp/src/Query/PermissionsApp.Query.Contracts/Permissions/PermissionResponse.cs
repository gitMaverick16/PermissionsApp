namespace PermissionsApp.Query.Contracts.Permissions
{
    public record PermissionResponse(
        int Id,
        string EmployeeName,
        string EmployeeLastName,
        DateTime PermissionDate,
        int PermissionTypeId,
        string PermissionTypeDescription);
}
