using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Admin
{
    public class CarAdmin
    {
        [Key]
        public long CarAdminId { get; set; }

        [ForeignKey("CarCompany.CarCompanyId")]
        public long CarCompanyId { get; set; }

        public bool SetUpPassword { get; set; }
    }
}
