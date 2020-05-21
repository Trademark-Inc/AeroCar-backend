using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Car
{
    public class Office
    {
        [Key]
        public long OfficeId { get; set; }

        [ForeignKey("CarCompany.CarCompanyId")]
        public long CarCompanyId { get; set; }

        public Destination Location { get; set; }

        public string Address { get; set; }
    }
}
