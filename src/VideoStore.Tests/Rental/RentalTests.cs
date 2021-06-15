using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using VideoStore.Enums;
using VideoStore.Models;
using VideoStore.Services;

namespace VideoStore.Tests.Rental
{
    [TestFixture]
    public class RentalTests : ContainerFixture
    {
        private IRentalService _rentalService;
        private Models.Customer _customer;
        private readonly string _decimalPoint = Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator;

        [SetUp]
        public void Setup()
        {
            _customer = new Models.Customer
            {
                Name = "Fred",
                Rentals = new List<Models.Rental>()
            };
        }

        [Test]
        public void SingleNewReleaseStatement()
        {
            _customer.Rentals.Add(new Models.Rental
            {
                Movie = new Movie
                {
                    Title = "The Cell",
                    PriceCode = PriceCode.NewRelease
                },
                DaysRented = 3
            });

            Assert.AreEqual("Rental Record for Fred\n\tThe Cell\t9\nYou owed 9\nYou earned 2 frequent renter points\n",
                _rentalService.GetCustomerRentalRecordStatement(_customer));
        }

        [Test]
        public void DualNewReleaseStatement()
        {
            _customer.Rentals.Add(new Models.Rental
            {
                Movie = new Movie
                {
                    Title = "The Cell",
                    PriceCode = PriceCode.NewRelease
                },
                DaysRented = 3
            });

            _customer.Rentals.Add(new Models.Rental
            {
                Movie = new Movie
                {
                    Title = "The Tigger Movie",
                    PriceCode = PriceCode.NewRelease
                },
                DaysRented = 3
            });

            Assert.AreEqual("Rental Record for Fred\n\tThe Cell\t9\n\tThe Tigger Movie\t9\nYou owed 18\nYou earned 4 frequent renter points\n",
                _rentalService.GetCustomerRentalRecordStatement(_customer));
        }

        [Test]
        public void SingleChildrenStatement()
        {
            _customer.Rentals.Add(new Models.Rental
            {
                Movie = new Movie
                {
                    Title = "The Tigger Movie",
                    PriceCode = PriceCode.Children
                },
                DaysRented = 3
            });

            Assert.AreEqual($"Rental Record for Fred\n\tThe Tigger Movie\t1{_decimalPoint}5\nYou owed 1{_decimalPoint}5\nYou earned 1 frequent renter points\n",
                _rentalService.GetCustomerRentalRecordStatement(_customer));
        }

        [Test]
        public void MultipleRegularStatement()
        {
            _customer.Rentals.Add(new Models.Rental
            {
                Movie = new Movie
                {
                    Title = "Plan 9 from Outer Space",
                    PriceCode = PriceCode.Regular
                },
                DaysRented = 1
            });

            _customer.Rentals.Add(new Models.Rental
            {
                Movie = new Movie
                {
                    Title = "8 1/2",
                    PriceCode = PriceCode.Regular
                },
                DaysRented = 2
            });

            _customer.Rentals.Add(new Models.Rental
            {
                Movie = new Movie
                {
                    Title = "Eraserhead",
                    PriceCode = PriceCode.Regular
                },
                DaysRented = 3
            });

            Assert.AreEqual($"Rental Record for Fred\n\tPlan 9 from Outer Space\t2\n\t8 1/2\t2\n\tEraserhead\t3{_decimalPoint}5\nYou owed 7{_decimalPoint}5\nYou earned 3 frequent renter points\n",
                _rentalService.GetCustomerRentalRecordStatement(_customer));
        }

    }
}
