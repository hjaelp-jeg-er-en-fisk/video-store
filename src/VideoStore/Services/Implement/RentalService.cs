using System;
using System.Linq;
using VideoStore.Enums;
using VideoStore.Models;

namespace VideoStore.Services.Implement
{
    public class RentalService : IRentalService
    {
        public string GetCustomerRentalRecordStatement(Customer customer)
        {
            // Quick and dirty
            if (customer == null || string.IsNullOrWhiteSpace(customer.Name) || customer.Rentals == null || !customer.Rentals.Any())
                throw new ArgumentException("Customer data missing");

            var frequentRenterPoints = 0D;
            var totalAmount = 0D;
            var rentals = "";

            foreach (var rental in customer.Rentals)
            {
                var rentalPrice = GetRentalPriceFromPriceCode(rental.Movie.PriceCode, rental.DaysRented);
                rentals += $"\t{rental.Movie.Title}\t{rentalPrice.ToString(System.Globalization.CultureInfo.InvariantCulture)}\n";
                totalAmount += rentalPrice;
                frequentRenterPoints += GetFrequentRenterPointsFromRental(rental.Movie.PriceCode, rental.DaysRented);
            }

            string result = $"Rental Record for {customer.Name}\n"
                            + rentals
                            + $"You owed {totalAmount.ToString(System.Globalization.CultureInfo.InvariantCulture)}\n"
                            + $"You earned {frequentRenterPoints.ToString(System.Globalization.CultureInfo.InvariantCulture)} frequent renter points\n";

            return result;
        }

        private double GetRentalPriceFromPriceCode(PriceCode code, int daysRented)
        {
            var rentalAmount = 0D;

            switch (code)
            {
                case PriceCode.Regular:
                    rentalAmount += 2;
                    if (daysRented > 2)
                        rentalAmount += (daysRented - 2) * 1.5;
                    break;

                case PriceCode.NewRelease:
                    rentalAmount += daysRented * 3;
                    break;

                case PriceCode.Children:
                    rentalAmount += 1.5;
                    if (daysRented > 3)
                        rentalAmount += (daysRented - 3) * 1.5;
                    break;
            }

            return rentalAmount;
        }

        private double GetFrequentRenterPointsFromRental(PriceCode code, int daysRented)
        {
            var frequentRenterPoints = 1;

            if (code == PriceCode.NewRelease)
                frequentRenterPoints++;

            return frequentRenterPoints;
        }
    }
}