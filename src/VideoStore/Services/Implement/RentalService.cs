using System;
using System.Linq;
using LightInject;
using VideoStore.Enums;
using VideoStore.Models;

namespace VideoStore.Services.Implement
{
    public class RentalService : IRentalService
    {
        private readonly IPriceCalculatorService _priceCalculatorService;
        private readonly IFrequentRenterPointsService _frequentRenterPointService;

        public RentalService(IPriceCalculatorService priceCalculatorService, IFrequentRenterPointsService frequentRenterPointService)
        {
            _priceCalculatorService = priceCalculatorService ?? throw new ArgumentNullException(nameof(priceCalculatorService));
            _frequentRenterPointService = frequentRenterPointService ?? throw new ArgumentNullException(nameof(frequentRenterPointService));
        }

        public string GetCustomerRentalRecordStatement(Customer customer)
        {
            // Quick and dirty error handling
            if (customer == null || string.IsNullOrWhiteSpace(customer.Name) || customer.Rentals == null || !customer.Rentals.Any())
                throw new ArgumentException("Customer data missing");

            var rentals = "";
            var frequentRenterPoints = 0D;
            var totalAmount = 0D;

            foreach (var rental in customer.Rentals)
            {
                //Get rental price based on price code and rent days
                var rentalPrice = _priceCalculatorService.GetSingleRentalPrice(rental.Movie.PriceCode, rental.DaysRented);

                //Update amounts & rental string
                rentals += $"\t{rental.Movie.Title}\t{rentalPrice.ToString(System.Globalization.CultureInfo.InvariantCulture)}\n";
                frequentRenterPoints += _frequentRenterPointService.GetSingleRentalFrequentRenterPoints(rental.Movie.PriceCode, rental.DaysRented);
                totalAmount += rentalPrice;
            }

            //Build return statement
            string result = $"Rental Record for {customer.Name}\n"
                            + rentals
                            + $"You owed {totalAmount.ToString(System.Globalization.CultureInfo.InvariantCulture)}\n"
                            + $"You earned {frequentRenterPoints.ToString(System.Globalization.CultureInfo.InvariantCulture)} frequent renter points\n";

            return result;
        }
    }
}