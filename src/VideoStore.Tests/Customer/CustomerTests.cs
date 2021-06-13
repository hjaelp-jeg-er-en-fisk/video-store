using System;
using System.Collections.Generic;
using System.Threading;
using LightInject;
using NUnit.Framework;
using VideoStore.Enums;
using VideoStore.Models;
using VideoStore.Services;
using VideoStore.Services.Implement;

namespace VideoStore.Tests.Customer
{
    [TestFixture]
	public class CustomerTests : ContainerFixture
	{
        private IRentalService rentalService;
        private Models.Customer customer;

        [SetUp]
        public void Setup()
        {
            customer = new Models.Customer
            {
                Name = "Fred",
                Rentals = new List<Rental>()
            };
        }

        [Test]
        public void SingleNewReleaseStatement()
        {
            customer.Rentals.Add(new Rental
            {
                Movie = new Movie
                {
                    Title = "The Cell",
                    PriceCode = PriceCode.NewRelease
                },
                DaysRented = 3
            });

            var rentalRecordForCustomer = rentalService.GetCustomerRentalRecordStatement(customer);

            Assert.AreEqual("Rental Record for Fred\n\tThe Cell\t9\nYou owed 9\nYou earned 2 frequent renter points\n", rentalRecordForCustomer);
        }

        [Test]
        public void DualNewReleaseStatement()
        {
            customer.Rentals.Add(new Rental
            {
                Movie = new Movie
                {
                    Title = "The Cell",
                    PriceCode = PriceCode.NewRelease
                },
                DaysRented = 3
            });

            customer.Rentals.Add(new Rental
            {
                Movie = new Movie
                {
                    Title = "The Tigger Movie",
                    PriceCode = PriceCode.NewRelease
                },
                DaysRented = 3
            });

            var rentalRecordForCustomer = rentalService.GetCustomerRentalRecordStatement(customer);

            Assert.AreEqual("Rental Record for Fred\n\tThe Cell\t9\n\tThe Tigger Movie\t9\nYou owed 18\nYou earned 4 frequent renter points\n", rentalRecordForCustomer);
        }

        [Test]
        public void SingleChildrensStatement()
        {
            customer.Rentals.Add(new Rental
            {
                Movie = new Movie
                {
                    Title = "The Tigger Movie",
                    PriceCode = PriceCode.Children
                },
                DaysRented = 3
            });

            var rentalRecordForCustomer = rentalService.GetCustomerRentalRecordStatement(customer);

            var numberFormatNumberDecimalSeparator = Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
            Assert.AreEqual($"Rental Record for Fred\n\tThe Tigger Movie\t1{numberFormatNumberDecimalSeparator}5\nYou owed 1{numberFormatNumberDecimalSeparator}5\nYou earned 1 frequent renter points\n", rentalRecordForCustomer);
        }

        [Test]
        public void MultipleRegularStatement()
        {
            customer.Rentals.Add(new Rental
            {
                Movie = new Movie
                {
                    Title = "Plan 9 from Outer Space",
                    PriceCode = PriceCode.Regular
                },
                DaysRented = 1
            });

            customer.Rentals.Add(new Rental
            {
                Movie = new Movie
                {
                    Title = "8 1/2",
                    PriceCode = PriceCode.Regular
                },
                DaysRented = 2
            });

            customer.Rentals.Add(new Rental
            {
                Movie = new Movie
                {
                    Title = "Eraserhead",
                    PriceCode = PriceCode.Regular
                },
                DaysRented = 3
            });

            var rentalRecordForCustomer = rentalService.GetCustomerRentalRecordStatement(customer);

            var numberFormatNumberDecimalSeparator = Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator;

            Assert.AreEqual($"Rental Record for Fred\n\tPlan 9 from Outer Space\t2\n\t8 1/2\t2\n\tEraserhead\t3{numberFormatNumberDecimalSeparator}5\nYou owed 7{numberFormatNumberDecimalSeparator}5\nYou earned 3 frequent renter points\n", rentalRecordForCustomer);
        }

    }
}
