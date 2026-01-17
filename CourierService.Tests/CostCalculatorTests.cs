using CourierService.Application.Interfaces;
using CourierService.Domain;
using CourierService.Infrastructure.Offers;
using CourierService.Infrastructure.Services;
using FluentAssertions;
using Moq;

namespace CourierService.Tests
{
    public class CostCalculatorTests
    {
        [Fact]
        public void Should_calculate_cost_without_offer()
        {
            // Arrange
            var offers = new List<OfferOFR001>();
            var calculator = new CostCalculator(offers);
            var pkg = new Package("PKG1", 5, 5, "NA");

            // Act
            calculator.Calculate(pkg, 100);

            // Assert
            pkg.Discount.Should().Be(0);
            pkg.TotalCost.Should().Be(175);
        }

        
        [Fact]
        public void Calculate_ValidOffer_AppliesDiscount()
        {
            // Arrange
            var offerMock = new Mock<IOfferStrategy>();
            offerMock.Setup(o => o.Code).Returns("OFR001");
            offerMock.Setup(o => o.IsApplicable(It.IsAny<Package>())).Returns(true);
            offerMock.Setup(o => o.CalculateDiscount(700)).Returns(70);

            var calculator = new CostCalculator(new List<IOfferStrategy> { offerMock.Object });
            var package = new Package("PKG3", 10, 100, "OFR001");

            // Act
            calculator.Calculate(package, 100);

            // Assert
            Assert.Equal(70, package.Discount);
            Assert.Equal(630, package.TotalCost);
        }
        

        [Fact]
        public void Should_not_apply_discount_if_offer_not_found()
        {
            // Arrange
            var calculator = new CostCalculator(new List<OfferOFR001>());
            var pkg = new Package("PKG1", 50, 30, "INVALID");

            // Act
            calculator.Calculate(pkg, 100);

            // Assert
            pkg.Discount.Should().Be(0);
        }

        [Fact]
        public void Calculate_OfferNotApplicable_NoDiscount()
        {
            // Arrange
            var offerMock = new Mock<IOfferStrategy>();
            offerMock.Setup(o => o.Code).Returns("OFR001");
            offerMock.Setup(o => o.IsApplicable(It.IsAny<Package>()))
                     .Returns(false);

            var calculator = new CostCalculator(
                new[] { offerMock.Object });

            var package = new Package("PKG1", 1, 1, "OFR001");
            
            // Act
            calculator.Calculate(package, 100);

            // Assert
            Assert.Equal(0, package.Discount);
        }

    }
}