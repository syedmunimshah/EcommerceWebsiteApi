using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWebsiteDbConnection.Migrations
{
    /// <inheritdoc />
    public partial class assignrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_userRoles");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tbl_Product",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "tbl_assignRoleRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_assignRoleRoles");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tbl_Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_userRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_userRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_userRoles_tbl_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_userRoles_tbl_users_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_userRoles_RoleId",
                table: "tbl_userRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_userRoles_UserId",
                table: "tbl_userRoles",
                column: "UserId");
        }
    }
}
