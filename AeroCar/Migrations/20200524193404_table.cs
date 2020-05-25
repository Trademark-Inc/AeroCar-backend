using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aeroplane_AvioCompanies_AvioCompanyId",
                table: "Aeroplane");

            migrationBuilder.DropForeignKey(
                name: "FK_Destination_Flight_FlightId",
                table: "Destination");

            migrationBuilder.DropForeignKey(
                name: "FK_FastReservationFlightTicket_AvioCompanies_AvioCompanyId",
                table: "FastReservationFlightTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_AvioCompanies_AvioCompanyId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Destination_DepartureLocationDestinationId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItem_AvioCompanies_AvioCompanyId",
                table: "PriceListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Aeroplane_AeroplaneId",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceListItem",
                table: "PriceListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flight",
                table: "Flight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FastReservationFlightTicket",
                table: "FastReservationFlightTicket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aeroplane",
                table: "Aeroplane");

            migrationBuilder.RenameTable(
                name: "PriceListItem",
                newName: "PriceListItems");

            migrationBuilder.RenameTable(
                name: "Flight",
                newName: "Flights");

            migrationBuilder.RenameTable(
                name: "FastReservationFlightTicket",
                newName: "FastReservationFlightTickets");

            migrationBuilder.RenameTable(
                name: "Aeroplane",
                newName: "Aeroplanes");

            migrationBuilder.RenameIndex(
                name: "IX_PriceListItem_AvioCompanyId",
                table: "PriceListItems",
                newName: "IX_PriceListItems_AvioCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_DepartureLocationDestinationId",
                table: "Flights",
                newName: "IX_Flights_DepartureLocationDestinationId");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_AvioCompanyId",
                table: "Flights",
                newName: "IX_Flights_AvioCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_FastReservationFlightTicket_AvioCompanyId",
                table: "FastReservationFlightTickets",
                newName: "IX_FastReservationFlightTickets_AvioCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Aeroplane_AvioCompanyId",
                table: "Aeroplanes",
                newName: "IX_Aeroplanes_AvioCompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceListItems",
                table: "PriceListItems",
                column: "PriceListIdemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flights",
                table: "Flights",
                column: "FlightId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FastReservationFlightTickets",
                table: "FastReservationFlightTickets",
                column: "FRFTId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aeroplanes",
                table: "Aeroplanes",
                column: "AeroplaneId");

            migrationBuilder.CreateTable(
                name: "AvioAdmins",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AvioCompanyId = table.Column<long>(nullable: false),
                    SetUpPassword = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvioAdmins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AvioCompanyProfiles",
                columns: table => new
                {
                    AvioCompanyProfileId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvioCompanyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PromoDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvioCompanyProfiles", x => x.AvioCompanyProfileId);
                });

            migrationBuilder.CreateTable(
                name: "AvioCompanyRatings",
                columns: table => new
                {
                    RatingId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvioCompanyId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvioCompanyRatings", x => x.RatingId);
                });

            migrationBuilder.CreateTable(
                name: "CarAdmins",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CarCompanyId = table.Column<long>(nullable: false),
                    SetUpPassword = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAdmins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarCompanies",
                columns: table => new
                {
                    CarCompanyId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CarCompanyProfileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompanies", x => x.CarCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "CarCompanyProfiles",
                columns: table => new
                {
                    CarCompanyProfileId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CarCompanyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PromoDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompanyProfiles", x => x.CarCompanyProfileId);
                });

            migrationBuilder.CreateTable(
                name: "CarCompanyRatings",
                columns: table => new
                {
                    RatingId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CarCompanyId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompanyRatings", x => x.RatingId);
                });

            migrationBuilder.CreateTable(
                name: "CarRatings",
                columns: table => new
                {
                    RatingId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VehicleId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRatings", x => x.RatingId);
                });

            migrationBuilder.CreateTable(
                name: "FlightRatings",
                columns: table => new
                {
                    RatingId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FlightId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightRatings", x => x.RatingId);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    InvitationId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FromUserId = table.Column<string>(nullable: true),
                    ToUserId = table.Column<string>(nullable: true),
                    SentDate = table.Column<DateTime>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.InvitationId);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserProfileId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Bonus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserProfileId);
                });

            migrationBuilder.CreateTable(
                name: "FastReservationCarTickets",
                columns: table => new
                {
                    FRCTId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VehicleId = table.Column<long>(nullable: false),
                    Percent = table.Column<double>(nullable: false),
                    CarCompanyId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FastReservationCarTickets", x => x.FRCTId);
                    table.ForeignKey(
                        name: "FK_FastReservationCarTickets_CarCompanies_CarCompanyId",
                        column: x => x.CarCompanyId,
                        principalTable: "CarCompanies",
                        principalColumn: "CarCompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    OfficeId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CarCompanyId = table.Column<long>(nullable: false),
                    LocationDestinationId = table.Column<long>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.OfficeId);
                    table.ForeignKey(
                        name: "FK_Offices_CarCompanies_CarCompanyId",
                        column: x => x.CarCompanyId,
                        principalTable: "CarCompanies",
                        principalColumn: "CarCompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offices_Destination_LocationDestinationId",
                        column: x => x.LocationDestinationId,
                        principalTable: "Destination",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvioCompanyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CarType = table.Column<int>(nullable: false),
                    Passangers = table.Column<int>(nullable: false),
                    Baggage = table.Column<int>(nullable: false),
                    Doors = table.Column<int>(nullable: false),
                    Fuel = table.Column<string>(nullable: true),
                    Transmission = table.Column<string>(nullable: true),
                    Additional = table.Column<string>(nullable: true),
                    CostPerDay = table.Column<double>(nullable: false),
                    CarCompanyId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_CarCompanies_CarCompanyId",
                        column: x => x.CarCompanyId,
                        principalTable: "CarCompanies",
                        principalColumn: "CarCompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarReservations",
                columns: table => new
                {
                    CarReservationId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VehicleId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    PickUpDate = table.Column<DateTime>(nullable: false),
                    PickUpLocationDestinationId = table.Column<long>(nullable: true),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    ReturnLocationDestinationId = table.Column<long>(nullable: true),
                    Canceled = table.Column<bool>(nullable: false),
                    Finished = table.Column<bool>(nullable: false),
                    UserProfileId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarReservations", x => x.CarReservationId);
                    table.ForeignKey(
                        name: "FK_CarReservations_Destination_PickUpLocationDestinationId",
                        column: x => x.PickUpLocationDestinationId,
                        principalTable: "Destination",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarReservations_Destination_ReturnLocationDestinationId",
                        column: x => x.ReturnLocationDestinationId,
                        principalTable: "Destination",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarReservations_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightReservations",
                columns: table => new
                {
                    FlightReservationId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FlightId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    SeatNumber = table.Column<int>(nullable: false),
                    Passport = table.Column<string>(nullable: true),
                    Invitation = table.Column<bool>(nullable: false),
                    Canceled = table.Column<bool>(nullable: false),
                    Finished = table.Column<bool>(nullable: false),
                    UserProfileId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightReservations", x => x.FlightReservationId);
                    table.ForeignKey(
                        name: "FK_FlightReservations_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    RelationshipId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FriendId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    UserProfileId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.RelationshipId);
                    table.ForeignKey(
                        name: "FK_Friend_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_PickUpLocationDestinationId",
                table: "CarReservations",
                column: "PickUpLocationDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_ReturnLocationDestinationId",
                table: "CarReservations",
                column: "ReturnLocationDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_UserProfileId",
                table: "CarReservations",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_FastReservationCarTickets_CarCompanyId",
                table: "FastReservationCarTickets",
                column: "CarCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightReservations_UserProfileId",
                table: "FlightReservations",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_UserProfileId",
                table: "Friend",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_CarCompanyId",
                table: "Offices",
                column: "CarCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_LocationDestinationId",
                table: "Offices",
                column: "LocationDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CarCompanyId",
                table: "Vehicles",
                column: "CarCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aeroplanes_AvioCompanies_AvioCompanyId",
                table: "Aeroplanes",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Destination_Flights_FlightId",
                table: "Destination",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FastReservationFlightTickets_AvioCompanies_AvioCompanyId",
                table: "FastReservationFlightTickets",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_AvioCompanies_AvioCompanyId",
                table: "Flights",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Destination_DepartureLocationDestinationId",
                table: "Flights",
                column: "DepartureLocationDestinationId",
                principalTable: "Destination",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItems_AvioCompanies_AvioCompanyId",
                table: "PriceListItems",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Aeroplanes_AeroplaneId",
                table: "Seats",
                column: "AeroplaneId",
                principalTable: "Aeroplanes",
                principalColumn: "AeroplaneId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aeroplanes_AvioCompanies_AvioCompanyId",
                table: "Aeroplanes");

            migrationBuilder.DropForeignKey(
                name: "FK_Destination_Flights_FlightId",
                table: "Destination");

            migrationBuilder.DropForeignKey(
                name: "FK_FastReservationFlightTickets_AvioCompanies_AvioCompanyId",
                table: "FastReservationFlightTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_AvioCompanies_AvioCompanyId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Destination_DepartureLocationDestinationId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItems_AvioCompanies_AvioCompanyId",
                table: "PriceListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Aeroplanes_AeroplaneId",
                table: "Seats");

            migrationBuilder.DropTable(
                name: "AvioAdmins");

            migrationBuilder.DropTable(
                name: "AvioCompanyProfiles");

            migrationBuilder.DropTable(
                name: "AvioCompanyRatings");

            migrationBuilder.DropTable(
                name: "CarAdmins");

            migrationBuilder.DropTable(
                name: "CarCompanyProfiles");

            migrationBuilder.DropTable(
                name: "CarCompanyRatings");

            migrationBuilder.DropTable(
                name: "CarRatings");

            migrationBuilder.DropTable(
                name: "CarReservations");

            migrationBuilder.DropTable(
                name: "FastReservationCarTickets");

            migrationBuilder.DropTable(
                name: "FlightRatings");

            migrationBuilder.DropTable(
                name: "FlightReservations");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "CarCompanies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceListItems",
                table: "PriceListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flights",
                table: "Flights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FastReservationFlightTickets",
                table: "FastReservationFlightTickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aeroplanes",
                table: "Aeroplanes");

            migrationBuilder.RenameTable(
                name: "PriceListItems",
                newName: "PriceListItem");

            migrationBuilder.RenameTable(
                name: "Flights",
                newName: "Flight");

            migrationBuilder.RenameTable(
                name: "FastReservationFlightTickets",
                newName: "FastReservationFlightTicket");

            migrationBuilder.RenameTable(
                name: "Aeroplanes",
                newName: "Aeroplane");

            migrationBuilder.RenameIndex(
                name: "IX_PriceListItems_AvioCompanyId",
                table: "PriceListItem",
                newName: "IX_PriceListItem_AvioCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_DepartureLocationDestinationId",
                table: "Flight",
                newName: "IX_Flight_DepartureLocationDestinationId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_AvioCompanyId",
                table: "Flight",
                newName: "IX_Flight_AvioCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_FastReservationFlightTickets_AvioCompanyId",
                table: "FastReservationFlightTicket",
                newName: "IX_FastReservationFlightTicket_AvioCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Aeroplanes_AvioCompanyId",
                table: "Aeroplane",
                newName: "IX_Aeroplane_AvioCompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceListItem",
                table: "PriceListItem",
                column: "PriceListIdemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flight",
                table: "Flight",
                column: "FlightId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FastReservationFlightTicket",
                table: "FastReservationFlightTicket",
                column: "FRFTId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aeroplane",
                table: "Aeroplane",
                column: "AeroplaneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aeroplane_AvioCompanies_AvioCompanyId",
                table: "Aeroplane",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Destination_Flight_FlightId",
                table: "Destination",
                column: "FlightId",
                principalTable: "Flight",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FastReservationFlightTicket_AvioCompanies_AvioCompanyId",
                table: "FastReservationFlightTicket",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_AvioCompanies_AvioCompanyId",
                table: "Flight",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Destination_DepartureLocationDestinationId",
                table: "Flight",
                column: "DepartureLocationDestinationId",
                principalTable: "Destination",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItem_AvioCompanies_AvioCompanyId",
                table: "PriceListItem",
                column: "AvioCompanyId",
                principalTable: "AvioCompanies",
                principalColumn: "AvioCompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Aeroplane_AeroplaneId",
                table: "Seats",
                column: "AeroplaneId",
                principalTable: "Aeroplane",
                principalColumn: "AeroplaneId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
