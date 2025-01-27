namespace PermissionsApp.Command.Infrastructure.Configurations
{
    public class KafkaSettings
    {
        public string HostName { get; set; }
        public string Port { get; set; }
        public string GroupId { get; set; }
    }
}
