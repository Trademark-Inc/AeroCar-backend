using AeroCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Utility
{
    public class EnumsUtility
    {
        public static double GetStarRatingAsDouble(StarRating s)
        {
            switch (s)
            {
                case StarRating.One: return 1.0;
                case StarRating.Two: return 2.0;
                case StarRating.Three: return 3.0;
                case StarRating.Four: return 4.0;
                case StarRating.Five: return 5.0;
                default: return 0.0;
            }
        }
    }
}
