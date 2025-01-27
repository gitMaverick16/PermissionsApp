using ErrorOr;
using Moq;
using PermissionsApp.Query.Application.Common.Interfaces;
using PermissionsApp.Query.Application.Permissions.Queries.GetPermission;
using PermissionsApp.Query.Domain.Permissions;

namespace PermissionsApp.Query.Application.Test.Permissions
{
    public class GetPermissionQueryHandlerTests
    {
        private readonly Mock<IPermissionRepository> _mockPermissionRepository;
        private readonly GetPermissionQueryHandler _handler;

        public GetPermissionQueryHandlerTests()
        {
            _mockPermissionRepository = new Mock<IPermissionRepository>();
            _handler = new GetPermissionQueryHandler(_mockPermissionRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPermission_WhenIdExists()
        {
            // Arrange
            var permissionId = 1;
            var permission = new Permission
            {
                Id = permissionId,
                EmployeeName = "John",
                EmployeeLastName = "Doe",
                PermissionDate = DateTime.UtcNow,
                PermissionType = new PermissionType
                {
                    Id = 1,
                    Description = "Vacations"
                }
            };

            _mockPermissionRepository
                .Setup(repo => repo.GetByIdAsync(permissionId))
                .ReturnsAsync(permission);

            var query = new GetPermissionQuery(permissionId);
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(query, cancellationToken);

            // Assert
            Assert.True(!result.IsError);
            Assert.Equal(permissionId, result.Value.Id);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFoundError_WhenIdDoesNotExist()
        {
            // Arrange
            var permissionId = 1000;

            _mockPermissionRepository
                .Setup(repo => repo.GetByIdAsync(permissionId))
                .ReturnsAsync((Permission)null);

            var query = new GetPermissionQuery(permissionId);
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(query, cancellationToken);

            // Assert
            Assert.True(result.IsError);
            Assert.Contains(result.Errors, error => error.Type == ErrorType.NotFound);
        }
    }
}
