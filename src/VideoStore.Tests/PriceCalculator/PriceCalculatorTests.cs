using NUnit.Framework;
using VideoStore.Enums;
using VideoStore.Services;

namespace VideoStore.Tests.PriceCalculator
{
    [TestFixture]
    public class PriceCalculatorTests : ContainerFixture
    {
        private IPriceCalculatorService _priceCalculatorService;

        [Test]
        public void RegularMovieRentalPrice1Day()
        {
            Assert.AreEqual(2D,
                _priceCalculatorService.GetSingleRentalPrice(PriceCode.Regular, 2));
        }

        [Test]
        public void RegularMovieRentalPrice5Days()
        {
            Assert.AreEqual(6.5,
                _priceCalculatorService.GetSingleRentalPrice(PriceCode.Regular, 5));
        }

        //todo: Create test cases for remaining price calculations.

    }
}