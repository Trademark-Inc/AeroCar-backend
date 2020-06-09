using AeroCar.Models.Rating;
using AeroCar.Repositories;
using AeroCar.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class VehicleService
    {
        private readonly VehicleRepository _repository;

        public VehicleService(VehicleRepository repository)
        {
            _repository = repository;
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
    }
}
