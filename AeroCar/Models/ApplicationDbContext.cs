using AeroCar.Models.Admin;
using AeroCar.Models.Avio;
using AeroCar.Models.Car;
using AeroCar.Models.Rating;
using AeroCar.Models.Registration;
using AeroCar.Models.Reservation;
using AeroCar.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models
{
    public class ApplicationDbContext : IdentityDbContext<RegularUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<AvioAdmin> AvioAdmins { get; set; }

        public DbSet<CarAdmin> CarAdmins { get; set; }

        public DbSet<Aeroplane> Aeroplanes { get; set; }

        public DbSet<AvioCompany> AvioCompanies { get; set; }

        public DbSet<AvioCompanyProfile> AvioCompanyProfiles { get; set; }

        public DbSet<DeletedSeats> DeletedSeats { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<PriceListItem> PriceListItems { get; set; }

        public DbSet<Seats> Seats { get; set; }

        public DbSet<CarCompany> CarCompanies { get; set; }

        public DbSet<CarCompanyProfile> CarCompanyProfiles { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<AvioCompanyRating> AvioCompanyRatings { get; set; }

        public DbSet<CarCompanyRating> CarCompanyRatings { get; set; }

        public DbSet<FlightRating> FlightRatings { get; set; }

        public DbSet<VehicleRating> CarRatings { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<CarReservation> CarReservations { get; set; }

        public DbSet<FastReservationCarTicket> FastReservationCarTickets { get; set; }

        public DbSet<FastReservationFlightTicket> FastReservationFlightTickets { get; set; }

        public DbSet<FlightReservation> FlightReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
