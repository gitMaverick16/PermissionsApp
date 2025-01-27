using PermissionsApp.Command.Application.Common.Events;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Common.Interfaces
{
    public interface IPermissionEventProducer
    {
        void Produce(string topic, PermissionEvent permission);
    }
}
