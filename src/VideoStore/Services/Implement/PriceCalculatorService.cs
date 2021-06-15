using System;
using VideoStore.Enums;
using VideoStore.Models;

namespace VideoStore.Services.Implement
{
    public class PriceCalculatorService : IPriceCalculatorService
    {

        public double GetSingleRentalPrice(PriceCode code, int daysRented)
        {
            switch (code)
            {
                case PriceCode.Regular:
                    return GetRegularMovieSingleRentalPrice(daysRented);

                case PriceCode.NewRelease:
                    return GetNewReleaseMovieSingleRentalPrice(daysRented);

                case PriceCode.Children:
                    return GetChildrenMovieSingleRentalPrice(daysRented);
                
                default:
                    return 0D;
            }
        }

        private double GetRegularMovieSingleRentalPrice(int daysRented)
        {
            var rentalAmount = 2D;

            if (daysRented > 2)
                rentalAmount += (daysRented - 2) * 1.5;

            return rentalAmount;
        }
      
        private double GetNewReleaseMovieSingleRentalPrice(int daysRented)
        {
            return daysRented * 3;
        }

        private double GetChildrenMovieSingleRentalPrice(int daysRented)
        {
            var rentalAmount = 1.5D;

            if (daysRented > 3)
                rentalAmount += (daysRented - 3) * 1.5;

            return rentalAmount;
        }
    }
}