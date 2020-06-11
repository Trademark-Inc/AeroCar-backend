using AeroCar.Models.Avio;
using AeroCar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class PriceListItemService
    {
        private readonly PriceListItemRepository _repository;

        public PriceListItemService(PriceListItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PriceListItem>> GetAllPriceListItems()
        {
            return await _repository.GetAllPriceListItems();
        }

        public async Task<PriceListItem> GetPriceListItem(long id)
        {
            return await _repository.GetPriceListItem(id);
        }

        public async Task AddPriceListItem(PriceListItem p)
        {
            await _repository.AddPriceListItem(p);
        }

        public async Task RemovePriceListItem(PriceListItem p)
        {
            await _repository.RemovePriceListItem(p);
        }
    }
}
