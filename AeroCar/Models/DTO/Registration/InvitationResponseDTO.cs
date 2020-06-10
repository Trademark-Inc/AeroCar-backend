using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Registration
{
    public class InvitationResponseDTO
    {
        public long InvitationId { get; set; }

        public bool Accepted { get; set; }
    }
}
