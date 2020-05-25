using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Avio
{
    public class Seats
    {
        [Key]
        public long SeatsId { get; set; }

        [ForeignKey("Aeroplane.AeroplaneId")]
        public long AeroplaneId { get; set; }

        public int SeatCount { get; set; }

        public int InOneRow { get; set; }

        public List<DeletedSeats> DeletedSeats { get; set; }
    }
}
