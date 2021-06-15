using NUnit.Framework;
using VideoStore.Enums;
using VideoStore.Models;
using VideoStore.Services;

namespace VideoStore.Tests.FrequentRenterPoints
{
    [TestFixture]
    public class FrequentRenterPointsTests : ContainerFixture
    {
        private IFrequentRenterPointsService _frequentRenterPointsService;

        [Test]
        public void RegularMovieRentalFrequentRenterPoints()
        {
            Assert.AreEqual(1, 
                _frequentRenterPointsService.GetSingleRentalFrequentRenterPoints(PriceCode.Regular, 1));
        }

        [Test]
        public void ChildrenMovieRentalFrequentRenterPoints()
        {
            Assert.AreEqual(1,
                _frequentRenterPointsService.GetSingleRentalFrequentRenterPoints(PriceCode.Children, 1));
        }

        [Test]
        public void NewReleaseMovieRentalFrequentRenterPoints()
        {
            Assert.AreEqual(2,
                _frequentRenterPointsService.GetSingleRentalFrequentRenterPoints(PriceCode.NewRelease, 1));
        }

    }
}