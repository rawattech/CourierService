using CourierService.Infrastructure.Offers;
using CourierService.Domain;
using Xunit;

namespace CourierService.Tests
{
    public class ConfigurableOfferStrategyTests
    {
        [Fact]
        public void IsApplicable_ReturnsTrue_WhenWithinLimits()
        {
            var rule = new OfferRule
            {
                Code = "OFR001",
                DiscountPercentage = 0.1m,
                MinWeight = 50,
                MaxWeight = 200,
                MinDistance = 0,
                MaxDistance = 200
            };

            var offer = new ConfigurableOfferStrategy(rule);

            var package = new Package("PKG1", 100, 100, "OFR001");
            

            Assert.True(offer.IsApplicable(package));
        }

        [Fact]
        public void IsApplicable_ReturnsFalse_WhenWeightTooHigh()
        {
            // Arrange
            var rule = new OfferRule
            {
                MinWeight = 0,
                MaxWeight = 50,
                MinDistance = 0,
                MaxDistance = 200
            };

            var offer = new ConfigurableOfferStrategy(rule);

            var package = new Package("PKG1", 100, 100, "OFR001");
            
            // Act, Assert
            Assert.False(offer.IsApplicable(package));
        }

        [Fact]
        public void CalculateDiscount_ReturnsCorrectPercentage()
        {
            // Arrange
            var rule = new OfferRule
            {
                DiscountPercentage = 0.2m
            };

            var offer = new ConfigurableOfferStrategy(rule);

            // Act
            var discount = offer.CalculateDiscount(500);

            // Assert
            Assert.Equal(100, discount);
        }

    }
}
