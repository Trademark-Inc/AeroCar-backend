using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models
{
    public enum UserType
    {
        Regular,
        SystemAdmin,
        AvioAdmin,
        CarAdmin
    }

    public enum UserStatus
    {
        InProcess = 0,
        Activated = 1,
        Declined = 2,
    }
}
