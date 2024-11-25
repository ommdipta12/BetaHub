using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BetaHub.Auth.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblRoleMaster",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRoleMaster", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TblUserRegistration",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LockOutEnable = table.Column<bool>(type: "bit", nullable: false),
                    FailedCount = table.Column<byte>(type: "tinyint", nullable: false),
                    UnlockTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserRegistration", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "TblRoleMaster",
                columns: new[] { "RoleId", "Role" },
                values: new object[,]
                {
                    { 1, "SUPER_ADMIN" },
                    { 2, "DEVELOPER" },
                    { 3, "ADMIN" },
                    { 4, "USER" }
                });

            migrationBuilder.InsertData(
                table: "TblUserRegistration",
                columns: new[] { "UserId", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedFlag", "DeletedOn", "Email", "FailedCount", "LockOutEnable", "LoginType", "Mobile", "Name", "Password", "RoleId", "UnlockTime", "UpdatedBy", "UpdatedOn", "UserName" },
                values: new object[] { 1, 1, new DateTime(2024, 11, 25, 10, 38, 10, 81, DateTimeKind.Local).AddTicks(4240), null, false, null, "superadmin@betahub.com", (byte)0, false, "pwd", "9999999999", "SUPERADMIN", "", 1, null, null, null, "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblRoleMaster");

            migrationBuilder.DropTable(
                name: "TblUserRegistration");
        }
    }
}
