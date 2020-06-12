using AeroCar.Models;
using AeroCar.Models.Admin;
using AeroCar.Models.Avio;
using AeroCar.Models.DTO.Avio;
using AeroCar.Models.Reservation;
using AeroCar.Models.Users;
using AeroCar.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AvioAdminController : ControllerBase
    {
        public AvioAdminController(AvioService avioService, AvioAdminService avioAdminService, AeroplaneService aeroplaneService, FlightService flightService,
            FastReservationFlightTicketService fastReservationFlightTicketService, PriceListItemService priceListItemService)
        {
            AvioService = avioService;
            AvioAdminService = avioAdminService;
            AeroplaneService = aeroplaneService;
            FlightService = flightService;
            FastReservationFlightTicketService = fastReservationFlightTicketService;
            PriceListItemService = priceListItemService;
        }

        public AvioService AvioService { get; set; }
        public AvioAdminService AvioAdminService { get; set; }
        public AeroplaneService AeroplaneService { get; set; }
        public FlightService FlightService { get; set; }
        public FastReservationFlightTicketService FastReservationFlightTicketService { get; set; }
        public PriceListItemService PriceListItemService { get; set; }

        // GET api/avioadmin/company/get/profile
        [HttpGet]
        [Route("company/get/profile")]
        public async Task<IActionResult> GetCompanyProfile()
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        var avioCompanyProfile = await AvioService.GetCompanyProfile(avioCompany.AvioCompanyProfileId);

                        return Ok(new { avioCompany, avioCompanyProfile });
                    }
                }
            }

            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

        // POST api/avioadmin/company/update/profile
        [HttpPost]
        [Route("company/update/profile")]
        public async Task<IActionResult> UpdateCompanyProfile([FromBody]AvioCompanyProfile model)
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        var avioCompanyProfile = await AvioService.GetCompanyProfile(avioCompany.AvioCompanyProfileId);

                        avioCompanyProfile.Name = model.Name;
                        avioCompanyProfile.Address = model.Address;
                        avioCompanyProfile.PromoDescription = model.PromoDescription;

                        await AvioService.UpdateCompanyProfile(avioCompanyProfile);
                        return Ok(200);
                    }

                    return BadRequest("Avio company not found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
            }

            return BadRequest("Not enough data provided.");
        }

        // GET api/avioadmin/company/get/report
        [HttpGet]
        [Route("company/get/report")]
        public async Task<IActionResult> GetCompanyReport()
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        var companyRating = await AvioService.GetAverageCompanyRating(avioCompany.AvioCompanyId);
                        var flightRating = await AvioService.GetAverageFlightRating(avioCompany.AvioCompanyId);
                        var graph = await AvioService.GetLastMonthsSoldTickets(avioCompany.AvioCompanyId, 6);
                        var revenue = await AvioService.GetRevenue(avioCompany.AvioCompanyId);

                        return Ok(new { companyRating, flightRating, graph, revenue });
                    }
                }
            }

            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

        #region Flights
        // POST api/avioadmin/company/create/flight
        [HttpPost]
        [Route("company/create/flight")]
        public async Task<IActionResult> CreateFlight([FromBody]FlightDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model.Departure > model.Arrival) return BadRequest("Departure cannot be before landing.");

                if (model.Departure < DateTime.Now) return BadRequest("Departure cannot be before today's date.");

                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        model.DepartureLocation.DestinationId = 0;
                        model.ArrivalLocation.DestinationId = 0;
                        foreach (Destination d in model.Transit)
                        {
                            d.DestinationId = 0;
                        }

                        var flight = new Flight()
                        {
                            AeroplaneId = (await AeroplaneService.GetAeroplane(user.AvioCompanyId, model.Aeroplane)).AeroplaneId,
                            Arrival = model.Arrival,
                            ArrivalLocation = model.ArrivalLocation,
                            Departure = model.Departure,
                            DepartureLocation = model.DepartureLocation,
                            AvioCompanyId = user.AvioCompanyId,
                            Price = model.Price,
                            Transit = model.Transit,
                            TravelDistance = model.TravelDistance,
                            TravelTime = model.TravelTime
                        };

                        avioCompany.Flights.Add(flight);
                        await AvioService.UpdateCompany(avioCompany);

                        return Ok(200);
                    }

                    return BadRequest("Avio company not found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
            }

            return BadRequest("Not enough data provided.");
        }

        // POST api/avioadmin/company/remove/flight/{id}
        [HttpPost]
        [Route("company/remove/flight/{id}")]
        public async Task<IActionResult> RemoveFlight(long id)
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        var flight = avioCompany.Flights.Where(f => f.FlightId == id).SingleOrDefault();

                        if (flight != null)
                        {
                            await FlightService.RemoveFlight(flight);

                            return Ok(200);
                        }

                        return NotFound("Flight not found.");
                    }
                    
                    return BadRequest("Company wasn't found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
            }

            return BadRequest("No sufficient data provided.");
        }
        #endregion

        #region Fast Reservation Flight Tickets
        // POST api/avioadmin/company/create/ticket
        [HttpPost]
        [Route("company/create/ticket")]
        public async Task<IActionResult> CreateTicket([FromBody]FastReservationFlightTicketDTO ticket)
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        FastReservationFlightTicket frft = new FastReservationFlightTicket()
                        {
                            FlightId = ticket.FlightId,
                            Percent = ticket.Percent
                        };

                        if ((await FlightService.GetFlight(frft.FlightId)) == null) return BadRequest("Flight not found.");

                        avioCompany.FastReservationTickets.Add(frft);
                        await AvioService.UpdateCompany(avioCompany);

                        return Ok(200);
                    }

                    return BadRequest("Company wasn't found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
            }

            return BadRequest("Not enough data provided.");
        }

        // POST api/avioadmin/company/remove/ticket/{id}
        [HttpPost]
        [Route("company/remove/ticket/{id}")]
        public async Task<IActionResult> RemoveTicket(long id)
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        var ticket = avioCompany.FastReservationTickets.Where(t => t.FRFTId == id).SingleOrDefault();

                        if (ticket != null)
                        {
                            await FastReservationFlightTicketService.RemoveTicket(ticket);

                            return Ok(200);
                        }

                        return NotFound("Ticket not found.");
                    }
                    else return BadRequest("Company wasn't found.");
                }
            }

            return BadRequest("No sufficient data provided.");
        }
        #endregion

        #region Price List Items
        // POST api/avioadmin/company/create/item
        [HttpPost]
        [Route("company/create/item")]
        public async Task<IActionResult> CreateItem([FromBody]PriceListItemDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        PriceListItem item = new PriceListItem()
                        {
                            Name = model.Name,
                            Price = model.Price
                        };

                        avioCompany.PriceList.Add(item);
                        await AvioService.UpdateCompany(avioCompany);

                        return Ok(200);
                    }

                    return BadRequest("Company wasn't found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
            }

            return BadRequest("Not enough data provided.");
        }

        // POST api/avioadmin/company/remove/item/{id}
        [HttpPost]
        [Route("company/remove/item/{id}")]
        public async Task<IActionResult> RemoveItem(long id)
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        var item = avioCompany.PriceList.Where(t => t.PriceListIdemId == id).SingleOrDefault();

                        if (item != null)
                        {
                            await PriceListItemService.RemovePriceListItem(item);

                            return Ok(200);
                        }

                        return NotFound("Item not found.");
                    }

                    return BadRequest("Company wasn't found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
            }

            return BadRequest("No sufficient data provided.");
        }
        #endregion
    }
}
