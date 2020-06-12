using AeroCar.Models.Car;
using AeroCar.Models.DTO.Car;
using AeroCar.Models.Rating;
using AeroCar.Models.Reservation;
using AeroCar.Repositories;
using AeroCar.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class VehicleService
    {
        private readonly VehicleRepository _repository;
        private readonly RentACarRepository _companyRepository;

        public VehicleService(VehicleRepository repository, RentACarRepository companyRepository)
        {
            _repository = repository;
            _companyRepository = companyRepository;
        }

        public async Task<Vehicle> GetVehicleById(long id)
        {
            return await _repository.GetVehicleById(id);
        }

        public async Task<int> GetVehicleRatingAsInteger(long id)
        {
            var ratings = await _repository.GetRatingsByVehicleId(id);

            if (ratings != null && ratings.Count > 0)
            {
                int sum = 0;
                foreach (VehicleRating v in ratings)
                {
                    sum += (int)EnumsUtility.GetStarRatingAsDouble(v.Rate);
                }

                return (int) (sum / ratings.Count);
            }

            return 0;
        }

        public async Task<List<Vehicle>> GetVehiclesBySearch(CarSearch searchParameters)
        {
            List<Vehicle> retVal = new List<Vehicle>();

            if (searchParameters.PickUpDate < DateTime.Now) return retVal;

            if (searchParameters.PickUpDate > searchParameters.ReturnDate) return retVal;

            if (searchParameters.PickUpDate == searchParameters.ReturnDate)
            {
                if (searchParameters.PickUpTime >= searchParameters.ReturnTime)
                {
                    return retVal;
                }
            }

            var vehicles = await _repository.GetAllVehicles();
            foreach (Vehicle v in vehicles)
            {
                if (v.CarType == searchParameters.CarType && v.Passangers >= searchParameters.Passangers)
                {
                    var company = await _companyRepository.GetCompany(v.CarCompanyId);
                    var pickUpLocationExists = company.Offices.SingleOrDefault(o => o.Location.Name == searchParameters.PickUpLocation) != null;
                    var returnLocationExists = company.Offices.SingleOrDefault(o => o.Location.Name == searchParameters.ReturnLocation) != null;

                    if (pickUpLocationExists && returnLocationExists)
                    {
                        var allCarReservations = await _companyRepository.GetCarReservationsByCompanyId(v.CarCompanyId);

                        if (allCarReservations != null)
                        {
                            var reservations = allCarReservations.Where(cr => cr.VehicleId == v.VehicleId);

                            DateTime pickUpDate = searchParameters.PickUpDate.Add(searchParameters.PickUpTime.TimeOfDay);
                            DateTime returnDate = searchParameters.ReturnDate.Add(searchParameters.ReturnTime.TimeOfDay);
                            bool available = true;
                            foreach (CarReservation r in reservations)
                            {
                                // rp    p     rr     r
                                if (r.PickUpDate < pickUpDate && r.ReturnDate > pickUpDate)
                                {
                                    available = false;
                                    break;
                                }

                                // p     rp    r     rr
                                if (r.PickUpDate > pickUpDate && r.PickUpDate < returnDate)
                                {
                                    available = false;
                                    break;
                                }

                                if (r.PickUpDate == pickUpDate && r.ReturnDate == returnDate)
                                {
                                    available = false;
                                    break;
                                }
                            }

                            if (available)
                            {
                                retVal.Add(v);
                            }
                        }
                        else
                        {
                            retVal.Add(v);
                        }
                    }
                }
            }

            return retVal;
        }

        public async Task RemoveVehicle(Vehicle v)
        {
            await _repository.RemoveVehicle(v);
        }

        public async Task<List<VehicleRating>> VehicleRating()
        {
            return await _repository.VehicleRating();
        }
    }
}
