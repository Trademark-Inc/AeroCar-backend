using AeroCar.Models.Admin;
using AeroCar.Models.Car;
using AeroCar.Models.DTO.Car;
using AeroCar.Models.Rating;
using AeroCar.Models.Reservation;
using AeroCar.Repositories;
using AeroCar.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class RentACarService
    {
        private readonly RentACarRepository _rentACarRepository;

        public RentACarService(RentACarRepository rentACarRepository)
        {
            _rentACarRepository = rentACarRepository;
        }

        public async Task<List<CarCompany>> GetCompanies()
        {
            return await _rentACarRepository.GetCompanies();
        }

        public async Task<CarCompany> GetCompany(long id)
        {
            return await _rentACarRepository.GetCompany(id);
        }

        public async Task<CarCompany> GetCompanyByName(string companyName)
        {
            return await _rentACarRepository.GetCompany(companyName);
        }

        public async Task<CarCompanyProfile> GetCompanyProfile(long profileId)
        {
            return await _rentACarRepository.GetCompanyProfile(profileId);
        }

        public async Task<int> GetCompanyRatingAsInteger(long id)
        {
            var ratings = await _rentACarRepository.GetCompanyRatingsByCompanyId(id);

            if (ratings == null) return 0;
            if (ratings.Count == 0) return 0;

            double sum = 0;
            foreach (CarCompanyRating r in ratings)
            {
                sum += EnumsUtility.GetStarRatingAsDouble(r.Rate);
            }

            return (int) (sum / ratings.Count);
        }

        public async Task<Double> GetCompanyRatingAsDouble(long id)
        {
            var ratings = await _rentACarRepository.GetCompanyRatingsByCompanyId(id);

            if (ratings == null) return 0;
            if (ratings.Count == 0) return 0;

            double sum = 0;
            foreach (CarCompanyRating r in ratings)
            {
                sum += EnumsUtility.GetStarRatingAsDouble(r.Rate);
            }

            return sum / ratings.Count;
        }

        public async Task<List<GraphDTO>> GetLastMonthsCarReservations(long id, int numberOfMonths)
        {
            var reservations = await _rentACarRepository.GetCarReservationsByCompanyId(id);
            var company = await GetCompany(id);
            var vehicles = company.Vehicles;

            List<GraphDTO> months = new List<GraphDTO>();
            var lastMonths = Enumerable.Range(0, numberOfMonths)
                              .Select(i => DateTime.Now.AddMonths(i - numberOfMonths + 1))
                              .Select(date => date.ToString("MM/yyyy"));

            var lastMonthsList = lastMonths.ToList();
            for (int i = 0; i < numberOfMonths; ++i)
            {
                months.Add(new GraphDTO());

                months[i].Month = lastMonthsList[i];
                months[i].CarReservations = 0;
            }

            if (vehicles != null && reservations != null)
            {
                foreach (CarReservation cr in reservations)
                {
                    int index = vehicles.FindIndex(v => v.VehicleId == cr.VehicleId);

                    if (index >= 0)
                    {
                        var date = cr.PickUpDate.ToString("MM/yyyy");

                        if (lastMonths.Contains(date))
                        {
                            var monthIndex = months.FindIndex(g => g.Month == date);

                            if (monthIndex >= 0)
                            {
                                ++months[monthIndex].CarReservations;
                            }
                        }
                    }
                }

                return months;
            }

            return months;
        }

        public async Task<bool> CompanyExists(string name)
        {
            var company = await _rentACarRepository.GetCompany(name);
            return company != null;
        }

        public async Task CreateCompany(CarCompanyProfile profile)
        {
            await _rentACarRepository.CreateCompany(profile);
        }

        public async Task<bool> RemoveCompany(long id)
        {
            var result = await _rentACarRepository.RemoveCompany(id);
            return result;
        }

        public async Task UpdateCompanyProfile(CarCompanyProfile carCompanyProfile)
        {
            await _rentACarRepository.UpdateCompanyProfile(carCompanyProfile);
        }

        public async Task UpdateCompany(CarCompany company)
        {
            await _rentACarRepository.UpdateCompany(company);
        }
    }
}
