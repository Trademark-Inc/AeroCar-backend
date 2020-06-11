using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Avio
{
    public class AvioCompanyProfileDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public int RatingPicture { get; set; }

        public string Address { get; set; }

        public string Destinations { get; set; }

        public string Description { get; set; }

        public List<Destination> DestinationsList { get; set; }
    }
}
