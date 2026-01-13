using CourierService.Domain;
using CourierService.Infrastructure.Offers;
using FluentAssertions;

namespace CourierService.Tests
{
    public class OfferOFR001Tests
    {
        [Fact]
        public void Should_be_applicable_when_weight_and_distance_match()
        {
            var offer = new OfferOFR001();
            var pkg = new Package("PKG1", 100, 150, "OFR001");

            offer.IsApplicable(pkg).Should().BeTrue();
        }

        [Fact]
        public void Should_not_be_applicable_when_criteria_fails()
        {
            var offer = new OfferOFR001();
            var pkg = new Package("PKG1", 50, 250, "OFR001");

            offer.IsApplicable(pkg).Should().BeFalse();
        }
    }
}
