using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class generalupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReservations_Destination_PickUpLocationDestinationId",
                table: "CarReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_CarReservations_Destination_ReturnLocationDestinationId",
                table: "CarReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Destination_AvioCompanies_AvioCompanyId",
                table: "Destination");

            migrationBuilder.DropForeignKey(
                name: "FK_Destination_Flights_FlightId",
                table: "Destination");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Destination_DepartureLocationDestinationId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Destination_LocationDestinationId",
                table: "Offices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Destination",
                table: "Destination");

            migrationBuilder.RenameTable(
                name: "Destination",
                newName: "Destinations");

            migrationBuilder.RenameIndex(
                name: "IX_Destination_FlightId",
                table: "Destinations",
                newName: "IX_Destinations_FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_Destination_AvioCompanyId",
                table: "Destinations",
                newName: "IX_Destinations_AvioCompanyId");

            migrationBuilder.AlterColumn<double>(
                name: "TravelTime",
                table: "Flights",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ArrivalLocationDestinationId",
                table: "Flights",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Destinations",
                table: "Destinations",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ArrivalLocationDestinationId",
                table: "Flights",
                column: "ArrivalLocationDestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarReservations_Destinations_PickUpLocationDestinationId",
                table: "CarReservations",
                column: "PickUpLocationDestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarReservations_Destinations_ReturnLocationDestinationId",
                table: "CarReservations",
                column: "ReturnLocationDestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_AvioCompanies_AvioCompanyId",
                table: "Destinations",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Flights_FlightId",
                table: "Destinations",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Destinations_ArrivalLocationDestinationId",
                table: "Flights",
                column: "ArrivalLocationDestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Destinations_DepartureLocationDestinationId",
                table: "Flights",
                column: "DepartureLocationDestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Destinations_LocationDestinationId",
                table: "Offices",
                column: "LocationDestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReservations_Destinations_PickUpLocationDestinationId",
                table: "CarReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_CarReservations_Destinations_ReturnLocationDestinationId",
                table: "CarReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_AvioCompanies_AvioCompanyId",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Flights_FlightId",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Destinations_ArrivalLocationDestinationId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Destinations_DepartureLocationDestinationId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Destinations_LocationDestinationId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Flights_ArrivalLocationDestinationId",
                table: "Flights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Destinations",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "ArrivalLocationDestinationId",
                table: "Flights");

            migrationBuilder.RenameTable(
                name: "Destinations",
                newName: "Destination");

            migrationBuilder.RenameIndex(
                name: "IX_Destinations_FlightId",
                table: "Destination",
                newName: "IX_Destination_FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_Destinations_AvioCompanyId",
                table: "Destination",
                newName: "IX_Destination_AvioCompanyId");

            migrationBuilder.AlterColumn<long>(
                name: "TravelTime",
                table: "Flights",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Destination",
                table: "Destination",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarReservations_Destination_PickUpLocationDestinationId",
                table: "CarReservations",
                column: "PickUpLocationDestinationId",
                principalTable: "Destination",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarReservations_Destination_ReturnLocationDestinationId",
                table: "CarReservations",
                column: "ReturnLocationDestinationId",
                principalTable: "Destination",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Destination_AvioCompanies_AvioCompanyId",
                table: "Destination",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Destination_Flights_FlightId",
                table: "Destination",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Destination_DepartureLocationDestinationId",
                table: "Flights",
                column: "DepartureLocationDestinationId",
                principalTable: "Destination",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Destination_LocationDestinationId",
                table: "Offices",
                column: "LocationDestinationId",
                principalTable: "Destination",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
