using VideoStore.Enums;

namespace VideoStore.Services
{
    public interface IFrequentRenterPointsService
    {
        double GetSingleRentalFrequentRenterPoints(PriceCode code, int daysRented);
    }
}