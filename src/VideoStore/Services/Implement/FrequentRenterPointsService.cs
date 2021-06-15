using VideoStore.Enums;
using VideoStore.Models;

namespace VideoStore.Services.Implement
{
    public class FrequentRenterPointsService : IFrequentRenterPointsService
    {
        public double GetSingleRentalFrequentRenterPoints(PriceCode code, int daysRented)
        {
            //Consider changing this logic if more special cases are added.
            return code == PriceCode.NewRelease ? 2D : 1D;
        }
    }
}