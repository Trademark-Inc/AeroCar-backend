using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class updatedadmins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CarAdmins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AvioAdmins",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CarAdmins");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AvioAdmins");
        }
    }
}
