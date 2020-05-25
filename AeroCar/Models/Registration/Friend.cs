using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Registration
{
    public class Friend
    {
        [Key]
        public long RelationshipId { get; set; }

        [ForeignKey("RegularUser.Id")]
        public string FriendId { get; set; }

        [ForeignKey("RegularUser.Id")]
        public string UserId { get; set; }
    }
}
