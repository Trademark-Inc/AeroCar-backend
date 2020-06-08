using AeroCar.Models.Admin;
using AeroCar.Models.Car;
using AeroCar.Repositories;
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
    }
}
