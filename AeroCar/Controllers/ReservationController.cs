using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeroCar.Models;
using AeroCar.Models.Avio;
using AeroCar.Models.DTO.Registration;
using AeroCar.Models.DTO.Reservation;
using AeroCar.Models.Reservation;
using AeroCar.Services;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroCar.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        public ReservationController(UserService userService, FlightService flightService, AvioService avioService, 
            ReservationService reservationService, AeroplaneService aeroplaneService) 
        {
            UserService = userService;
            FlightService = flightService;
            AvioService = avioService;
            ReservationService = reservationService;
            AeroplaneService = aeroplaneService;
        }

        private readonly UserService UserService;
        private readonly FlightService FlightService;
        private readonly AvioService AvioService;
        private readonly ReservationService ReservationService;
        private readonly AeroplaneService AeroplaneService;

        // POST api/reservation/car
        [HttpPost]
        [Route("car")]
        public async Task<IActionResult> CarReservation([FromBody]CarReservationDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetCurrentUser();

                if (user != null)
                {
                    var pickUpTime = DateTime.ParseExact(model.ReservationDetails.PickUpTime, "H:mm", null, System.Globalization.DateTimeStyles.None);
                    var returnTime = DateTime.ParseExact(model.ReservationDetails.ReturnTime, "H:mm", null, System.Globalization.DateTimeStyles.None);
                    var reservation = new CarReservation()
                    {
                        PickUpDate = model.ReservationDetails.PickUpDate.Add(pickUpTime.TimeOfDay),
                        PickUpLocation = new Destination()
                        {
                            Name = model.ReservationDetails.PickUpLocation
                        },
                        ReturnDate = model.ReservationDetails.ReturnDate.Add(returnTime.TimeOfDay),
                        ReturnLocation = new Destination()
                        {
                            Name = model.ReservationDetails.ReturnLocation,
                        },
                        VehicleId = model.VehicleId,
                        Finished = true
                    };

                    user.ReservedCars.Add(reservation);
                    await UserService.UpdateUser(user);

                    return Ok(200);
                }
            }

            return BadRequest(ModelState);
        }

        // GET api/reservation/flight/remove/{id}
        [HttpGet]
        [Route("car/remove/{id}")]
        public async Task<IActionResult> RemoveCarReservation(long id)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetCurrentUser();

                if (user != null)
                {
                    var reservation = user.ReservedCars.SingleOrDefault(rc => rc.CarReservationId == id);

                    if (reservation != null)
                    {
                        await ReservationService.RemoveCarReservation(reservation);

                        return Ok(200);
                    }
                }
            }

            return BadRequest(ModelState);
        }

        // POST api/reservation/flight/step/1
        [HttpPost]
        [Route("flight/step/1")]
        public async Task<IActionResult> FlightReservationStepOne([FromBody]FlightStepOne model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetCurrentUser();

                if (user != null)
                {
                    var company = await AvioService.GetCompany(model.Flight.AvioCompanyId);

                    List<PriceListItem> priceListItems = new List<PriceListItem>();
                    foreach (PriceListItem p in company.PriceList)
                    {
                        var selected = model.SelectedPriceListItems.SingleOrDefault(s => s.Id == p.PriceListIdemId && s.Selected);
                        if (selected != null)
                            priceListItems.Add(new PriceListItem() 
                            { 
                                Name = p.Name,
                                Price = p.Price,
                            });
                    }

                    var reservation = new FlightReservation()
                    {
                        FlightId = model.Flight.FlightId,
                        Canceled = false,
                        Finished = false,
                        Invitation = false,
                        UserId = user.Id,
                        PriceListItems = priceListItems,
                        SeatNumber = -1,
                    };

                    user.ReservedFlights.Add(reservation);
                    await UserService.UpdateUser(user);

                    return Ok(new { reservation });
                }
            }

            return BadRequest("Not enough data provided.");
        }

        // POST api/reservation/flight/step/2
        [HttpPost]
        [Route("flight/step/2")]
        public async Task<IActionResult> FlightReservationStepTwo([FromBody]FlightStepTwo model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetCurrentUser();

                if (user != null)
                {
                    var reservation = user.ReservedFlights.SingleOrDefault(rf => rf.FlightReservationId == model.ReservationId);

                    if (reservation != null)
                    {
                        bool finished = true;
                        if (!reservation.Finished)
                        {
                            finished = false;
                        }

                        reservation.SeatNumber = model.Seat;
                        await ReservationService.UpdateFlightReservation(reservation);

                        if (!finished && reservation.Finished)
                        {
                            var flight = await FlightService.GetFlight(reservation.FlightId);

                            var sCoord = new GeoCoordinate(flight.DepartureLocation.Latitude, flight.DepartureLocation.Longitude);
                            var eCoord = new GeoCoordinate(flight.ArrivalLocation.Latitude, flight.ArrivalLocation.Longitude);

                            user.Bonus += (int)(sCoord.GetDistanceTo(eCoord) / 100000);
                            await UserService.UpdateUser(user);
                        }

                        return Ok(new { reservation });
                    }
                }
            }

            return BadRequest("Not enough data provided.");
        }

        // POST api/reservation/flight/step/3
        [HttpPost]
        [Route("flight/step/3")]
        public async Task<IActionResult> FlightReservationStepThree([FromBody]FlightStepThree model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetCurrentUser();

                if (user != null)
                {
                    var reservation = user.ReservedFlights.SingleOrDefault(rf => rf.FlightReservationId == model.ReservationId);

                    if (reservation != null)
                    {
                        reservation.Name = model.Name;
                        reservation.Surname = model.Surname;
                        reservation.Passport = model.Passport;

                        bool finished = true;
                        if (!reservation.Finished)
                        {
                            finished = false;
                        }

                        await ReservationService.UpdateFlightReservation(reservation);

                        if (!finished && reservation.Finished)
                        {
                            var flight = await FlightService.GetFlight(reservation.FlightId);

                            var sCoord = new GeoCoordinate(flight.DepartureLocation.Latitude, flight.DepartureLocation.Longitude);
                            var eCoord = new GeoCoordinate(flight.ArrivalLocation.Latitude, flight.ArrivalLocation.Longitude);

                            user.Bonus += (int) (sCoord.GetDistanceTo(eCoord) / 100000);
                            await UserService.UpdateUser(user);
                        }

                        return Ok(new { reservation });
                    }
                }
            }

            return BadRequest("Not enough data provided.");
        }

        // GET api/reservation/flight/{id}
        [HttpGet]
        [Route("flight/{id}")]
        public async Task<IActionResult> GetFlightReservation(long id)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetCurrentUser();

                if (user != null)
                {
                    var reservation = user.ReservedFlights.SingleOrDefault(rf => rf.FlightReservationId == id);

                    if (reservation != null)
                    {
                        return Ok(new { reservation });
                    }
                }
            }

            return BadRequest(ModelState);
        }

        // GET api/reservation/flight/remove/{id}
        [HttpGet]
        [Route("flight/remove/{id}")]
        public async Task<IActionResult> RemoveFlightReservation(long id)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetCurrentUser();

                if (user != null)
                {
                    var reservation = user.ReservedFlights.SingleOrDefault(rf => rf.FlightReservationId == id);

                    if (reservation != null)
                    {
                        await ReservationService.RemoveFlightReservation(reservation);

                        return Ok(200);
                    }
                }
            }

            return BadRequest(ModelState);
        }

        // GET api/reservation/flight/{id}/seats/taken
        [HttpGet]
        [Route("flight/{id}/seats/taken")]
        public async Task<IActionResult> GetFlightTakenSeats(long id)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetCurrentUser();

                if (user != null)
                {
                    var flight = await FlightService.GetFlight(id);

                    if (flight != null)
                    {
                        var aeroplane = await AeroplaneService.GetAeroplane(flight.AeroplaneId);

                        if (aeroplane != null)
                        {
                            var takenSeats = await ReservationService.GetTakenSeats(flight, aeroplane);

                            return Ok(new { takenSeats });
                        }
                    }
                }
            }

            return BadRequest(ModelState);
        }

        // POST api/reservation/flight/invite
        [HttpPost]
        [Route("flight/invite")]
        public async Task<IActionResult> FlightInvitation([FromBody]InvitationDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetCurrentUser();

                if (user != null)
                {
                    var reservation = user.ReservedFlights.SingleOrDefault(rf => rf.FlightReservationId == model.Id);

                    if (reservation != null)
                    {
                        var flight = await FlightService.GetFlight(reservation.FlightId);

                        if (flight != null)
                        {
                            var successfull = await UserService.InviteToFlight(model.FlightId, model.FriendUsername);

                            if (successfull)
                                return Ok(200);
                            else
                                return BadRequest("Friend not found!");
                        }
                    }
                }
            }

            return BadRequest("Not enough data provided.");
        }

        // POST api/reservation/flight/invite/response
        [HttpPost]
        [Route("flight/invite/response")]
        public async Task<IActionResult> FlightInvitationResponse([FromBody]InvitationResponseDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetCurrentUser();

                if (user != null)
                {
                    await UserService.AcceptInvitation(model.InvitationId, model.Accepted);
                    return Ok(200);
                }
            }

            return BadRequest("Not enough data provided.");
        }
    }
}