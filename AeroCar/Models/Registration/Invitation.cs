using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Registration
{
    public class Invitation
    {
        [Key]
        public long InvitationId { get; set; }

        [ForeignKey("RegularUser.Id")]
        public string FromUserId { get; set; }

        [ForeignKey("RegularUser.Id")]
        public string ToUserId { get; set; }

        [ForeignKey("Flight.FlightId")]
        public long FlightId { get; set; }

        public DateTime SentDate { get; set; }

        public bool Accepted { get; set; }
    }
}
