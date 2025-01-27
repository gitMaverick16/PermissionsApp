using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PermissionsApp.Command.Infrastructure.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "EmployerLastName", "EmployerName", "PermissionDate", "PermissionTypeId" },
                values: new object[,]
                {
                    { 1, "Doe", "Jhon", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Smith", "Jane", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Johnson", "Emily", new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, "Brown", "Olivia", new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 5, "Martinez", "Lucas", new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
