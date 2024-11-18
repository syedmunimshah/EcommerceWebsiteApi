using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWebsiteDbConnection.Migrations
{
    /// <inheritdoc />
    public partial class roletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_assignRoleRoles");

            migrationBuilder.CreateTable(
                name: "tbl_UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_UserRole_tbl_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_UserRole_tbl_users_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserRole_RoleId",
                table: "tbl_UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserRole_UserId",
                table: "tbl_UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_UserRole");

            migrationBuilder.CreateTable(
                name: "tbl_assignRoleRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_assignRoleRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_assignRoleRoles_tbl_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "tbl_Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_assignRoleRoles_tbl_admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "tbl_admin",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_assignRoleRoles_tbl_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_assignRoleRoles_tbl_users_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_assignRoleRoles_AdminId",
                table: "tbl_assignRoleRoles",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_assignRoleRoles_CustomerId",
                table: "tbl_assignRoleRoles",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_assignRoleRoles_RoleId",
                table: "tbl_assignRoleRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_assignRoleRoles_UserId",
                table: "tbl_assignRoleRoles",
                column: "UserId");
        }
    }
}
