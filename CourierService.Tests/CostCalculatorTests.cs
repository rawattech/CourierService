using CourierService.Domain;
using CourierService.Infrastructure.Offers;
using CourierService.Infrastructure.Services;
using FluentAssertions;

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
        public void Should_apply_OFR003_discount_when_criteria_met()
        {
            var offers = new List<OfferOFR003>
                {
                    new OfferOFR003()
                };

            var calculator = new CostCalculator(offers);
            var pkg = new Package("PKG3", 10, 100, "OFR003");

            calculator.Calculate(pkg, 100);

            pkg.Discount.Should().Be(35);
            pkg.TotalCost.Should().Be(665);

        }

        [Fact]
        public void Should_not_apply_discount_if_offer_not_found()
        {
            var calculator = new CostCalculator(new List<OfferOFR001>());
            var pkg = new Package("PKG1", 50, 30, "INVALID");

            calculator.Calculate(pkg, 100);

            pkg.Discount.Should().Be(0);
        }

    }
}