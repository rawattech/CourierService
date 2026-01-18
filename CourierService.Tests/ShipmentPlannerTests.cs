using CourierService.Domain;
using CourierService.Infrastructure.Offers;
using CourierService.Infrastructure.Services;
using FluentAssertions;

namespace CourierService.Tests
{
    public class ShipmentPlannerTests
    {
        [Fact]
        public void Should_select_packages_with_max_weight_under_limit()
        {
            var packages = new List<Package>
            {
                new("PKG1", 50, 30, "NA"),
                new("PKG2", 75, 125, "NA"),
                new("PKG3", 175, 100, "NA")
            };

            var planner = new ShipmentPlanner();
            var shipment = planner.PlanShipment(packages, 200);

            shipment.ToList().First().Weight.Should().Be(175);
            shipment.ToList().First().Id.Should().Contain("PKG3");
        }

        [Fact]
        public void SelectShipment_MaximizesPackageCount()
        {
            // Arrange
            var packages = new List<Package>
            {
                new("PKG1", 50, 10, "NA"),
                new("PKG2", 60, 20, "NA"),
                new("PKG3", 70, 30, "NA")
            };

            var selector = new ShipmentPlanner();

            // Act  
            var shipment = selector.PlanShipment(packages, 200);

            // Assert
            Assert.Equal(3, shipment.Count);
        }
    }
}
