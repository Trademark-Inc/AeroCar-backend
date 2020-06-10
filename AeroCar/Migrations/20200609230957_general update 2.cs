using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class generalupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReservations_UserProfiles_UserProfileId",
                table: "CarReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightReservations_UserProfiles_UserProfileId",
                table: "FlightReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_UserProfiles_UserProfileId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_UserProfileId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_FlightReservations_UserProfileId",
                table: "FlightReservations");

            migrationBuilder.DropIndex(
                name: "IX_CarReservations_UserProfileId",
                table: "CarReservations");

            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "FlightReservations");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "CarReservations");

            migrationBuilder.AddColumn<long>(
                name: "FlightReservationId",
                table: "PriceListItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegularUserId",
                table: "Friends",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FlightReservations",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "RegularUserId",
                table: "FlightReservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegularUserId",
                table: "CarReservations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bonus",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItems_FlightReservationId",
                table: "PriceListItems",
                column: "FlightReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_RegularUserId",
                table: "Friends",
                column: "RegularUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightReservations_RegularUserId",
                table: "FlightReservations",
                column: "RegularUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_RegularUserId",
                table: "CarReservations",
                column: "RegularUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarReservations_AspNetUsers_RegularUserId",
                table: "CarReservations",
                column: "RegularUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightReservations_AspNetUsers_RegularUserId",
                table: "FlightReservations",
                column: "RegularUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_RegularUserId",
                table: "Friends",
                column: "RegularUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItems_FlightReservations_FlightReservationId",
                table: "PriceListItems",
                column: "FlightReservationId",
                principalTable: "FlightReservations",
                principalColumn: "FlightReservationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReservations_AspNetUsers_RegularUserId",
                table: "CarReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightReservations_AspNetUsers_RegularUserId",
                table: "FlightReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_RegularUserId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItems_FlightReservations_FlightReservationId",
                table: "PriceListItems");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItems_FlightReservationId",
                table: "PriceListItems");

            migrationBuilder.DropIndex(
                name: "IX_Friends_RegularUserId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_FlightReservations_RegularUserId",
                table: "FlightReservations");

            migrationBuilder.DropIndex(
                name: "IX_CarReservations_RegularUserId",
                table: "CarReservations");

            migrationBuilder.DropColumn(
                name: "FlightReservationId",
                table: "PriceListItems");

            migrationBuilder.DropColumn(
                name: "RegularUserId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "RegularUserId",
                table: "FlightReservations");

            migrationBuilder.DropColumn(
                name: "RegularUserId",
                table: "CarReservations");

            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Bonus",
                table: "UserProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UserProfileId",
                table: "Friends",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "FlightReservations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserProfileId",
                table: "FlightReservations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserProfileId",
                table: "CarReservations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UserProfileId",
                table: "Friends",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightReservations_UserProfileId",
                table: "FlightReservations",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_UserProfileId",
                table: "CarReservations",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarReservations_UserProfiles_UserProfileId",
                table: "CarReservations",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightReservations_UserProfiles_UserProfileId",
                table: "FlightReservations",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_UserProfiles_UserProfileId",
                table: "Friends",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
