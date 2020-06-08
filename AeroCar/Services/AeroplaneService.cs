using AeroCar.Models.Avio;
using AeroCar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class AeroplaneService
    {
        private readonly AeroplaneRepository _repository;

        public AeroplaneService(AeroplaneRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Aeroplane>> GetAllAeroplanes()
        {
            return await _repository.GetAllAeroplanes();
        }

        public async Task<List<Aeroplane>> GetAeroplanesByCompanyId(long id)
        {
            return await _repository.GetAeroplanesByCompanyId(id);
        }

        public async Task<Aeroplane> GetAeroplane(long companyId, string name)
        {
            var aeroplanes = await _repository.GetAeroplanesByCompanyId(companyId);

            if (aeroplanes != null)
            {
                var plane = aeroplanes.SingleOrDefault(a => a.Name == name);
                return plane;
            }

            return null;
        }

        public async Task<Aeroplane> GetAeroplane(long id)
        {
            return await _repository.GetAeroplane(id);
        }

        public async Task<bool> AeroplaneExists(long companyId, string name)
        {
            var aeroplanes = await _repository.GetAeroplanesByCompanyId(companyId);

            if (aeroplanes != null)
            {
                var plane = aeroplanes.SingleOrDefault(a => a.Name == name);
                return plane != null;
            }

            return false;
        }

        public async Task AddAeroplane(Aeroplane a)
        {
            await _repository.AddAeroplane(a);
        }

        public async Task RemoveAeroplane(Aeroplane a)
        {
            await _repository.RemoveAeroplane(a);
        }
    }
}
