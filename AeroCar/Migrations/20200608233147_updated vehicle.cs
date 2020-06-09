using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class updatedvehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarCompanies_CarCompanyId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "AvioCompanyId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<long>(
                name: "CarCompanyId",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarCompanies_CarCompanyId",
                table: "Vehicles",
                column: "CarCompanyId",
                principalTable: "CarCompanies",
                principalColumn: "CarCompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarCompanies_CarCompanyId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<long>(
                name: "CarCompanyId",
                table: "Vehicles",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "AvioCompanyId",
                table: "Vehicles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarCompanies_CarCompanyId",
                table: "Vehicles",
                column: "CarCompanyId",
                principalTable: "CarCompanies",
                principalColumn: "CarCompanyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
