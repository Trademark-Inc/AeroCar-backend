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
        Zero,
        One,
        Two,
        Three,
        Four,
        Five
    }

    public enum CarType
    {
        HatchBack = 1,
        Sedan,
        Coupe,
        SUV
    }

    public enum FlightType
    {
        OneWay = 1,
        RoundTrip
    }
}
