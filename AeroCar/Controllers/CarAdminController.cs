using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeroCar.Models.Car;
using AeroCar.Models.DTO.Car;
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
    public class CarAdminController : ControllerBase
    {
        public CarAdminController(RentACarService rentACarService, CarAdminService carAdminService, VehicleService vehicleService, 
            OfficeService officeService)
        {
            RentACarService = rentACarService;
            CarAdminService = carAdminService;
            VehicleService = vehicleService;
            OfficeService = officeService;
        }

        public RentACarService RentACarService { get; set; }
        public CarAdminService CarAdminService { get; set; }
        public VehicleService VehicleService { get; set; }
        public OfficeService OfficeService { get; set; }

        // GET api/caradmin/company/get/profile
        [HttpGet]
        [Route("company/get/profile")]
        public async Task<IActionResult> GetCompanyProfile()
        {
            if (ModelState.IsValid)
            {
                var user = await CarAdminService.GetCurrentUser();

                if (user != null)
                {
                    var carCompany = await RentACarService.GetCompany(user.CarCompanyId);

                    if (carCompany != null)
                    {
                        var carCompanyProfile = await RentACarService.GetCompanyProfile(carCompany.CarCompanyProfileId);

                        return Ok(new { carCompany, carCompanyProfile });
                    }
                }
            }

            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

        // POST api/caradmin/company/update/profile
        [HttpPost]
        [Route("company/update/profile")]
        public async Task<IActionResult> UpdateCompanyProfile([FromBody]CarCompanyProfile model)
        {
            if (ModelState.IsValid)
            {
                var user = await CarAdminService.GetCurrentUser();

                if (user != null)
                {
                    var carCompany = await RentACarService.GetCompany(user.CarCompanyId);

                    if (carCompany != null)
                    {
                        var carCompanyProfile = await RentACarService.GetCompanyProfile(carCompany.CarCompanyProfileId);

                        carCompanyProfile.Name = model.Name;
                        carCompanyProfile.Address = model.Address;
                        carCompanyProfile.PromoDescription = model.PromoDescription;

                        await RentACarService.UpdateCompanyProfile(carCompanyProfile);
                        return Ok(200);
                    }
                }
            }

            return BadRequest("Not enough data provided.");
        }

        #region Vehicles
        // GET api/caradmin/company/get/vehicles
        [HttpGet]
        [Route("company/get/vehicles")]
        public async Task<IActionResult> GetCompanyVehicles()
        {
            if (ModelState.IsValid)
            {
                var user = await CarAdminService.GetCurrentUser();

                if (user != null)
                {
                    var carCompany = await RentACarService.GetCompany(user.CarCompanyId);

                    if (carCompany != null)
                    {
                        var vehicles = carCompany.Vehicles;

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
                }
            }

            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

        // POST api/caradmin/company/create/vehicle
        [HttpPost]
        [Route("company/create/vehicle")]
        public async Task<IActionResult> CreateVehicle([FromBody]VehicleDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await CarAdminService.GetCurrentUser();

                if (user != null)
                {
                    var carCompany = await RentACarService.GetCompany(user.CarCompanyId);

                    if (carCompany != null)
                    {
                        var vehicle = new Vehicle()
                        {
                            Additional = model.Additional,
                            Baggage = model.Baggage,
                            CarCompanyId = carCompany.CarCompanyId,
                            CarType = model.CarType,
                            CostPerDay = model.CostPerDay,
                            Doors = model.Doors,
                            Fuel = model.Fuel,
                            Name = model.Name,
                            Passangers = model.Passangers,
                            Transmission = model.Transmission
                        };

                        carCompany.Vehicles.Add(vehicle);
                        await RentACarService.UpdateCompany(carCompany);

                        return Ok(200);
                    }
                }
            }

            return BadRequest("Not enough data provided.");
        }
        #endregion
    }
}