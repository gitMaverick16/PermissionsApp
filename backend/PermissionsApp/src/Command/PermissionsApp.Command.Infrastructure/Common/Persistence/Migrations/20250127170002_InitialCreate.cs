﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PermissionsApp.Command.Infrastructure.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermissionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_PermissionTypes_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalTable: "PermissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PermissionTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Sick Leave" },
                    { 2, "Vacation" },
                    { 3, "Maternity Leave" },
                    { 4, "Paternity Leave" },
                    { 5, "Personal Leave" },
                    { 6, "Bereavement Leave" },
                    { 7, "Medical Leave" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "EmployeeLastName", "EmployeeName", "PermissionDate", "PermissionTypeId" },
                values: new object[,]
                {
                    { 1, "Doe", "Jhon", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Smith", "Jane", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Johnson", "Emily", new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, "Brown", "Olivia", new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 5, "Martinez", "Lucas", new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionTypeId",
                table: "Permissions",
                column: "PermissionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PermissionTypes");
        }
    }
}
