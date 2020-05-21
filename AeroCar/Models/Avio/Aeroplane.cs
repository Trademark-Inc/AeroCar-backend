using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Avio
{
    public class Aeroplane
    {
        [Key]
        public long AeroplaneId { get; set; }

        [ForeignKey("AvioCompany.AvioCompanyId")]
        public long AvioCompanyId { get; set; }

        public string Name { get; set; }

        public Seats Seats { get; set; }
    }
}
