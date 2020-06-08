using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Admin
{
    public class AdminDTO
    {
        public string Id { get; set; }

        public string Username { get; set; }
        
        public string AdminType { get; set; }

        public string Company { get; set; }
    }
}
