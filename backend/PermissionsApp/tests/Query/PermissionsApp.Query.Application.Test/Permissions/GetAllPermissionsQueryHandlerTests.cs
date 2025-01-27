using Moq;
using PermissionsApp.Query.Application.Common.Interfaces;
using PermissionsApp.Query.Application.Permissions.Queries.GetAllPermissions;
using PermissionsApp.Query.Domain.Permissions;

namespace PermissionsApp.Query.Application.Test.Permissions
{
    public class GetAllPermissionsQueryHandlerTests
    {
        private readonly Mock<IPermissionRepository> _mockPermissionRepository;
        private readonly GetAllPermissionsQueryHandler _handler;

        public GetAllPermissionsQueryHandlerTests()
        {
            _mockPermissionRepository = new Mock<IPermissionRepository>();
            _handler = new GetAllPermissionsQueryHandler(_mockPermissionRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfPermissions()
        {
            // Arrange
            var permissions = new List<Permission>
        {
            new Permission { Id = 1, EmployeeName = "John", EmployeeLastName = "Doe", PermissionDate = DateTime.UtcNow, PermissionType = new PermissionType{ Id = 1, Description= "vacations"} },
            new Permission { Id = 2, EmployeeName = "Jane", EmployeeLastName = "Smith", PermissionDate = DateTime.UtcNow, PermissionType = new PermissionType{ Id = 1, Description= "maternity"} }
        };

            _mockPermissionRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(permissions);

            var query = new GetAllPermissionsQuery();
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(query, cancellationToken);

            // Assert
            Assert.True(!result.IsError);
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyListIfNoPermissionsExist()
        {
            // Arrange
            _mockPermissionRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Permission>());

            var query = new GetAllPermissionsQuery();
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(query, cancellationToken);

            // Assert
            Assert.True(!result.IsError);
            Assert.Empty(result.Value);
        }
    }
}
