using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Registration
{
    public class InvitationDTO
    {
        public long FlightId { get; set; }

        public long Id { get; set; }

        public string FriendUsername { get; set; }

        public string FlightDestination { get; set; }
    }
}
