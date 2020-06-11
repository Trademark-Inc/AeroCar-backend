using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Avio
{
    public class PriceListItem
    {
        [Key]
        public long PriceListIdemId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }
    }
}
