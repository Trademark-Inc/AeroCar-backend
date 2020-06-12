using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeroCar.Models;
using AeroCar.Models.Car;
using AeroCar.Models.DTO;
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

                    return BadRequest("Car company not found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
            }

            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

        // GET api/caradmin/company/get/report
        [HttpGet]
        [Route("company/get/report")]
        public async Task<IActionResult> GetCompanyReport()
        {
            if (ModelState.IsValid)
            {
                var user = await CarAdminService.GetCurrentUser();

                if (user != null)
                {
                    var carCompany = await RentACarService.GetCompany(user.CarCompanyId);

                    if (carCompany != null)
                    {
                        var companyRating = await RentACarService.GetCompanyRatingAsInteger(carCompany.CarCompanyId);
                        var graph = await RentACarService.GetLastMonthsCarReservations(carCompany.CarCompanyId, 6);

                        return Ok(new { companyRating, graph });
                    }

                    return BadRequest("Car company not found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
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

                    return BadRequest("Car company not found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
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

                    return BadRequest("Car company not found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
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

        // POST api/caradmin/company/remove/vehicle/{id}
        [HttpPost]
        [Route("company/remove/vehicle/{id}")]
        public async Task<IActionResult> RemoveVehicle(long id)
        {
            if (ModelState.IsValid)
            {
                var user = await CarAdminService.GetCurrentUser();

                if (user != null)
                {
                    var carCompany = await RentACarService.GetCompany(user.CarCompanyId);

                    if (carCompany != null)
                    {
                        var vehicle = carCompany.Vehicles.Where(v => v.VehicleId == id).SingleOrDefault();

                        if (vehicle != null)
                        {
                            await VehicleService.RemoveVehicle(vehicle);

                            return Ok(200);
                        }

                        return NotFound("Vehicle not found.");
                    }
                    else return BadRequest("Company wasn't found.");
                }
            }

            return BadRequest("No sufficient data provided.");
        }
        #endregion

        #region Offices
        // GET api/caradmin/company/get/offices
        [HttpGet]
        [Route("company/get/offices")]
        public async Task<IActionResult> GetCompanyOffices()
        {
            if (ModelState.IsValid)
            {
                var user = await CarAdminService.GetCurrentUser();

                if (user != null)
                {
                    var carCompany = await RentACarService.GetCompany(user.CarCompanyId);

                    if (carCompany != null)
                    {
                        var offices = carCompany.Offices;

                        List<OfficeDTO> officeDTOs = new List<OfficeDTO>();
                        foreach (Office o in offices)
                        {
                            officeDTOs.Add(new OfficeDTO()
                            {
                                Location = new DestinationDTO()
                                {
                                    Name = o.Location.Name,
                                    Latitude = o.Location.Latitude,
                                    Longitude = o.Location.Longitude
                                },
                                Address = o.Address,
                                OfficeId = o.OfficeId
                            });
                        }

                        return Ok(officeDTOs);
                    }
                    return BadRequest("Car company not found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
            }

            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

        // POST api/caradmin/company/create/office
        [HttpPost]
        [Route("company/create/office")]
        public async Task<IActionResult> CreateOffice([FromBody]OfficeRequest model)
        {
            if (ModelState.IsValid)
            {
                var user = await CarAdminService.GetCurrentUser();

                if (user != null)
                {
                    var carCompany = await RentACarService.GetCompany(user.CarCompanyId);

                    if (carCompany != null)
                    {
                        var office = new Office()
                        {
                            Address = model.Address,
                            Location = new Destination() { Name = model.City },
                            CarCompanyId = carCompany.CarCompanyId
                        };

                        carCompany.Offices.Add(office);
                        await RentACarService.UpdateCompany(carCompany);

                        return Ok(200);
                    }
                    return BadRequest("Car company not found.");
                }

                return Unauthorized("You must log in as an administrator of this company.");
            }

            return BadRequest("Not enough data provided.");
        }

        // POST api/caradmin/company/remove/office/{id}
        [HttpPost]
        [Route("company/remove/office/{id}")]
        public async Task<IActionResult> RemoveOffice(long id)
        {
            if (ModelState.IsValid)
            {
                var user = await CarAdminService.GetCurrentUser();

                if (user != null)
                {
                    var carCompany = await RentACarService.GetCompany(user.CarCompanyId);

                    if (carCompany != null)
                    {
                        var office = carCompany.Offices.Where(o => o.OfficeId == id).SingleOrDefault();

                        if (office != null)
                        {
                            await OfficeService.RemoveOffice(office);

                            return Ok(200);
                        }

                        return NotFound("Office wasn't found.");
                    }
                    else return BadRequest("Car company wasn't found.");
                }
            }

            return BadRequest("No sufficient data provided.");
        }
        #endregion
    }
}