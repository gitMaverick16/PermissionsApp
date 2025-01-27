using Moq;
using PermissionsApp.Command.Application.Common.Events;
using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Application.Permissions.Commands.ModifyPermission;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Test.Permissions
{
    public class ModifyPermissionCommandHandlerTests
    {
        private readonly Mock<IPermissionRepository> _mockPermissionRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPermissionEventProducer> _mockEventProducer;
        private readonly ModifyPermissionCommandHandler _handler;

        public ModifyPermissionCommandHandlerTests()
        {
            _mockPermissionRepository = new Mock<IPermissionRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEventProducer = new Mock<IPermissionEventProducer>();

            _handler = new ModifyPermissionCommandHandler(
                _mockPermissionRepository.Object,
                _mockUnitOfWork.Object,
                _mockEventProducer.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldModifyPermissionSuccessfully()
        {
            // Arrange
            var permission = new Permission
            {
                Id = 1,
                EmployeeName = "John",
                EmployeeLastName = "Doe",
                PermissionDate = DateTime.UtcNow,
                PermissionTypeId = 1
            };

            var command = new ModifyPermissionCommand(
                permission.Id,
                "Jane",
                "Smith",
                DateTime.UtcNow.AddDays(1),
                2
            );

            var cancellationToken = new CancellationToken();

            _mockPermissionRepository
                .Setup(repo => repo.GetByIdAsync(command.PermissionId))
                .ReturnsAsync(permission);

            _mockPermissionRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Permission>()))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork
                .Setup(uow => uow.CommitChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(!result.IsError);
            Assert.Equal(command.EmployeeName, result.Value.EmployeeName);
            Assert.Equal(command.EmployeeLastName, result.Value.EmployeeLastName);
            Assert.Equal(command.PermissionDate, result.Value.PermissionDate);
            Assert.Equal(command.PermissionTypeId, result.Value.PermissionTypeId);
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateAsyncAndCommitChanges()
        {
            // Arrange
            var permission = new Permission
            {
                Id = 1,
                EmployeeName = "John",
                EmployeeLastName = "Doe",
                PermissionDate = DateTime.UtcNow,
                PermissionTypeId = 1
            };

            var command = new ModifyPermissionCommand(permission.Id, "Jane", "Smith", DateTime.UtcNow.AddDays(1), 2);
            var cancellationToken = new CancellationToken();

            _mockPermissionRepository
                .Setup(repo => repo.GetByIdAsync(command.PermissionId))
                .ReturnsAsync(permission);

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockPermissionRepository.Verify(repo => repo.UpdateAsync(permission), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldProduceEventAfterModifyingPermission()
        {
            // Arrange
            var permission = new Permission
            {
                Id = 1,
                EmployeeName = "John",
                EmployeeLastName = "Doe",
                PermissionDate = DateTime.UtcNow,
                PermissionTypeId = 1
            };

            var command = new ModifyPermissionCommand(permission.Id, "Jane", "Smith", DateTime.UtcNow.AddDays(1), 2);
            var cancellationToken = new CancellationToken();

            _mockPermissionRepository
                .Setup(repo => repo.GetByIdAsync(command.PermissionId))
                .ReturnsAsync(permission);

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockEventProducer.Verify(producer =>
                producer.Produce("permissions_topic", It.Is<PermissionEvent>(e =>
                    e.Action == ActionType.Modify &&
                    e.Id == permission.Id &&
                    e.EmployeeName == command.EmployeeName &&
                    e.EmployeeLastName == command.EmployeeLastName &&
                    e.PermissionDate == command.PermissionDate &&
                    e.PermissionTypeId == command.PermissionTypeId
                )), Times.Once);
        }
    }
}
