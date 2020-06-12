using AeroCar.Models.Avio;
using AeroCar.Models.DTO.Avio;
using AeroCar.Models.Rating;
using AeroCar.Models.Reservation;
using AeroCar.Repositories;
using AeroCar.Utility;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class AvioService
    {
        private readonly AvioRepository _avioRepository;

        public AvioService(AvioRepository avioRepository)
        {
            _avioRepository = avioRepository;
        }

        public async Task<List<AvioCompany>> GetCompanies()
        {
            return await _avioRepository.GetCompanies();
        }

        public async Task<AvioCompany> GetCompany(long id)
        {
            return await _avioRepository.GetCompany(id);
        }

        public async Task<AvioCompany> GetCompanyByName(string companyName)
        {
            return await _avioRepository.GetCompany(companyName);
        }

        public async Task<AvioCompanyProfile> GetCompanyProfile(long profileId)
        {
            return await _avioRepository.GetCompanyProfile(profileId);
        }

        public async Task<double> GetAverageCompanyRating(long id)
        {
            var ratings = await _avioRepository.GetCompanyRatingsByCompanyId(id);

            if (ratings == null) return 0.0;
            if (ratings.Count == 0) return 0.0;

            double sum = 0;
            foreach (AvioCompanyRating r in ratings)
            {
                sum += EnumsUtility.GetStarRatingAsDouble(r.Rate);
            }

            return sum / ratings.Count;
        }

        public async Task<double> GetAverageFlightRating(long id)
        {
            var ratings = await _avioRepository.GetFlightRatingsByCompanyId(id);

            if (ratings == null) return 0.0;
            if (ratings.Count == 0) return 0.0;

            double sum = 0;
            foreach (FlightRating r in ratings)
            {
                sum += EnumsUtility.GetStarRatingAsDouble(r.Rate);
            }

            return sum / ratings.Count;
        }

        public async Task<double> GetRevenue(long id)
        {
            var reservations = await _avioRepository.GetFlightReservationsByCompanyId(id);
            var flights = await _avioRepository.GetFlightsByCompanyId(id);

            if (flights != null && reservations != null)
            {
                double revenue = 0;
                foreach (FlightReservation fr in reservations)
                {
                    int index = flights.FindIndex(f => f.FlightId == fr.FlightId);

                    if (index >= 0) revenue += flights[index].Price;
                }

                return revenue;
            }

            return 0;
        }

        public async Task<List<GraphDTO>> GetLastMonthsSoldTickets(long id, int numberOfMonths)
        {
            var reservations = await _avioRepository.GetFlightReservationsByCompanyId(id);
            var flights = await _avioRepository.GetFlightsByCompanyId(id);

            List<GraphDTO> months = new List<GraphDTO>();
            var lastMonths = Enumerable.Range(0, numberOfMonths)
                              .Select(i => DateTime.Now.AddMonths(i - numberOfMonths + 1))
                              .Select(date => date.ToString("MM/yyyy"));

            var lastMonthsList = lastMonths.ToList();
            for (int i = 0; i < numberOfMonths; ++i)
            {
                months.Add(new GraphDTO());

                months[i].Month = lastMonthsList[i];
                months[i].SoldCount = 0;
            }

            if (flights != null && reservations != null)
            {
                foreach (FlightReservation fr in reservations)
                {
                    int index = flights.FindIndex(f => f.FlightId == fr.FlightId);

                    if (index >= 0)
                    {
                        var date = flights[index].Departure.ToString("MM/yyyy");

                        if (lastMonths.Contains(date))
                        {
                            var monthIndex = months.FindIndex(g => g.Month == date);

                            if (monthIndex >= 0)
                            {
                                ++months[monthIndex].SoldCount;
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
            var company = await _avioRepository.GetCompany(name);
            return company != null;
        }

        public async Task CreateCompany(AvioCompany company, AvioCompanyProfile profile)
        {
            await _avioRepository.CreateCompany(company, profile);
        }

        public async Task<bool> RemoveCompany(long id)
        {
            var result = await _avioRepository.RemoveCompany(id);
            return result;
        }

        public async Task UpdateCompanyProfile(AvioCompanyProfile avioCompanyProfile)
        {
            await _avioRepository.UpdateCompanyProfile(avioCompanyProfile);
        }

        public async Task UpdateCompany(AvioCompany company)
        {
            await _avioRepository.UpdateCompany(company);
        }
    }
}
