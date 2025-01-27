using Moq;
using PermissionsApp.Command.Application.Common.Events;
using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Application.Permissions.Commands.CreatePermission;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Test.Permissions
{
    public class CreatePermissionCommandHandlerTests
    {
        private readonly Mock<IPermissionRepository> _mockPermissionRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPermissionEventProducer> _mockEventProducer;
        private readonly CreatePermissionCommandHandler _handler;

        public CreatePermissionCommandHandlerTests()
        {
            _mockPermissionRepository = new Mock<IPermissionRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEventProducer = new Mock<IPermissionEventProducer>();

            _handler = new CreatePermissionCommandHandler(
                _mockPermissionRepository.Object,
                _mockUnitOfWork.Object,
                _mockEventProducer.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldCreatePermissionSuccessfully()
        {
            // Arrange
            var command = new CreatePermissionCommand(
                "John",
                "Doe",
                DateTime.UtcNow,
                2);

            var cancellationToken = new CancellationToken();

            _mockPermissionRepository
                .Setup(repo => repo.AddPermissionAsync(It.IsAny<Permission>()))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork
                .Setup(uow => uow.CommitChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(!result.IsError);
            Assert.NotNull(result.Value);
            Assert.Equal(command.EmployeeName, result.Value.EmployeeName);
            Assert.Equal(command.EmployeeLastName, result.Value.EmployeeLastName);
            Assert.Equal(command.PermissionDate, result.Value.PermissionDate);
            Assert.Equal(command.PermissionTypeId, result.Value.PermissionTypeId);
        }

        [Fact]
        public async Task Handle_ShouldCallAddPermissionAndCommitChanges()
        {
            // Arrange
            var command = new CreatePermissionCommand
            (
                "Jane",
                "Smith",
                DateTime.UtcNow,
                1
            );

            var cancellationToken = new CancellationToken();

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockPermissionRepository.Verify(repo => repo.AddPermissionAsync(It.IsAny<Permission>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldProduceEventAfterCreatingPermission()
        {
            // Arrange
            var command = new CreatePermissionCommand
            (
                "Carlos",
                "Lopez",
                DateTime.UtcNow,
                3
            );

            var cancellationToken = new CancellationToken();

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockEventProducer.Verify(producer =>
                producer.Produce("permissions_topic", It.Is<PermissionEvent>(e =>
                    e.Action == ActionType.Add &&
                    e.EmployeeName == command.EmployeeName &&
                    e.EmployeeLastName == command.EmployeeLastName &&
                    e.PermissionDate == command.PermissionDate &&
                    e.PermissionTypeId == command.PermissionTypeId
                )), Times.Once);
        }

    }
}
