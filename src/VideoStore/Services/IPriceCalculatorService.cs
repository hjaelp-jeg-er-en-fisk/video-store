using VideoStore.Enums;
using VideoStore.Models;

namespace VideoStore.Services
{
    public interface IPriceCalculatorService
    {
        double GetSingleRentalPrice(PriceCode code, int daysRented);
    }
}