using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class changedadmins2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    UserId = table.Column<string>(nullable: false),
                    AvioCompanyId = table.Column<long>(nullable: false),
                    SetUpPassword = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvioAdmins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "CarAdmins",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    CarCompanyId = table.Column<long>(nullable: false),
                    SetUpPassword = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAdmins", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvioAdmins");

            migrationBuilder.DropTable(
                name: "CarAdmins");

            migrationBuilder.AddColumn<long>(
                name: "AvioCompanyId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SetUpPassword",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CarCompanyId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CarAdmin_SetUpPassword",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");
        }
    }
}
