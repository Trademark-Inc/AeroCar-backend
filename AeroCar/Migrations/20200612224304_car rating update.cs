using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class carratingupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CarReservationId",
                table: "CarRatings",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CarReservationId",
                table: "CarCompanyRatings",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarReservationId",
                table: "CarRatings");

            migrationBuilder.DropColumn(
                name: "CarReservationId",
                table: "CarCompanyRatings");
        }
    }
}
