﻿// <auto-generated />
using System;
using AeroCar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AeroCar.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200612223331_avio rating update")]
    partial class avioratingupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AeroCar.Models.Admin.AvioAdmin", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("AvioCompanyId")
                        .HasColumnType("bigint");

                    b.Property<bool>("SetUpPassword")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId");

                    b.ToTable("AvioAdmins");
                });

            modelBuilder.Entity("AeroCar.Models.Admin.CarAdmin", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CarCompanyId")
                        .HasColumnType("bigint");

                    b.Property<bool>("SetUpPassword")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId");

                    b.ToTable("CarAdmins");
                });

            modelBuilder.Entity("AeroCar.Models.Avio.Aeroplane", b =>
                {
                    b.Property<long>("AeroplaneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AvioCompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("AeroplaneId");

                    b.HasIndex("AvioCompanyId");

                    b.ToTable("Aeroplanes");
                });

            modelBuilder.Entity("AeroCar.Models.Avio.AvioCompany", b =>
                {
                    b.Property<long>("AvioCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AvioCompanyProfileId")
                        .HasColumnType("bigint");

                    b.Property<string>("BaggageDescription")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("AvioCompanyId");

                    b.ToTable("AvioCompanies");
                });

            modelBuilder.Entity("AeroCar.Models.Avio.AvioCompanyProfile", b =>
                {
                    b.Property<long>("AvioCompanyProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PromoDescription")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("AvioCompanyProfileId");

                    b.ToTable("AvioCompanyProfiles");
                });

            modelBuilder.Entity("AeroCar.Models.Avio.DeletedSeats", b =>
                {
                    b.Property<long>("DeletedSeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("SeatId")
                        .HasColumnType("bigint");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.Property<long?>("SeatsId")
                        .HasColumnType("bigint");

                    b.HasKey("DeletedSeatId");

                    b.HasIndex("SeatsId");

                    b.ToTable("DeletedSeats");
                });

            modelBuilder.Entity("AeroCar.Models.Avio.Flight", b =>
                {
                    b.Property<long>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AeroplaneId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Arrival")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ArrivalLocationDestinationId")
                        .HasColumnType("bigint");

                    b.Property<long>("AvioCompanyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Departure")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("DepartureLocationDestinationId")
                        .HasColumnType("bigint");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<double>("TravelDistance")
                        .HasColumnType("double");

                    b.Property<double>("TravelTime")
                        .HasColumnType("double");

                    b.HasKey("FlightId");

                    b.HasIndex("ArrivalLocationDestinationId");

                    b.HasIndex("AvioCompanyId");

                    b.HasIndex("DepartureLocationDestinationId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("AeroCar.Models.Avio.PriceListItem", b =>
                {
                    b.Property<long>("PriceListIdemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("AvioCompanyId")
                        .HasColumnType("bigint");

                    b.Property<long?>("FlightReservationId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.HasKey("PriceListIdemId");

                    b.HasIndex("AvioCompanyId");

                    b.HasIndex("FlightReservationId");

                    b.ToTable("PriceListItems");
                });

            modelBuilder.Entity("AeroCar.Models.Avio.Seats", b =>
                {
                    b.Property<long>("SeatsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AeroplaneId")
                        .HasColumnType("bigint");

                    b.Property<int>("InOneRow")
                        .HasColumnType("int");

                    b.Property<int>("SeatCount")
                        .HasColumnType("int");

                    b.HasKey("SeatsId");

                    b.HasIndex("AeroplaneId")
                        .IsUnique();

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("AeroCar.Models.Car.CarCompany", b =>
                {
                    b.Property<long>("CarCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CarCompanyProfileId")
                        .HasColumnType("bigint");

                    b.HasKey("CarCompanyId");

                    b.ToTable("CarCompanies");
                });

            modelBuilder.Entity("AeroCar.Models.Car.CarCompanyProfile", b =>
                {
                    b.Property<long>("CarCompanyProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PromoDescription")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("CarCompanyProfileId");

                    b.ToTable("CarCompanyProfiles");
                });

            modelBuilder.Entity("AeroCar.Models.Car.Office", b =>
                {
                    b.Property<long>("OfficeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("CarCompanyId")
                        .HasColumnType("bigint");

                    b.Property<long?>("LocationDestinationId")
                        .HasColumnType("bigint");

                    b.HasKey("OfficeId");

                    b.HasIndex("CarCompanyId");

                    b.HasIndex("LocationDestinationId");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("AeroCar.Models.Car.Vehicle", b =>
                {
                    b.Property<long>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Additional")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Baggage")
                        .HasColumnType("int");

                    b.Property<long>("CarCompanyId")
                        .HasColumnType("bigint");

                    b.Property<int>("CarType")
                        .HasColumnType("int");

                    b.Property<double>("CostPerDay")
                        .HasColumnType("double");

                    b.Property<int>("Doors")
                        .HasColumnType("int");

                    b.Property<string>("Fuel")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Passangers")
                        .HasColumnType("int");

                    b.Property<string>("Transmission")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("VehicleId");

                    b.HasIndex("CarCompanyId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("AeroCar.Models.Destination", b =>
                {
                    b.Property<long>("DestinationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("AvioCompanyId")
                        .HasColumnType("bigint");

                    b.Property<long?>("FlightId")
                        .HasColumnType("bigint");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<double>("Longitude")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("DestinationId");

                    b.HasIndex("AvioCompanyId");

                    b.HasIndex("FlightId");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("AeroCar.Models.Rating.AvioCompanyRating", b =>
                {
                    b.Property<long>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AvioCompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("FlightReservationId")
                        .HasColumnType("bigint");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("RatingId");

                    b.ToTable("AvioCompanyRatings");
                });

            modelBuilder.Entity("AeroCar.Models.Rating.CarCompanyRating", b =>
                {
                    b.Property<long>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CarCompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("RatingId");

                    b.ToTable("CarCompanyRatings");
                });

            modelBuilder.Entity("AeroCar.Models.Rating.FlightRating", b =>
                {
                    b.Property<long>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("FlightId")
                        .HasColumnType("bigint");

                    b.Property<long>("FlightReservationId")
                        .HasColumnType("bigint");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("RatingId");

                    b.ToTable("FlightRatings");
                });

            modelBuilder.Entity("AeroCar.Models.Rating.VehicleRating", b =>
                {
                    b.Property<long>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("VehicleId")
                        .HasColumnType("bigint");

                    b.HasKey("RatingId");

                    b.ToTable("CarRatings");
                });

            modelBuilder.Entity("AeroCar.Models.Registration.Friend", b =>
                {
                    b.Property<long>("RelationshipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("FriendId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RegularUserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("RelationshipId");

                    b.HasIndex("RegularUserId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("AeroCar.Models.Registration.Invitation", b =>
                {
                    b.Property<long>("InvitationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Accepted")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("FlightId")
                        .HasColumnType("bigint");

                    b.Property<string>("FromUserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ToUserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("InvitationId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("AeroCar.Models.Registration.UserProfile", b =>
                {
                    b.Property<long>("UserProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserProfileId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("AeroCar.Models.Reservation.CarReservation", b =>
                {
                    b.Property<long>("CarReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Canceled")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Finished")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("PickUpDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("PickUpLocationDestinationId")
                        .HasColumnType("bigint");

                    b.Property<string>("RegularUserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ReturnLocationDestinationId")
                        .HasColumnType("bigint");

                    b.Property<long>("VehicleId")
                        .HasColumnType("bigint");

                    b.HasKey("CarReservationId");

                    b.HasIndex("PickUpLocationDestinationId");

                    b.HasIndex("RegularUserId");

                    b.HasIndex("ReturnLocationDestinationId");

                    b.ToTable("CarReservations");
                });

            modelBuilder.Entity("AeroCar.Models.Reservation.FastReservationCarTicket", b =>
                {
                    b.Property<long>("FRCTId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CarCompanyId")
                        .HasColumnType("bigint");

                    b.Property<double>("Percent")
                        .HasColumnType("double");

                    b.Property<long>("VehicleId")
                        .HasColumnType("bigint");

                    b.HasKey("FRCTId");

                    b.HasIndex("CarCompanyId");

                    b.ToTable("FastReservationCarTickets");
                });

            modelBuilder.Entity("AeroCar.Models.Reservation.FastReservationFlightTicket", b =>
                {
                    b.Property<long>("FRFTId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("AvioCompanyId")
                        .HasColumnType("bigint");

                    b.Property<long>("FlightId")
                        .HasColumnType("bigint");

                    b.Property<double>("Percent")
                        .HasColumnType("double");

                    b.HasKey("FRFTId");

                    b.HasIndex("AvioCompanyId");

                    b.ToTable("FastReservationFlightTickets");
                });

            modelBuilder.Entity("AeroCar.Models.Reservation.FlightReservation", b =>
                {
                    b.Property<long>("FlightReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Canceled")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Finished")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("FlightId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Invitation")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Passport")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RegularUserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("FlightReservationId");

                    b.HasIndex("RegularUserId");

                    b.ToTable("FlightReservations");
                });

            modelBuilder.Entity("AeroCar.Models.Users.RegularUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Bonus")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AeroCar.Models.Avio.Aeroplane", b =>
                {
                    b.HasOne("AeroCar.Models.Avio.AvioCompany", null)
                        .WithMany("Aeroplanes")
                        .HasForeignKey("AvioCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AeroCar.Models.Avio.DeletedSeats", b =>
                {
                    b.HasOne("AeroCar.Models.Avio.Seats", null)
                        .WithMany("DeletedSeats")
                        .HasForeignKey("SeatsId");
                });

            modelBuilder.Entity("AeroCar.Models.Avio.Flight", b =>
                {
                    b.HasOne("AeroCar.Models.Destination", "ArrivalLocation")
                        .WithMany()
                        .HasForeignKey("ArrivalLocationDestinationId");

                    b.HasOne("AeroCar.Models.Avio.AvioCompany", null)
                        .WithMany("Flights")
                        .HasForeignKey("AvioCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AeroCar.Models.Destination", "DepartureLocation")
                        .WithMany()
                        .HasForeignKey("DepartureLocationDestinationId");
                });

            modelBuilder.Entity("AeroCar.Models.Avio.PriceListItem", b =>
                {
                    b.HasOne("AeroCar.Models.Avio.AvioCompany", null)
                        .WithMany("PriceList")
                        .HasForeignKey("AvioCompanyId");

                    b.HasOne("AeroCar.Models.Reservation.FlightReservation", null)
                        .WithMany("PriceListItems")
                        .HasForeignKey("FlightReservationId");
                });

            modelBuilder.Entity("AeroCar.Models.Avio.Seats", b =>
                {
                    b.HasOne("AeroCar.Models.Avio.Aeroplane", null)
                        .WithOne("Seats")
                        .HasForeignKey("AeroCar.Models.Avio.Seats", "AeroplaneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AeroCar.Models.Car.Office", b =>
                {
                    b.HasOne("AeroCar.Models.Car.CarCompany", null)
                        .WithMany("Offices")
                        .HasForeignKey("CarCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AeroCar.Models.Destination", "Location")
                        .WithMany()
                        .HasForeignKey("LocationDestinationId");
                });

            modelBuilder.Entity("AeroCar.Models.Car.Vehicle", b =>
                {
                    b.HasOne("AeroCar.Models.Car.CarCompany", null)
                        .WithMany("Vehicles")
                        .HasForeignKey("CarCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AeroCar.Models.Destination", b =>
                {
                    b.HasOne("AeroCar.Models.Avio.AvioCompany", null)
                        .WithMany("Destinations")
                        .HasForeignKey("AvioCompanyId");

                    b.HasOne("AeroCar.Models.Avio.Flight", null)
                        .WithMany("Transit")
                        .HasForeignKey("FlightId");
                });

            modelBuilder.Entity("AeroCar.Models.Registration.Friend", b =>
                {
                    b.HasOne("AeroCar.Models.Users.RegularUser", null)
                        .WithMany("Friends")
                        .HasForeignKey("RegularUserId");
                });

            modelBuilder.Entity("AeroCar.Models.Reservation.CarReservation", b =>
                {
                    b.HasOne("AeroCar.Models.Destination", "PickUpLocation")
                        .WithMany()
                        .HasForeignKey("PickUpLocationDestinationId");

                    b.HasOne("AeroCar.Models.Users.RegularUser", null)
                        .WithMany("ReservedCars")
                        .HasForeignKey("RegularUserId");

                    b.HasOne("AeroCar.Models.Destination", "ReturnLocation")
                        .WithMany()
                        .HasForeignKey("ReturnLocationDestinationId");
                });

            modelBuilder.Entity("AeroCar.Models.Reservation.FastReservationCarTicket", b =>
                {
                    b.HasOne("AeroCar.Models.Car.CarCompany", null)
                        .WithMany("FastReservationCarTickets")
                        .HasForeignKey("CarCompanyId");
                });

            modelBuilder.Entity("AeroCar.Models.Reservation.FastReservationFlightTicket", b =>
                {
                    b.HasOne("AeroCar.Models.Avio.AvioCompany", null)
                        .WithMany("FastReservationTickets")
                        .HasForeignKey("AvioCompanyId");
                });

            modelBuilder.Entity("AeroCar.Models.Reservation.FlightReservation", b =>
                {
                    b.HasOne("AeroCar.Models.Users.RegularUser", null)
                        .WithMany("ReservedFlights")
                        .HasForeignKey("RegularUserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AeroCar.Models.Users.RegularUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AeroCar.Models.Users.RegularUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AeroCar.Models.Users.RegularUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AeroCar.Models.Users.RegularUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
