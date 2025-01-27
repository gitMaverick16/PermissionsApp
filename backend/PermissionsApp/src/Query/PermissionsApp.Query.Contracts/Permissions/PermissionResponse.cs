namespace PermissionsApp.Query.Contracts.Permissions
{
    public record PermissionResponse(
        int Id,
        string EmployerName,
        string EmployerLastName,
        DateTime PermissionDate,
        int PermissionTypeId);
}
