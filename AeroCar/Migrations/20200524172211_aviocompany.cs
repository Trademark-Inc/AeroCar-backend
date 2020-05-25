using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class aviocompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvioCompanies",
                columns: table => new
                {
                    AvioCompanyId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvioCompanyProfileId = table.Column<long>(nullable: false),
                    BaggageDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvioCompanies", x => x.AvioCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Aeroplane",
                columns: table => new
                {
                    AeroplaneId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvioCompanyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeroplane", x => x.AeroplaneId);
                    table.ForeignKey(
                        name: "FK_Aeroplane_AvioCompanies_AvioCompanyId",
                        column: x => x.AvioCompanyId,
                        principalTable: "AvioCompanies",
                        principalColumn: "AvioCompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FastReservationFlightTicket",
                columns: table => new
                {
                    FRFTId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FlightId = table.Column<long>(nullable: false),
                    Percent = table.Column<double>(nullable: false),
                    AvioCompanyId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FastReservationFlightTicket", x => x.FRFTId);
                    table.ForeignKey(
                        name: "FK_FastReservationFlightTicket_AvioCompanies_AvioCompanyId",
                        column: x => x.AvioCompanyId,
                        principalTable: "AvioCompanies",
                        principalColumn: "AvioCompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceListItem",
                columns: table => new
                {
                    PriceListIdemId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvioCompanyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListItem", x => x.PriceListIdemId);
                    table.ForeignKey(
                        name: "FK_PriceListItem_AvioCompanies_AvioCompanyId",
                        column: x => x.AvioCompanyId,
                        principalTable: "AvioCompanies",
                        principalColumn: "AvioCompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatsId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AeroplaneId = table.Column<long>(nullable: false),
                    SeatCount = table.Column<int>(nullable: false),
                    InOneRow = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatsId);
                    table.ForeignKey(
                        name: "FK_Seats_Aeroplane_AeroplaneId",
                        column: x => x.AeroplaneId,
                        principalTable: "Aeroplane",
                        principalColumn: "AeroplaneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeletedSeats",
                columns: table => new
                {
                    DeletedSeatId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SeatId = table.Column<long>(nullable: false),
                    SeatNumber = table.Column<int>(nullable: false),
                    SeatsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedSeats", x => x.DeletedSeatId);
                    table.ForeignKey(
                        name: "FK_DeletedSeats_Seats_SeatsId",
                        column: x => x.SeatsId,
                        principalTable: "Seats",
                        principalColumn: "SeatsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Destination",
                columns: table => new
                {
                    DestinationId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    AvioCompanyId = table.Column<long>(nullable: true),
                    FlightId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destination", x => x.DestinationId);
                    table.ForeignKey(
                        name: "FK_Destination_AvioCompanies_AvioCompanyId",
                        column: x => x.AvioCompanyId,
                        principalTable: "AvioCompanies",
                        principalColumn: "AvioCompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    FlightId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvioCompanyId = table.Column<long>(nullable: false),
                    AeroplaneId = table.Column<long>(nullable: false),
                    Departure = table.Column<DateTime>(nullable: false),
                    DepartureLocationDestinationId = table.Column<long>(nullable: true),
                    Arrival = table.Column<DateTime>(nullable: false),
                    TravelTime = table.Column<long>(nullable: false),
                    TravelDistance = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flight_AvioCompanies_AvioCompanyId",
                        column: x => x.AvioCompanyId,
                        principalTable: "AvioCompanies",
                        principalColumn: "AvioCompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flight_Destination_DepartureLocationDestinationId",
                        column: x => x.DepartureLocationDestinationId,
                        principalTable: "Destination",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aeroplane_AvioCompanyId",
                table: "Aeroplane",
                column: "AvioCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedSeats_SeatsId",
                table: "DeletedSeats",
                column: "SeatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Destination_AvioCompanyId",
                table: "Destination",
                column: "AvioCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Destination_FlightId",
                table: "Destination",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_FastReservationFlightTicket_AvioCompanyId",
                table: "FastReservationFlightTicket",
                column: "AvioCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AvioCompanyId",
                table: "Flight",
                column: "AvioCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_DepartureLocationDestinationId",
                table: "Flight",
                column: "DepartureLocationDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItem_AvioCompanyId",
                table: "PriceListItem",
                column: "AvioCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_AeroplaneId",
                table: "Seats",
                column: "AeroplaneId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Destination_Flight_FlightId",
                table: "Destination",
                column: "FlightId",
                principalTable: "Flight",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destination_AvioCompanies_AvioCompanyId",
                table: "Destination");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_AvioCompanies_AvioCompanyId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Destination_Flight_FlightId",
                table: "Destination");

            migrationBuilder.DropTable(
                name: "DeletedSeats");

            migrationBuilder.DropTable(
                name: "FastReservationFlightTicket");

            migrationBuilder.DropTable(
                name: "PriceListItem");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Aeroplane");

            migrationBuilder.DropTable(
                name: "AvioCompanies");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "Destination");
        }
    }
}
