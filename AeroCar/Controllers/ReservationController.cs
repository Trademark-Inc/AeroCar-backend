using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeroCar.Models.Avio;
using AeroCar.Models.DTO.Reservation;
using AeroCar.Models.Reservation;
using AeroCar.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
                            priceListItems.Add(p);
                    }

                    var reservation = new FlightReservation()
                    {
                        FlightId = model.Flight.FlightId,
                        Canceled = false,
                        Finished = false,
                        Invitation = false,
                        UserId = user.Id,
                        PriceListItems = priceListItems
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
                        reservation.SeatNumber = model.Seat;
                        await ReservationService.UpdateFlightReservation(reservation);

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
                        await ReservationService.UpdateFlightReservation(reservation);

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

        // GET api/reservation/flight/{id}/aeroplane
        [HttpGet]
        [Route("flight/{id}/aeroplane")]
        public async Task<IActionResult> GetFlightAeroplane(long id)
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
                            return Ok(new { aeroplane });
                        }
                    }
                }
            }

            return BadRequest(ModelState);
        }
    }
}