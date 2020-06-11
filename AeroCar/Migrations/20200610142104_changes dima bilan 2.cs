using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class changesdimabilan2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItems_AvioCompanies_AvioCompanyId",
                table: "PriceListItems");

            migrationBuilder.AlterColumn<long>(
                name: "AvioCompanyId",
                table: "PriceListItems",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItems_AvioCompanies_AvioCompanyId",
                table: "PriceListItems",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItems_AvioCompanies_AvioCompanyId",
                table: "PriceListItems");

            migrationBuilder.AlterColumn<long>(
                name: "AvioCompanyId",
                table: "PriceListItems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItems_AvioCompanies_AvioCompanyId",
                table: "PriceListItems",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
