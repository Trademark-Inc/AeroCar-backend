using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class companiesandprofilesupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarCompanyId",
                table: "CarCompanyProfiles");

            migrationBuilder.DropColumn(
                name: "AvioCompanyId",
                table: "AvioCompanyProfiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CarCompanyId",
                table: "CarCompanyProfiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AvioCompanyId",
                table: "AvioCompanyProfiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
