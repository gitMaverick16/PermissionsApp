namespace PermissionsApp.Query.Infrastructure.Events
{
    public class PermissionEvent
    {
        public int Id { get; set; }
        public string EmployerName { get; set; }
        public string EmployerLastName { get; set; }
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
