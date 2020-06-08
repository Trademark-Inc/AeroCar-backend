using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Avio
{
    public class AvioCompanyProfile
    {
        [Key]
        public long AvioCompanyProfileId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PromoDescription { get; set; }
    }
}
