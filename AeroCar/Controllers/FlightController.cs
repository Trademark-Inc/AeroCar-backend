using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeroCar.Models.Avio;
using AeroCar.Models.DTO.Avio;
using AeroCar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        public FlightController(FlightService flightService, AvioService avioService)
        {
            FlightService = flightService;
            AvioService = avioService;
        }

        private readonly FlightService FlightService;
        private readonly AvioService AvioService;

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFlight(long id)
        {
            if (ModelState.IsValid)
            {
                var flight = await FlightService.GetFlight(id);
                
                if (flight != null)
                {
                    var company = await AvioService.GetCompany(flight.AvioCompanyId);

                    if (company != null)
                    {
                        var companyProfile = await AvioService.GetCompanyProfile(company.AvioCompanyProfileId);

                        return Ok(new { flight, company, companyProfile });
                    }
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetFlights([FromQuery]FlightSearch model)
        {
            if (ModelState.IsValid)
            {
                if (model.Arrival != new DateTime() && model.Departure > model.Arrival)
                {
                    return BadRequest("Arrival date can't be before than departure date.");
                }

                if (model.Ticket == Models.FlightType.OneWay)
                {
                    var peopleCount = model.Adults + model.Children + model.Infants;
                    var outboundFlights = await FlightService.GetFlightsBySearch(model.Origin, model.Destination, model.Departure, peopleCount);
                    return Ok(new { outboundFlights });
                }
                else
                {
                    if (model.Arrival == null || model.Arrival == new DateTime()) return BadRequest("Arrival date not chosen.");

                    var peopleCount = model.Adults + model.Children + model.Infants;
                    var outboundFlights = await FlightService.GetFlightsBySearch(model.Origin, model.Destination, model.Departure, peopleCount);
                    var returnFlights = await FlightService.GetFlightsBySearch(model.Destination, model.Origin, model.Arrival, peopleCount);
                    
                    return Ok(new { outboundFlights, returnFlights });
                }
            }

            return BadRequest(ModelState);
        }
    }
}