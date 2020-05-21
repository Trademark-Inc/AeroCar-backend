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
        InProcess,
        Activated,
        Declined,
    }

    public enum StarRating
    {
        One,
        Two,
        Three,
        Four,
        Five
    }

    public enum CarType
    {
        HatchBack,
        Sedan,
        Coupe,
        SUV
    }
}
