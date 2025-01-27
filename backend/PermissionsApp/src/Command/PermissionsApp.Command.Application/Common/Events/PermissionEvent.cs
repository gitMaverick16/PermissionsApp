namespace PermissionsApp.Command.Application.Common.Events
{
    public class PermissionEvent
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public DateTime PermissionDate { get; set; }
        public int PermissionTypeId { get; set; }
        public string PermissionTypeDescription { get; set; }
        public ActionType Action { get; set; }
    }
    public enum ActionType
    {
        Add,
        Modify,
        Delete
    }
}
