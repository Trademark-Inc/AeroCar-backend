using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeroCar.Models.Car;
using AeroCar.Models.DTO.Car;
using AeroCar.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        public CarController(VehicleService vehicleService)
        {
            VehicleService = vehicleService;
        }

        public VehicleService VehicleService { get; set; }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchCars([FromQuery]CarSearch model)
        {
            if (ModelState.IsValid)
            {
                var vehicles = await VehicleService.GetVehiclesBySearch(model);

                List<VehicleDTO> vehicleDTOs = new List<VehicleDTO>();
                foreach (Vehicle v in vehicles)
                {
                    vehicleDTOs.Add(new VehicleDTO()
                    {
                        VehicleId = v.VehicleId,
                        Additional = v.Additional,
                        Baggage = v.Baggage,
                        CarType = v.CarType,
                        CostPerDay = v.CostPerDay,
                        Doors = v.Doors,
                        Fuel = v.Fuel,
                        Name = v.Name,
                        Passangers = v.Passangers,
                        Transmission = v.Transmission,
                        Rating = await VehicleService.GetVehicleRatingAsInteger(v.VehicleId)
                    });
                }

                return Ok(vehicleDTOs);
            }

            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

    }
}