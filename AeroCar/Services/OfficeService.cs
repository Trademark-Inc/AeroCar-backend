using AeroCar.Models.Car;
using AeroCar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class OfficeService
    {
        private readonly OfficeRepository _repository;

        public OfficeService(OfficeRepository repository)
        {
            _repository = repository;
        }

        public async Task RemoveOffice(Office o)
        {
            await _repository.RemoveOffice(o);
        }
    }
}
