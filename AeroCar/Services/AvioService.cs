using AeroCar.Models.Avio;
using AeroCar.Repositories;
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
