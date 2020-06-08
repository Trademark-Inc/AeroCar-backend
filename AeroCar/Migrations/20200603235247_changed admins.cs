using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class changedadmins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvioAdmins");

            migrationBuilder.DropTable(
                name: "CarAdmins");

            migrationBuilder.AddColumn<long>(
                name: "AvioCompanyId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SetUpPassword",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CarCompanyId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CarAdmin_SetUpPassword",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvioCompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SetUpPassword",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CarCompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CarAdmin_SetUpPassword",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "AvioAdmins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    AvioCompanyId = table.Column<long>(type: "bigint", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PasswordHash = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SetUpPassword = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvioAdmins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarAdmins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    CarCompanyId = table.Column<long>(type: "bigint", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PasswordHash = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SetUpPassword = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAdmins", x => x.Id);
                });
        }
    }
}
