using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Avio
{
    public class DeletedSeats
    {
        [Key]
        public long DeletedSeatId { get; set; }

        [ForeignKey("Seats.SeatsId")]
        public long SeatId { get; set; }

        public int SeatNumber { get; set; }


    }
}
