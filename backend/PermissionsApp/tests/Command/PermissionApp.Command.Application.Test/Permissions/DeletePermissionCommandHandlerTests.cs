using ErrorOr;
using Moq;
using PermissionsApp.Command.Application.Common.Events;
using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Application.Permissions.Commands.DeletePermission;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Test.Permissions
{
    public class DeletePermissionCommandHandlerTests
    {
        private readonly Mock<IPermissionRepository> _mockPermissionRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPermissionEventProducer> _mockEventProducer;
        private readonly DeletePermissionCommandHandler _handler;

        public DeletePermissionCommandHandlerTests()
        {
            _mockPermissionRepository = new Mock<IPermissionRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEventProducer = new Mock<IPermissionEventProducer>();

            _handler = new DeletePermissionCommandHandler(
                _mockPermissionRepository.Object,
                _mockUnitOfWork.Object,
                _mockEventProducer.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldDeletePermissionSuccessfully()
        {
            // Arrange
            var permission = new Permission { Id = 1 };
            var command = new DeletePermissionCommand(permission.Id);
            var cancellationToken = new CancellationToken();

            _mockPermissionRepository
                .Setup(repo => repo.GetByIdAsync(command.Id))
                .ReturnsAsync(permission);

            _mockPermissionRepository
                .Setup(repo => repo.RemovePermissionAsync(permission))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork
                .Setup(uow => uow.CommitChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(!result.IsError);
            Assert.Equal(Result.Deleted, result.Value);
        }

        [Fact]
        public async Task Handle_ShouldCallRemovePermissionAndCommitChanges()
        {
            // Arrange
            var permission = new Permission { Id = 1 };
            var command = new DeletePermissionCommand(permission.Id);
            var cancellationToken = new CancellationToken();

            _mockPermissionRepository
                .Setup(repo => repo.GetByIdAsync(command.Id))
                .ReturnsAsync(permission);

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockPermissionRepository.Verify(repo => repo.RemovePermissionAsync(permission), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldProduceEventAfterDeletingPermission()
        {
            // Arrange
            var permission = new Permission { Id = 1 };
            var command = new DeletePermissionCommand(permission.Id);
            var cancellationToken = new CancellationToken();

            _mockPermissionRepository
                .Setup(repo => repo.GetByIdAsync(command.Id))
                .ReturnsAsync(permission);

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockEventProducer.Verify(producer =>
                producer.Produce("permissions_topic", It.Is<PermissionEvent>(e =>
                    e.Action == ActionType.Delete &&
                    e.Id == permission.Id
                )), Times.Once);
        }
    }
}
