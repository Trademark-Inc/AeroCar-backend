using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CarController : ControllerBase
    {
        public RentACarService CarService { get; set; }

        public DestinationService DestinationService { get; set; }

        public VehicleService VehicleService { get; set; }

        public CarController(RentACarService carService, DestinationService destinationService, VehicleService vehicleService)
        {
            CarService = carService;
            DestinationService = destinationService;
            VehicleService = vehicleService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("company/get")]
        public async Task<IActionResult> GetCompanyProfile()
        {
            List<CarCompanyProfileDTO> carCompanyProfileDTOList = new List<CarCompanyProfileDTO>();

            if (ModelState.IsValid)
            {
                List<CarCompany> companies = await CarService.GetCompanies();
                List<CarCompanyProfile> companiesProfile = new List<CarCompanyProfile>();
                List<double> carCompanyRating = new List<double>();
                List<int> carCompanyRatingPicture = new List<int>();

                foreach (var carCompany in companies)
                {
                    companiesProfile.Add(await CarService.GetCompanyProfile(carCompany.CarCompanyId));
                    carCompanyRating.Add(await CarService.GetCompanyRatingAsDouble(carCompany.CarCompanyId));
                    carCompanyRatingPicture.Add(await CarService.GetCompanyRatingAsInteger(carCompany.CarCompanyId));
                }

                for (int i = 0; i < companies.Count; i++)
                {
                    string allOffices = "";
                    for (int j = 0; j < companies[i].Offices.Count; j++)
                    {
                        allOffices += companies[i].Offices[j].Location + ",";
                    }

                    CarCompanyProfileDTO acpDTO = new CarCompanyProfileDTO()
                    {
                        Id = companies[i].CarCompanyId,
                        Name = companiesProfile[i].Name,
                        Address = companiesProfile[i].Address,
                        Description = companiesProfile[i].PromoDescription,
                        Offices = allOffices,
                        Rating = carCompanyRating[i],
                        RatingPicture = carCompanyRatingPicture[i]
                    };
                    carCompanyProfileDTOList.Add(acpDTO);
                }

                return Ok(new { carCompanyProfileDTOList });
            }
            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet]
        [Route("company/details/get/{id}")]
        public async Task<IActionResult> GetCompanyDetails(long id)
        {
            CarCompany company = await CarService.GetCompany(id);
            CarCompanyProfile companyProfile = new CarCompanyProfile();

            CarCompanyProfileDTO carCompanyProfileDTO = new CarCompanyProfileDTO();
            
            List<Office> officeList = company.Offices;
            List<Vehicle> vehicleList = company.Vehicles;
            List<int> vehicleRating = new List<int>();

            foreach (var vehicle in vehicleList)
            {
                vehicleRating.Add(await VehicleService.GetVehicleRatingAsInteger(vehicle.VehicleId));
            }

            int carCompanyRatingPicture = 0;

            if (ModelState.IsValid)
            {
                List<OfficeDTO> officeDTOList = new List<OfficeDTO>();

                for (int i = 0; i < officeList.Count; i++)
                {
                    OfficeDTO officeDTO = new OfficeDTO();

                    DestinationDTO destinationDTO = new DestinationDTO();

                    destinationDTO.Latitude = officeList[i].Location.Latitude;
                    destinationDTO.Longitude = officeList[i].Location.Longitude;
                    destinationDTO.Name = officeList[i].Location.Name;
                    
                    officeDTO.OfficeId = officeList[i].OfficeId;
                    officeDTO.Location = destinationDTO;
                    officeDTO.Address = officeList[i].Address;

                    officeDTOList.Add(officeDTO);
                }

                List<VehicleDTO> vehicleDTOList = new List<VehicleDTO>();

                for (int i = 0; i < vehicleList.Count; i++)
                {               
                    VehicleDTO vehicleDTO = new VehicleDTO();

                    vehicleDTO.Additional = vehicleList[i].Additional;
                    vehicleDTO.Baggage = vehicleList[i].Baggage;
                    vehicleDTO.CarType = vehicleList[i].CarType;
                    vehicleDTO.CostPerDay = vehicleList[i].CostPerDay;
                    vehicleDTO.Doors = vehicleList[i].Doors;
                    vehicleDTO.Fuel = vehicleList[i].Fuel;
                    vehicleDTO.Name = vehicleList[i].Name;
                    vehicleDTO.Passangers = vehicleList[i].Passangers;
                    vehicleDTO.Rating = vehicleRating[i];
                    vehicleDTO.Transmission = vehicleList[i].Transmission;
                    vehicleDTO.VehicleId = vehicleList[i].VehicleId;
                    vehicleDTO.Additional = vehicleList[i].Additional;

                    vehicleDTOList.Add(vehicleDTO);
                }

                string allOfficies = "";
                for (int i = 0; i < officeList.Count; i++)
                {
                    allOfficies += officeList[i].Location.Name + ",";
                }

                companyProfile = await CarService.GetCompanyProfile(id);
                carCompanyRatingPicture = await CarService.GetCompanyRatingAsInteger(id);

                carCompanyProfileDTO.Id = company.CarCompanyId;
                carCompanyProfileDTO.Name = companyProfile.Name;
                carCompanyProfileDTO.RatingPicture = carCompanyRatingPicture;
                carCompanyProfileDTO.Address = companyProfile.Address;
                carCompanyProfileDTO.Description = companyProfile.PromoDescription;
                carCompanyProfileDTO.OfficeList = officeList;
                carCompanyProfileDTO.VehicleList = vehicleDTOList;
                carCompanyProfileDTO.Offices = allOfficies;

                return Ok(new { carCompanyProfileDTO });
            }
            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }
    }
}
